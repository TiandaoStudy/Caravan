using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Collections.ReadOnly;
using Finsa.CodeServices.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using JsonSettings = Finsa.CodeServices.Serialization.JsonSerializerSettings;

namespace Finsa.Caravan.Common.Logging.Models
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
        public static readonly string JsonMessagePrefix = "#LOG_MESSAGE#";

        /// <summary>
        ///   JSON serializer settings for a readable log.
        /// </summary>
        public static JsonSettings ReadableJsonSettings { get; } = new JsonSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        /// <summary>
        ///   Short message.
        /// </summary>
        [JsonProperty("shortMessage")]
        public string ShortMessage { get; set; }

        /// <summary>
        ///   Long message.
        /// </summary>
        [JsonProperty("longMessage")]
        public string LongMessage { get; set; }

        /// <summary>
        ///   Context.
        /// </summary>
        [JsonProperty("context")]
        public string Context { get; set; }

        /// <summary>
        ///   Arguments.
        /// </summary>
        [JsonProperty("arguments")]
        public IEnumerable<KeyValuePair<string, string>> Arguments { get; set; }

        /// <summary>
        ///   Exception (optional), must _not_ be serialized. In fact, its content is assigned to
        ///   above properties.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///   Returns a complex JSON containing all log message information.
        /// </summary>
        public override string ToString()
        {
            var clone = new LogMessage();
            if (Exception != null)
            {
                var exception = Exception.GetBaseException();
                clone.ShortMessage = exception.Message;
                clone.LongMessage = exception.ToJsonString(ReadableJsonSettings);
                clone.Context = Context;
                // Keep aligned with Finsa.DataAccess.CaravanLoggerTarget.ParseMessage
                clone.Arguments = new List<KeyValuePair<string, string>>(Arguments ?? ReadOnlyList.Empty<KeyValuePair<string, string>>())
                {
                    KeyValuePair.Create("exception_stacktrace", exception.StackTrace ?? string.Empty),
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
            // Aggiungo NewLine così che nei file di testo parta da una riga sotto, dato che il
            // messaggio JSON è sicuramente molto lungo.
            return Environment.NewLine + clone.ToJsonString(ReadableJsonSettings);
        }
    }
}
