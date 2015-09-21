using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common.Collections.ReadOnly;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LogLevel = Common.Logging.LogLevel;

namespace Finsa.Caravan.DataAccess.Logging
{
    /// <summary>
    ///   Target per il log di Caravan su database.
    /// </summary>
    [Target("CaravanLog")]
    public sealed class CaravanLogTarget : Target
    {
        private static readonly SimpleLayout DefaultUserLogin = new SimpleLayout("${identity:name=true:lowercase=true}");
        private static readonly SimpleLayout DefaultCodeUnit = new SimpleLayout("${callsite:className=true:methodName=false:lowercase=true}");
        private static readonly SimpleLayout DefaultFunction = new SimpleLayout("${callsite:className=false:methodName=true:lowercase=true}");

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
                var logMessage = ToLogMessage(logEvent);

                // In order to be able to use thread local information, it must _not_ be async.
                var result = CaravanDataSource.Logger.LogRaw(
                    ParseLogLevel(logEvent.Level),
                    CommonConfiguration.Instance.AppName,
                    UserLogin.Render(logEvent),
                    CodeUnit.Render(logEvent),
                    Function.Render(logEvent),
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
                // Uso il log di emergenza nel caso ci siano stati errori.
                UseEmergencyLog(ex, logEvent.FormattedMessage);
            }
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            Write(logEvent.LogEvent);
            logEvent.Continuation?.Invoke(null);
        }

        protected override void Write(AsyncLogEventInfo[] logEvents)
        {
            foreach (var logEvent in logEvents)
            {
                Write(logEvent.LogEvent);
                logEvent.Continuation?.Invoke(null);
            }
        }

        protected override void FlushAsync(AsyncContinuation asyncContinuation)
        {
            asyncContinuation?.Invoke(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LogMessage ToLogMessage(LogEventInfo logEvent)
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

            logMessage.ShortMessage = ShortMessage?.Render(logEvent) ?? logMessage.ShortMessage;
            logMessage.LongMessage = LongMessage?.Render(logEvent) ?? logMessage.LongMessage;
            logMessage.Context = Context?.Render(logEvent) ?? logMessage.Context;

            return logMessage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static LogLevel ParseLogLevel(NLog.LogLevel logLevel)
        {
            switch (logLevel.Name[0])
            {
                case 'd':
                case 'D':
                    return LogLevel.Debug;

                case 't':
                case 'T':
                    return LogLevel.Trace;

                case 'i':
                case 'I':
                    return LogLevel.Info;

                case 'w':
                case 'W':
                    return LogLevel.Warn;

                case 'e':
                case 'E':
                    return LogLevel.Error;

                case 'f':
                case 'F':
                    return LogLevel.Fatal;

                default:
                    return LogLevel.Debug;
            }
        }

        private static void UseEmergencyLog(Exception ex, string logMessage)
        {
            try
            {
                // Devo loggare immediatamente l'eccezione che è stata ricevuta.
                // Cerco di salvare comunque il messaggio di log appena emesso.
                ServiceProvider.EmergencyLog.Error($"Internal error while logging [{logMessage}]", ex);
            }
            catch
            {
                // Se anche qui da errore, mi arrendo :(
            }
        }
    }
}