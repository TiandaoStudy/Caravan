using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Finsa.CodeServices.Serialization;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Collections.ReadOnly;
using JsonSettings = Finsa.CodeServices.Serialization.JsonSerializerSettings;
using Newtonsoft.Json.Serialization;

namespace Finsa.Caravan.Common.Models.Logging
{
    /// <summary>
    ///   Internal format used for complex log message passing.
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct LogMessage
    {
        /// <summary>
        ///   The prefix used to identify complex messages.
        /// </summary>
        public static readonly string JsonMessagePrefix = "#LOG_MESSAGE#" + Environment.NewLine;

        /// <summary>
        ///   JSON serializer settings for a readable log.
        /// </summary>
        public static JsonSettings ReadableJsonSettings { get; } = new JsonSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Newtonsoft.Json.Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

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
        ///   Arguments.
        /// </summary>
        [JsonProperty("a")]
        public IEnumerable<KeyValuePair<string, string>> Arguments { get; set; }

        /// <summary>
        ///   Exception (optional), must _not_ be serialized. In fact, its content is assigned to
        ///   above properties.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///   Returns the short message.
        /// </summary>
        public override string ToString()
        {
            return ShortMessage;
        }

        /// <summary>
        ///   Returns a complex JSON containing all log message information.
        /// </summary>
        public string ToLogString()
        {
            var clone = new LogMessage();
            if (Exception != null)
            {
                var exception = Exception;
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
                clone.ShortMessage = exception.Message;
                clone.LongMessage = exception.StackTrace;
                clone.Context = Context;
                // Keep aligned with Finsa.DataAccess.CaravanLoggerTarget.ParseMessage
                clone.Arguments = new List<KeyValuePair<string, string>>(Arguments ?? ReadOnlyList.Empty<KeyValuePair<string, string>>())
                {
                    KeyValuePair.Create("exception_data", exception.Data.ToJsonString(ReadableJsonSettings)),
                    KeyValuePair.Create("exception_source", exception.Source ?? string.Empty),
                    KeyValuePair.Create("exception_hresult", exception.HResult.ToString()),
                };
            }
            else
            {
                clone.ShortMessage = ShortMessage;
                clone.LongMessage = LongMessage;
                clone.Context = Context;
                clone.Arguments = Arguments;
            }
            return JsonMessagePrefix + clone.ToJsonString();
        }
    }
}