using System;
using System.Reflection;
using Common.Logging;
using Common.Logging.NLog;
using Finsa.Caravan.Common.Serialization;
using Finsa.Caravan.Common.Utilities;
using Newtonsoft.Json;
using NLog;
using LogLevel = Common.Logging.LogLevel;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Extensions for <see cref="ILog"/> which make it more compatible with <see cref="ILogger"/>.
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        ///   The prefix used to identify complex messages.
        /// </summary>
        public const string JsonMessagePrefix = "#JSON#";

        private static readonly IJsonSerializer CachedJsonSerializer = new JsonNetSerializer();

        

        /// <summary>
        ///   The JSON serializer used to pack messages.
        /// </summary>
        public static IJsonSerializer JsonSerializer
        {
            get { return CachedJsonSerializer; }
        }

        /// <summary>
        ///   Converts given object in a very compact JSON format. If given object is null, an empty
        ///   string is returned.
        /// </summary>
        /// <typeparam name="TObj">The type of the object. Used to avoid boxing.</typeparam>
        /// <param name="obj">The object that should be converted.</param>
        /// <returns>A very compact JSON corresponding to given object.</returns>
        public static string LogAsJson<TObj>(this TObj obj)
        {
            return ReferenceEquals(obj, null) ? Constants.EmptyString : CachedJsonSerializer.SerializeObject(obj);
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        public static void Trace(this ILog log, string shortMessage, string longMessage = null, string context = null)
        {
            WriteInternal(log, LogLevel.Trace, LogMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logMessageCallback"></param>
        public static void Trace(this ILog log, Func<LogMessage> logMessageCallback)
        {
            if (log.IsTraceEnabled)
            {
                WriteInternal(log, LogLevel.Trace, LogMessageHandler(logMessageCallback));
            }
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        public static void Debug(this ILog log, string shortMessage, string longMessage = null, string context = null)
        {
            log.Debug(LogMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logMessageCallback"></param>
        public static void Debug(this ILog log, Func<LogMessage> logMessageCallback)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(LogMessageHandler(logMessageCallback));
            }
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        public static void Info(this ILog log, string shortMessage, string longMessage = null, string context = null)
        {
            log.Info(LogMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logMessageCallback"></param>
        public static void Info(this ILog log, Func<LogMessage> logMessageCallback)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(LogMessageHandler(logMessageCallback));
            }
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        public static void Warn(this ILog log, string shortMessage, string longMessage = null, string context = null)
        {
            log.Warn(LogMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logMessageCallback"></param>
        public static void Warn(this ILog log, Func<LogMessage> logMessageCallback)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(LogMessageHandler(logMessageCallback));
            }
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        public static void Error(this ILog log, string shortMessage, string longMessage = null, string context = null)
        {
            log.Error(LogMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logMessageCallback"></param>
        public static void Error(this ILog log, Func<LogMessage> logMessageCallback)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(LogMessageHandler(logMessageCallback));
            }
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        public static void Fatal(this ILog log, string shortMessage, string longMessage = null, string context = null)
        {
            log.Fatal(LogMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logMessageCallback"></param>
        public static void Fatal(this ILog log, Func<LogMessage> logMessageCallback)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(LogMessageHandler(logMessageCallback));
            }
        }

        #region Private methods

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

        private static void WriteInternal(ILog log, LogLevel logLevel, string message)
        {
            if (log is NLogLogger)
            {
                var nlog = NLogLogger.GetValue(log) as Logger;
                var nlogEvent = new LogEventInfo(GetNLogLevel(logLevel), nlog.Name, message);
                nlog.Log(typeof(LogExtensions), nlogEvent);
            }
            else
            {
                switch (logLevel)
                {
                    case LogLevel.Trace:
                        log.Trace(message);
                        break;
                }
            }
        }

        #endregion

        #region NLog-specific code

        private static readonly FieldInfo NLogLogger = typeof(NLogLogger).GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);

        private static NLog.LogLevel GetNLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("logLevel", logLevel, "Unknown log level");
            }
        }

        #endregion
    }

    /// <summary>
    ///   Internal format used for complex log message passing.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct LogMessage
    {
        /// <summary>
        ///   Short message.
        /// </summary>
        [JsonProperty("s")]
        public string ShortMessage { get; set; }

        /// <summary>
        ///   Long message.
        /// </summary>
        [JsonProperty("l")]
        public string LongMessage { get; set; }

        /// <summary>
        ///   Context.
        /// </summary>
        [JsonProperty("c")]
        public string Context { get; set; }

        /// <summary>
        ///   Exception (optional), must _not_ be serialized.
        /// </summary>
        public Exception Exception { get; set; }
    }
}