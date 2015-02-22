using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Finsa.Caravan.Common.Serialization.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PommaLabs;

namespace Finsa.Caravan.Common.Models.Logging
{
    [Serializable, DataContract]
    public class LogSetting : EquatableObject<LogSetting>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1), JsonConverter(typeof(StringEnumConverter))]
        public LogType LogType { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2), JsonConverter(typeof(IntToBoolConverter))]
        public int Enabled { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public int Days { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public int MaxEntries { get; set; }

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("AppName", AppName);
            yield return GKeyValuePair.Create("LogType", LogType.ToString());
            yield return GKeyValuePair.Create("Enabled", (Enabled == 1).ToString(CultureInfo.InvariantCulture));
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return LogType;
            yield return AppName;
        }
    }

    public enum LogType : byte
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Error = 3,
        Fatal = 4
    }
}