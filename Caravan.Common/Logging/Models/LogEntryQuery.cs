using Common.Logging;
using Finsa.CodeServices.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Logging.Models
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public sealed class LogEntryQuery
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public IList<string> AppNames { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public IList<LogLevel> LogLevels { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public bool TruncateLongMessage { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public Option<DateTime> FromDate { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public Option<DateTime> ToDate { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public Option<string> UserLoginLike { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public Option<string> CodeUnitLike { get; set; }

        [JsonProperty(Order = 7), DataMember(Order = 7)]
        public Option<string> FunctionLike { get; set; }

        [JsonProperty(Order = 8), DataMember(Order = 8)]
        public Option<string> ShortMessageLike { get; set; }

        [JsonProperty(Order = 9), DataMember(Order = 9)]
        public Option<string> LongMessageLike { get; set; }

        [JsonProperty(Order = 10), DataMember(Order = 10)]
        public Option<string> ContextLike { get; set; }

        [JsonProperty(Order = 11), DataMember(Order = 11)]
        public int MaxTruncatedLongMessageLength { get; set; }
    }
}
