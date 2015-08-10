using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecEntry : EquatableObject<SecEntry>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public long Id { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string ContextName { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public string ObjectName { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string UserLogin { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public string GroupName { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public string RoleName { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create("AppName", AppName);
            yield return KeyValuePair.Create("Id", Id.ToString(CultureInfo.InvariantCulture));
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
            yield return AppName;
        }
    }
}