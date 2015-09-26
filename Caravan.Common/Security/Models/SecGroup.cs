using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecGroup : EquatableObject<SecGroup>, IRole<string>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string Description { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public string Notes { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public SecUser[] Users { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public SecRole[] Roles { get; set; }

        #region IRole members

        public string Id { get; private set; }

        #endregion

        #region FormattableObject members

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(AppName), AppName);
            yield return KeyValuePair.Create(nameof(Name), Name);
            yield return KeyValuePair.Create(nameof(Description), Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Name;
            yield return AppName;
        }

        #endregion
    }
}