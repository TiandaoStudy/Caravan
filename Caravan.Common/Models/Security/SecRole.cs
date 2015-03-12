using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PommaLabs;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract]
    public class SecRole : EquatableObject<SecRole>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string GroupName { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string Name { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public string Description { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string Notes { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public SecUser[] Users { get; set; }

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("AppName", AppName);
            yield return GKeyValuePair.Create("GroupName", GroupName);
            yield return GKeyValuePair.Create("Name", Name);
            yield return GKeyValuePair.Create("Description", Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Name;
            yield return GroupName;
            yield return AppName;
        }
    }
}