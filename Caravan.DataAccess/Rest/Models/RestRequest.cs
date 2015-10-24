using Newtonsoft.Json;
using System;

namespace Finsa.Caravan.DataAccess.Rest.Models
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