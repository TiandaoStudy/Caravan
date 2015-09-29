using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Collections.ReadOnly;
using Finsa.CodeServices.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using JsonSerializerSettings = Finsa.CodeServices.Serialization.JsonSerializerSettings;

namespace Finsa.Caravan.Common.Logging.Models
{
    /// <summary>
    ///   Internal format used for complex log message passing.
    /// </summary>
    [Serializable]
    public sealed class LogMessage
    {
        /// <summary>
        ///   JSON serializer settings for a readable log.
        /// </summary>
        public static JsonSerializerSettings ReadableJsonSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Error = IgnoreJsonSerializationError,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        /// <summary>
        ///   YAML serializer settings for a readable log.
        /// </summary>
        public static YamlSerializerSettings ReadableYamlSettings { get; } = new YamlSerializerSettings
        {
            NamingConvention = new CamelCaseNamingConvention(),
            SerializationOptions = SerializationOptions.EmitDefaults
        };

        /// <summary>
        ///   Short message.
        /// </summary>
        [JsonProperty(Order = 0), YamlMember(Order = 0)]
        public string ShortMessage { get; set; }

        /// <summary>
        ///   Long message.
        /// </summary>
        [JsonProperty(Order = 1), YamlMember(Order = 1)]
        public string LongMessage { get; set; }

        /// <summary>
        ///   Context.
        /// </summary>
        [JsonProperty(Order = 2), YamlMember(Order = 2)]
        public string Context { get; set; }

        /// <summary>
        ///   Arguments.
        /// </summary>
        [JsonProperty(Order = 3), YamlMember(Order = 3)]
        public IList<KeyValuePair<string, object>> Arguments { get; set; }

        /// <summary>
        ///   Exception (optional), must _not_ be serialized. In fact, its content is assigned to
        ///   above properties.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public Exception Exception
        {
            set
            {
                if (value == null)
                {
                    // Nulla da fare...
                    return;
                }

                var exception = value.GetBaseException();

                var shortMsgPrefix = ((ShortMessage != null) ? ShortMessage + " - " : string.Empty);
                ShortMessage = shortMsgPrefix + exception.Message;

                var longMsgPrefix = ((LongMessage != null) ? LongMessage + Environment.NewLine + Environment.NewLine : string.Empty);
                LongMessage = longMsgPrefix + exception.ToJsonString(ReadableJsonSettings);

                // Prendo anche eventuali argomenti già inseriti.
                Arguments = new List<KeyValuePair<string, object>>(Arguments ?? ReadOnlyList.Empty<KeyValuePair<string, object>>())
                {
                    KeyValuePair.Create<string, object>("exception_stacktrace", exception.StackTrace),
                    KeyValuePair.Create<string, object>("exception_data", exception.Data.ToYamlString(ReadableYamlSettings)),
                    KeyValuePair.Create<string, object>("exception_source", exception.Source),
                    KeyValuePair.Create<string, object>("exception_hresult", exception.HResult),
                };
            }
        }

        /// <summary>
        ///   Simply returns the short message.
        /// </summary>
        public override string ToString() => ShortMessage;

        private static void IgnoreJsonSerializationError(object sender, ErrorEventArgs errorArgs)
        {
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
