using Common.Logging;
using Finsa.Caravan.Common.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Logging
{
    [Serializable, DataContract]
    public class LogSetting : EquatableObject<LogSetting>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1), JsonConverter(typeof(StringEnumConverter))]
        public LogLevel LogLevel { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public bool Enabled { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public short Days { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public int MaxEntries { get; set; }

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("AppName", AppName);
            yield return GKeyValuePair.Create("LogLevel", LogLevel.ToString());
            yield return GKeyValuePair.Create("Enabled", Enabled.ToString(CultureInfo.InvariantCulture));
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return LogLevel;
            yield return AppName;
        }
    }
}