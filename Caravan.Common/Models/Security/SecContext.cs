using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PommaLabs;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract]
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

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("AppName", AppName);
            yield return GKeyValuePair.Create("Name", Name);
            yield return GKeyValuePair.Create("Description", Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return AppName;
            yield return Name;
        }
    }
}