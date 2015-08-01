using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Models.Logging
{
    /// <summary>
    ///   Internal format used for complex log message passing.
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct LogMessage
    {
        /// <summary>
        ///   Short message.
        /// </summary>
        [JsonProperty("s")]
        public string ShortMessage { get; set; }

        /// <summary>
        ///   Long message.
        /// </summary>
        [JsonProperty("l")]
        public string LongMessage { get; set; }

        /// <summary>
        ///   Context.
        /// </summary>
        [JsonProperty("c")]
        public string Context { get; set; }

        /// <summary>
        ///   Arguments.
        /// </summary>
        [JsonProperty("a")]
        public IEnumerable<KeyValuePair<string, string>> Arguments { get; set; }

        /// <summary>
        ///   Exception (optional), must _not_ be serialized. In fact, its content is assigned to
        ///   above properties.
        /// </summary>
        public Exception Exception { get; set; }
    }
}