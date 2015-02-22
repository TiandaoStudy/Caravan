using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.Caravan.Common.Models.Logging;
using Newtonsoft.Json;
using PommaLabs;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract(IsReference = true)]
    public class SecApp : EquatableObject<SecApp>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string Name { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Description { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public SecUser[] Users { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public SecGroup[] Groups { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public SecContext[] Contexts { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public LogSetting[] LogSettings { get; set; }

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