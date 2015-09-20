using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common.Collections.ReadOnly;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LogLevel = Common.Logging.LogLevel;

namespace Finsa.Caravan.DataAccess.Logging
{
    /// <summary>
    ///   Target per il log di Caravan su database.
    /// </summary>
    [Target("CaravanLog")]
    public class CaravanLogTarget : Target
    {
        static readonly SimpleLayout DefaultLogLevel = new SimpleLayout("${level}");
        static readonly SimpleLayout DefaultUserLogin = new SimpleLayout("${identity:name=true:lowercase=true}");
        static readonly SimpleLayout DefaultCodeUnit = new SimpleLayout("${callsite:className=true:methodName=false:lowercase=true}");
        static readonly SimpleLayout DefaultFunction = new SimpleLayout("${callsite:className=false:methodName=true:lowercase=true}");

        /// <summary>
        ///   Il layout da applicare per mostrare il livello di log.
        /// </summary>
        [RequiredParameter]
        public Layout LogLevel { get; set; } = DefaultLogLevel;

        /// <summary>
        ///   Il layout da applicare per mostrare l'utente loggato che ha prodotto il messaggio.
        /// </summary>
        [RequiredParameter]
        public Layout UserLogin { get; set; } = DefaultUserLogin;

        /// <summary>
        ///   Il layout da applicare per ottenere la classe, o il modulo, da cui è partito il messaggio.
        /// </summary>
        [RequiredParameter]
        public Layout CodeUnit { get; set; } = DefaultCodeUnit;

        /// <summary>
        ///   Il layout da applicare per ottenere la funzione, o la procedura, da cui è partito il messaggio.
        /// </summary>
        [RequiredParameter]
        public Layout Function { get; set; } = DefaultFunction;

        /// <summary>
        ///   Il layout da applicare per ottenere il messaggio breve.
        /// </summary>
        public Layout ShortMessage { get; set; }

        /// <summary>
        ///   Il layout da applicare per ottenere il messaggio esteso.
        /// </summary>
        public Layout LongMessage { get; set; }

        /// <summary>
        ///   Il layout da applicare per ottenere il contesto da dove è partito il messaggio.
        /// </summary>
        public Layout Context { get; set; }

        /// <summary>
        ///   Writes logging event to the log target. classes.
        /// </summary>
        /// <param name="logEvent">Logging event to be written out.</param>
        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                var logLevel = (LogLevel) Enum.Parse(typeof(LogLevel), LogLevel.Render(logEvent));
                var userLogin = UserLogin.Render(logEvent);
                var codeUnit = CodeUnit.Render(logEvent);
                var function = Function.Render(logEvent);

                var logMessage = ParseMessage(logEvent);
                logMessage.ShortMessage = (ShortMessage != null) ? ShortMessage.Render(logEvent) : logMessage.ShortMessage;
                logMessage.LongMessage = (LongMessage != null) ? LongMessage.Render(logEvent) : logMessage.LongMessage;
                logMessage.Context = (Context != null) ? Context.Render(logEvent) : logMessage.Context;          

                // In order to be able to use thread local information, it must _not_ be async.
                var result = CaravanDataSource.Logger.LogRaw(
                    logLevel,
                    CommonConfiguration.Instance.AppName,
                    userLogin,
                    codeUnit,
                    function,
                    logMessage.ShortMessage,
                    logMessage.LongMessage,
                    logMessage.Context,
                    logMessage.Arguments
                );

                if (!result.Succeeded)
                {
                    throw result.Exception;
                }
            }
            catch (Exception ex)
            {
                // Uso il log di emergenza, nel caso ci siano stati errori.
                try
                {
                    // Devo loggare immediatamente l'eccezione che è stata ricevuta.
                    // Cerco di salvare comunque il messaggio di log appena emesso.
                    ServiceProvider.EmergencyLog.Error($"Internal error while logging [{logEvent.FormattedMessage}]", ex);
                }
                catch
                {
                    // Se anche qui da errore, mi arrendo :(
                }
            }
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);
        }

        protected override void Write(AsyncLogEventInfo[] logEvents)
        {
            base.Write(logEvents);
        }

        protected override void FlushAsync(AsyncContinuation asyncContinuation)
        {
            asyncContinuation?.Invoke(null);
        }

        static void EmptyAsyncContinuation(Exception ex)
        {
            // Nulla di che, per ora...
        }

        static LogMessage ParseMessage(LogEventInfo logEvent)
        {
            // Il messaggio che è giunto nel log: può essere una stringa semplice, oppure uno YAML
            // di LogMessage.
            var formattedMessage = logEvent.FormattedMessage;

            // Verifico se è stato passato un LogMessage come parametro. Se si, lo uso, altrimenti
            // ne creo uno vuoto e inserisco valori di default.
            var logMessage = (logEvent.Parameters?.GetValue(0) as LogMessage) ?? new LogMessage
            {
                ShortMessage = formattedMessage,
                LongMessage = string.Empty,
                Context = string.Empty
            };

            if (logMessage.Arguments == null)
            {
                logMessage.Arguments = ReadOnlyList.Empty<KeyValuePair<string, string>>();
            }

            // Valuto se si tratta di un messaggio a cui è stata allegata una eccezione.
            var exception = logEvent.Exception;
            if (exception != null)
            {
                // Recupero l'eccezione più interna e poi creo un LogMessage ad hoc, di modo che me
                // lo trovi valorizzato con tutti i campi estratti dall'eccezione.
                exception = exception.GetBaseException();

                // Arricchisco il messaggio di log con le info presenti nell'eccezione.
                logMessage.Exception = exception;
            }

            // Restituisco il messaggio appena elaborato.
            return logMessage;
        }
    }
}
