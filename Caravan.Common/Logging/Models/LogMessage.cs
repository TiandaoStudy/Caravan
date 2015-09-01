using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Collections.ReadOnly;
using Finsa.CodeServices.Serialization;
using Newtonsoft.Json;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
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
        static readonly Regex RemoveBlankRows = new Regex(@"^[ \t]*((\r|\n)\n?)", RegexOptions.Compiled | RegexOptions.Multiline);
        static readonly Regex RemoveLastBlanks = new Regex(@"[ \t]+((\r|\n)\n?)", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        ///   JSON serializer settings for a readable log.
        /// </summary>
        public static JsonSerializerSettings ReadableJsonSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
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
        public IList<KeyValuePair<string, string>> Arguments { get; set; }

        /// <summary>
        ///   Exception (optional), must _not_ be serialized. In fact, its content is assigned to
        ///   above properties.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public Exception Exception
        {
            set
            {
                // Preconditions
                RaiseArgumentNullException.IfIsNull(value, nameof(value));

                var exception = value.GetBaseException();

                var shortMsgPrefix = ((ShortMessage != null) ? ShortMessage + " - " : string.Empty);
                ShortMessage = shortMsgPrefix + exception.Message;

                var longMsgPrefix = ((LongMessage != null) ? LongMessage + Environment.NewLine + Environment.NewLine : string.Empty);
                LongMessage = longMsgPrefix + exception.ToJsonString(ReadableJsonSettings);

                // Prendo anche eventuali argomenti già inseriti.
                Arguments = new List<KeyValuePair<string, string>>(Arguments ?? ReadOnlyList.Empty<KeyValuePair<string, string>>())
                {
                    KeyValuePair.Create("exception_stacktrace", exception.StackTrace ?? string.Empty),
                    KeyValuePair.Create("exception_data", exception.Data.ToYamlString(ReadableYamlSettings)),
                    KeyValuePair.Create("exception_source", exception.Source ?? string.Empty),
                    KeyValuePair.Create("exception_hresult", exception.HResult.ToString()),
                };
            }
        }

        /// <summary>
        ///   Returns a complex JSON containing all log message information.
        /// </summary>
        public override string ToString()
        {
            // Pulizia dell'oggetto .NET, in modo da avere un file YAML molto pulito. Per
            // ShortMessage e Context mi limito a una TRIM (è difficile che siano su più righe),
            // mentre per LongMessage e per gli Arguments devo applicare pulizie più efficaci.
            ShortMessage = ShortMessage?.Trim();
            Context = Context?.Trim();
            if (LongMessage != null)
            {
                LongMessage = RemoveBlankRows.Replace(LongMessage, string.Empty);
                LongMessage = RemoveLastBlanks.Replace(LongMessage, Environment.NewLine);
            }
            if (Arguments != null)
            {
                for (var i = 0; i < Arguments.Count; ++i)
                {
                    var kv = Arguments[i];
                    if (kv.Value == null)
                    {
                        continue;
                    }
                    var tmp = RemoveBlankRows.Replace(kv.Value, string.Empty);
                    tmp = RemoveLastBlanks.Replace(tmp, Environment.NewLine);
                    Arguments[i] = KeyValuePair.Create(kv.Key.Trim(), tmp);
                }
            }

            // Generazione della serializzazione YAML.
            var yaml = this.ToYamlString(ReadableYamlSettings);
            yaml = RemoveBlankRows.Replace(yaml, string.Empty);

            // Aggiungo NewLine così che nei file di testo parta da una riga sotto, dato che il
            // messaggio YAML è sicuramente molto lungo.
            return Environment.NewLine + yaml;
        }
    }
}
