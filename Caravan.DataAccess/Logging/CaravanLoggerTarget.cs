﻿using Fasterflect;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Utilities;
using Finsa.Caravan.Common.Utilities.Collections.ReadOnly;
using Finsa.CodeServices.Common;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Finsa.Caravan.Common;
using LogLevel = Common.Logging.LogLevel;

namespace Finsa.Caravan.DataAccess.Logging
{
    /// <summary>
    ///   Target per il log di Caravan su database.
    /// </summary>
    [Target("CaravanDbLogger")]
    public class CaravanLoggerTarget : Target
    {
        private static readonly SimpleLayout DefaultLogLevel = new SimpleLayout("${level}");

        /// <summary>
        ///   Builds the target with default layout formats.
        /// </summary>
        public CaravanLoggerTarget()
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
            var logLevel = (LogLevel) Enum.Parse(typeof(LogLevel), LogLevel.Render(logEvent));
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
            var result = DataSource.Logger.LogRaw(
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

        private static LogMessage ParseMessage(LogEventInfo logEvent)
        {
            var msg = logEvent.FormattedMessage;
            var ex = logEvent.Exception;
            if (ex != null)
            {
                // Get the innermost exception.
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return new LogMessage
                {
                    ShortMessage = ((msg != null) ? msg + " - " : Constants.EmptyString),
                    LongMessage = ex.StackTrace,
                    Context = Constants.EmptyString,
                    Arguments = new[]
                    {
                        // Keep aligned with Finsa.Common.Logging.CaravanLogger.SerializeJsonlogMessageCallback
                        KeyValuePair.Create("exception_data", ex.Data.LogAsJson()),
                        KeyValuePair.Create("exception_source", ex.Source ?? Constants.EmptyString)
                    }
                };
            }
            if (!String.IsNullOrWhiteSpace(msg) && msg.StartsWith(CaravanLogger.JsonMessagePrefix))
            {
                var json = msg.Substring(CaravanLogger.JsonMessagePrefix.Length);
                var entry = CaravanLogger.JsonSerializer.DeserializeObject<LogMessage>(json);
                entry.LongMessage = entry.LongMessage ?? Constants.EmptyString;
                entry.Context = entry.Context ?? Constants.EmptyString;
                entry.Arguments = entry.Arguments ?? ReadOnlyList.Empty<KeyValuePair<string, string>>();
                return entry;
            }
            return new LogMessage
            {
                ShortMessage = msg,
                LongMessage = Constants.EmptyString,
                Context = Constants.EmptyString,
                Arguments = ReadOnlyList.Empty<KeyValuePair<string, string>>()
            };
        }

        #region Raw reflection for NLog

        private static readonly Flags DictFlags = Flags.Static | Flags.NonPublic;
        private static readonly MemberGetter GlobalDict = typeof(GlobalDiagnosticsContext).DelegateForGetFieldValue("dict", DictFlags);
        private static readonly MemberGetter ThreadDict = typeof(MappedDiagnosticsContext).DelegateForGetPropertyValue("ThreadDictionary", DictFlags);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<KeyValuePair<string, string>> GetGlobalVariables()
        {
            var globalDict = GlobalDict.Invoke(null) as Dictionary<string, string>;
            Debug.Assert(globalDict != null, "This should always be true for NLog 3.2.0, check before using other versions.");
            // We make a snapshot of the dictionary, since it may easily change.
            return globalDict.Select(kv => KeyValuePair.Create(kv.Key, kv.Value)).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<KeyValuePair<string, string>> GetThreadVariables()
        {
            var threadDict = ThreadDict.Invoke(null) as Dictionary<string, string>;
            Debug.Assert(threadDict != null, "This should always be true for NLog 3.2.0, check before using other versions.");
            // We make a snapshot of the dictionary, since it may easily change.
            return threadDict.Select(kv => KeyValuePair.Create(kv.Key, kv.Value)).ToList();
        }

        #endregion Raw reflection for NLog
    }
}