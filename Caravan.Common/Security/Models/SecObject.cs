using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecObject : EquatableObject<SecObject>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string ContextName { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string Name { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public string Description { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string Type { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(AppName), AppName);
            yield return KeyValuePair.Create(nameof(ContextName), ContextName);
            yield return KeyValuePair.Create(nameof(Name), Name);
            yield return KeyValuePair.Create(nameof(Type), Type);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Name;
            yield return ContextName;
            yield return AppName;
        }
    }
}