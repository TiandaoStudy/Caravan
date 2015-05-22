using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecContext : EquatableObject<SecContext>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string Description { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public SecObject[] Objects { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create("AppName", AppName);
            yield return KeyValuePair.Create("Name", Name);
            yield return KeyValuePair.Create("Description", Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return AppName;
            yield return Name;
        }
    }
}