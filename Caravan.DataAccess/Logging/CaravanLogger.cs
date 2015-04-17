using Common.Logging;
using Common.Logging.Factory;
using Common.Logging.NLog;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Serialization;
using Finsa.Caravan.Common.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using FormatMessageCallback = System.Action<Common.Logging.FormatMessageHandler>;
using LoggerNLog = NLog.Logger;
using LogLevel = Common.Logging.LogLevel;
using LogLevelNLog = NLog.LogLevel;

namespace Finsa.Caravan.DataAccess.Logging
{
    /// <summary>
    ///   Concrete implementation of <see cref="ILog"/> interface specific to Caravan.
    /// </summary>
    /// <author>Alessio Parma</author>
    public class CaravanLogger : NLogLogger, ICaravanLog
    {
        #region Fields

        /// <summary>
        ///   The prefix used to identify complex messages.
        /// </summary>
        public const string JsonMessagePrefix = "#JSON#";

        private static readonly IJsonSerializer CachedJsonSerializer = new JsonNetSerializer();

        private readonly LoggerNLog _logger;

        #endregion Fields

        #region Construction

        /// <summary>
        ///   Builds an instance of the Caravan logger.
        /// </summary>
        /// <param name="logger">The <see cref="LoggerNLog"/> that will be used as backend.</param>
        public CaravanLogger(LoggerNLog logger) : base(logger)
        {
            _logger = logger;
        }

        #endregion Construction

        #region Properties

        /// <summary>
        ///   The JSON serializer used to pack messages.
        /// </summary>
        public static IJsonSerializer JsonSerializer
        {
            get { return CachedJsonSerializer; }
        }

        #endregion

        #region ICaravanLog Members

        #region Trace

        public void TraceArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void TraceArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion

        #region Debug

        public void DebugArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void DebugArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion

        #region Info

        public void InfoArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void InfoArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion

        #region Warn

        public void WarnArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void WarnArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion

        #region Error

        public void ErrorArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void ErrorArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion

        #region Fatal

        public void FatalArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void FatalArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion

        private static string LogMessageHandler(Func<LogMessage> logMessageCallback)
        {
            var safeCallback = logMessageCallback;
            return safeCallback == null ? Constants.EmptyString : SerializeJsonlogMessageCallback(safeCallback());
        }

        private static string LogMessageHandler(string shortMsg, string longMsg, string context)
        {
            return SerializeJsonlogMessageCallback(new LogMessage
            {
                ShortMessage = shortMsg,
                LongMessage = longMsg,
                Context = context
            });
        }

        private static string SerializeJsonlogMessageCallback(LogMessage logMessage)
        {
            if (logMessage.Exception != null)
            {
                var exception = logMessage.Exception;
                logMessage.Exception = null;
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
                logMessage.ShortMessage = exception.Message;
                logMessage.LongMessage = exception.StackTrace;
            }
            return JsonMessagePrefix + CachedJsonSerializer.SerializeObject(logMessage);
        }

        #endregion ICaravanLog Members

        #region Private Methods

        /// <summary>
        ///   Actually sends the message to the underlying log system.
        /// </summary>
        /// <param name="logLevel">the level of this log event.</param>
        /// <param name="message">the message to log</param>
        /// <param name="exception">the exception to log (may be null)</param>
        protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
        {
            var level = GetLevel(logLevel);
            var logEvent = new LogEventInfo(level, _logger.Name, null, "{0}", new[] { message }, exception);
            _logger.Log(typeof(CaravanLogger), logEvent);
        }

        private static LogLevelNLog GetLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.All:
                    return LogLevelNLog.Trace;

                case LogLevel.Trace:
                    return LogLevelNLog.Trace;

                case LogLevel.Debug:
                    return LogLevelNLog.Debug;

                case LogLevel.Info:
                    return LogLevelNLog.Info;

                case LogLevel.Warn:
                    return LogLevelNLog.Warn;

                case LogLevel.Error:
                    return LogLevelNLog.Error;

                case LogLevel.Fatal:
                    return LogLevelNLog.Fatal;

                case LogLevel.Off:
                    return LogLevelNLog.Off;

                default:
                    throw new ArgumentOutOfRangeException("logLevel", logLevel, @"Unknown log level");
            }
        }

        #endregion
    }
}