using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.Common.Models.Logging
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class LogEntry : EquatableObject<LogEntry>
    {
        public const string AutoFilled = "Automatically filled parameter";
        public const string NotSpecified = "...";

        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public long Id { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2), JsonConverter(typeof(StringEnumConverter))]
        public LogLevel LogLevel { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public DateTime Date { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string UserLogin { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public string CodeUnit { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public string Function { get; set; }

        [JsonProperty(Order = 7), DataMember(Order = 7)]
        public string ShortMessage { get; set; }

        [JsonProperty(Order = 8), DataMember(Order = 8)]
        public string LongMessage { get; set; }

        [JsonProperty(Order = 9), DataMember(Order = 9)]
        public string Context { get; set; }

        [JsonProperty(Order = 10), DataMember(Order = 10)]
        public IList<KeyValuePair<string, string>> Arguments { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create("AppName", AppName);
            yield return KeyValuePair.Create("Id", Id.ToString(CultureInfo.InvariantCulture));
            yield return KeyValuePair.Create("LogLevel", LogLevel.ToString());
            yield return KeyValuePair.Create("Date", Date.ToString(CultureInfo.InvariantCulture));
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
            yield return AppName;
        }
    }
}