using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PommaLabs;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract(IsReference = true)]
    public class SecGroup : EquatableObject<SecGroup>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public long Id { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public long AppId { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public SecApp App { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string Description { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public int IsAdmin { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string Notes { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public virtual ICollection<SecUser> Users { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public virtual ICollection<SecEntry> SecEntries { get; set; }

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("Name", Name);
            yield return GKeyValuePair.Create("Description", Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return App.Name;
            yield return Name;
        }
    }
}