using Finsa.CodeServices.Common;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecGroupUpdates
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public Option<string> Name { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public Option<string> Description { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public Option<string> Notes { get; set; }
    }
}