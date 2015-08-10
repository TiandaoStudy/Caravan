﻿using Common.Logging;
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

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create("AppName", AppName);
            yield return KeyValuePair.Create("LogLevel", LogLevel.ToString());
            yield return KeyValuePair.Create("Enabled", Enabled.ToString(CultureInfo.InvariantCulture));
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return LogLevel;
            yield return AppName;
        }
    }
}