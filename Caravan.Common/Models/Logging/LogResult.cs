using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Logging
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public struct LogResult
    {
        public static readonly LogResult Success = new LogResult { Succeeded = true };

        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public bool Succeeded { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public Exception Exception { get; set; }

        public static LogResult Failure(string cause)
        {
            return new LogResult { Succeeded = false, Exception = new Exception(cause) };
        }

        public static LogResult Failure(Exception ex)
        {
            return new LogResult { Succeeded = false, Exception = ex };
        }
    }
}