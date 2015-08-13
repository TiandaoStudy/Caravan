using Fasterflect;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Collections.ReadOnly;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

        /// <summary>
        ///   Builds the target with default layout formats.
        /// </summary>
        public CaravanLogTarget()
        {
            LogLevel = DefaultLogLevel;
            UserLogin = new SimpleLayout("${identity:name=true:lowercase=true}");
            CodeUnit = new SimpleLayout("${callsite:className=true:methodName=false:lowercase=true}");
            Function = new SimpleLayout("${callsite:className=false:methodName=true:lowercase=true}");
        }

        [RequiredParameter]
        public Layout LogLevel { get; set; }

        [RequiredParameter]
        public Layout UserLogin { get; set; }

        [RequiredParameter]
        public Layout CodeUnit { get; set; }

        [RequiredParameter]
        public Layout Function { get; set; }

        public Layout ShortMessage { get; set; }

        public Layout LongMessage { get; set; }

        public Layout Context { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            var logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), LogLevel.Render(logEvent));
            var userLogin = UserLogin.Render(logEvent);
            var codeUnit = CodeUnit.Render(logEvent);
            var function = Function.Render(logEvent);
            var logMessage = ParseMessage(logEvent);
            logMessage.ShortMessage = (ShortMessage != null) ? ShortMessage.Render(logEvent) : logMessage.ShortMessage;
            logMessage.LongMessage = (LongMessage != null) ? LongMessage.Render(logEvent) : logMessage.LongMessage;
            logMessage.Context = (Context != null) ? Context.Render(logEvent) : logMessage.Context;
            var arguments = GetGlobalVariables().Union(GetThreadVariables()).Union(logMessage.Arguments);

            // In order to be able to use thread local information, it must _not_ be async.
            // ReSharper disable once UnusedVariable
            var result = CaravanDataSource.Logger.LogRaw(
                logLevel,
                CommonConfiguration.Instance.AppName,
                userLogin,
                codeUnit,
                function,
                logMessage.ShortMessage,
                logMessage.LongMessage,
                logMessage.Context,
                arguments
            );
        }

        static LogMessage ParseMessage(LogEventInfo logEvent)
        {
            // Il messaggio che è giunto nel log: può essere una stringa semplice, oppure uno YAML
            // di LogMessage.
            var formattedMessage = logEvent.FormattedMessage;

            // Verifico se è stato passato un LogMessage come parametro. Se si, lo uso, altrimenti ne creo uno vuoto.
            var logMessage = (logEvent?.Parameters?.GetValue(0) as LogMessage) ?? new LogMessage
            {
                ShortMessage = formattedMessage,
                LongMessage = string.Empty,
                Context = string.Empty,
                Arguments = ReadOnlyList.Empty<KeyValuePair<string, string>>()
            };

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

        #region Raw reflection for NLog

        static readonly Flags DictFlags = Flags.Static | Flags.NonPublic;
        static readonly MemberGetter GlobalDict = typeof(GlobalDiagnosticsContext).DelegateForGetFieldValue("dict", DictFlags);
        static readonly MemberGetter ThreadDict = typeof(MappedDiagnosticsContext).DelegateForGetPropertyValue("ThreadDictionary", DictFlags);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static IEnumerable<KeyValuePair<string, string>> GetGlobalVariables()
        {
            var globalDict = GlobalDict.Invoke(null) as Dictionary<string, string>;
            Debug.Assert(globalDict != null, "This should always be true for NLog 4.0.1, check before using other versions.");
            // We make a snapshot of the dictionary, since it may easily change.
            return globalDict.Select(kv => KeyValuePair.Create(kv.Key, kv.Value)).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static IEnumerable<KeyValuePair<string, string>> GetThreadVariables()
        {
            var threadDict = ThreadDict.Invoke(null) as Dictionary<string, string>;
            Debug.Assert(threadDict != null, "This should always be true for NLog 4.0.1, check before using other versions.");
            // We make a snapshot of the dictionary, since it may easily change.
            return threadDict.Select(kv => KeyValuePair.Create(kv.Key, kv.Value)).ToList();
        }

        #endregion Raw reflection for NLog
    }
}
