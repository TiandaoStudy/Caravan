using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Models.Logging
{
    /// <summary>
    ///   Internal format used for complex log message passing.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct LogMessage
    {
        /// <summary>
        ///   Short message.
        /// </summary>
        [JsonProperty("s")]
        public object ShortMessage { get; set; }

        /// <summary>
        ///   Long message.
        /// </summary>
        [JsonProperty("l")]
        public object LongMessage { get; set; }

        /// <summary>
        ///   Context.
        /// </summary>
        [JsonProperty("c")]
        public object Context { get; set; }

        /// <summary>
        ///   Arguments.
        /// </summary>
        [JsonProperty("a")]
        public IEnumerable<KeyValuePair<string, object>> Arguments { get; set; } 

        /// <summary>
        ///   Exception (optional), must _not_ be serialized.
        /// </summary>
        public Exception Exception { get; set; }
    }
}