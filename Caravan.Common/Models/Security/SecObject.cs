using Finsa.Caravan.Common.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract]
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

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("AppName", AppName);
            yield return GKeyValuePair.Create("ContextName", ContextName);
            yield return GKeyValuePair.Create("Name", Name);
            yield return GKeyValuePair.Create("Type", Type);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Name;
            yield return ContextName;
            yield return AppName;
        }
    }
}