using System;
using Common.Logging;
using Finsa.Caravan.Common.Serialization;
using Finsa.Caravan.Common.Utilities;
using Newtonsoft.Json;

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
            log.Trace(JsonFormatMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="messageFormat"></param>
        public static void Trace(this ILog log, Func<JsonMessageFormat> messageFormat)
        {
            if (log.IsTraceEnabled)
            {
                log.Trace(JsonFormatMessageHandler(messageFormat));
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
            log.Debug(JsonFormatMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="messageFormat"></param>
        public static void Debug(this ILog log, Func<JsonMessageFormat> messageFormat)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(JsonFormatMessageHandler(messageFormat));
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
            log.Info(JsonFormatMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="messageFormat"></param>
        public static void Info(this ILog log, Func<JsonMessageFormat> messageFormat)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(JsonFormatMessageHandler(messageFormat));
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
            log.Warn(JsonFormatMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="messageFormat"></param>
        public static void Warn(this ILog log, Func<JsonMessageFormat> messageFormat)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(JsonFormatMessageHandler(messageFormat));
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
            log.Error(JsonFormatMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="messageFormat"></param>
        public static void Error(this ILog log, Func<JsonMessageFormat> messageFormat)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(JsonFormatMessageHandler(messageFormat));
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
            log.Fatal(JsonFormatMessageHandler(shortMessage, longMessage, context));
        }

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="log"></param>
        /// <param name="messageFormat"></param>
        public static void Fatal(this ILog log, Func<JsonMessageFormat> messageFormat)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(JsonFormatMessageHandler(messageFormat));
            }
        }

        private static string JsonFormatMessageHandler(Func<JsonMessageFormat> messageFormat)
        {
            var safeMessageFormat = messageFormat;
            return safeMessageFormat == null ? Constants.EmptyString : SerializeJsonMessageFormat(safeMessageFormat());
        }

        private static string JsonFormatMessageHandler(string shortMsg, string longMsg, string context)
        {
            return SerializeJsonMessageFormat(new JsonMessageFormat
            {
                ShortMessage = shortMsg,
                LongMessage = longMsg,
                Context = context
            });
        }

        private static string SerializeJsonMessageFormat(JsonMessageFormat jsonMessageFormat)
        {
            if (jsonMessageFormat.Exception != null)
            {
                var exception = jsonMessageFormat.Exception;
                jsonMessageFormat.Exception = null;
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
                jsonMessageFormat.ShortMessage = exception.Message;
                jsonMessageFormat.LongMessage = exception.StackTrace;
            }
            return JsonMessagePrefix + CachedJsonSerializer.SerializeObject(jsonMessageFormat);
        }
    }

    /// <summary>
    ///   Internal format used for complex log message passing.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct JsonMessageFormat
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
        ///   Exception (optional), must not be serialized.
        /// </summary>
        public Exception Exception { get; set; }
    }
}