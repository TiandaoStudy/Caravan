using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.Caravan.Common.DataModel.Logging;
using Newtonsoft.Json;
using PommaLabs;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract(IsReference = true)]
    public class SecApp : EquatableObject<SecApp>
    {
        [JsonProperty, DataMember]
        public long Id { get; set; }

        [JsonProperty, DataMember]
        public string Name { get; set; }

        [JsonProperty, DataMember]
        public string Description { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public virtual ICollection<SecUser> Users { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public virtual ICollection<SecGroup> Groups { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public virtual ICollection<SecContext> Contexts { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public virtual ICollection<SecObject> Objects { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public virtual ICollection<LogSetting> LogSettings { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public virtual ICollection<LogEntry> LogEntries { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public virtual ICollection<SecEntry> SecEntries { get; set; }

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("Name", Name);
            yield return GKeyValuePair.Create("Description", Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Name;
        }
    }
}