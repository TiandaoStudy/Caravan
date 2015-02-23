using System;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Models.Rest
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class RestRequest<TBody>
    {
        [JsonProperty(Order = 0)]
        public dynamic Auth { get; set; }

        [JsonProperty(Order = 1)]
        public TBody Body { get; set; }
    }
}