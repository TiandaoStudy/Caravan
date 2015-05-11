using Finsa.Caravan.Common.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecUser : EquatableObject<SecUser>, IUser<string>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Login { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string HashedPassword { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public bool Active { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string FirstName { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public string LastName { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public string Email { get; set; }

        [JsonProperty(Order = 7), DataMember(Order = 7)]
        public string PhoneNumber { get; set; }

        [JsonProperty(Order = 8), DataMember(Order = 8)]
        public SecGroup[] Groups { get; set; }

        [JsonProperty(Order = 9), DataMember(Order = 9)]
        public SecRole[] Roles { get; set; }

        #region IUser members

        public string Id
        {
            get { return Login; }
        }

        public string UserName
        {
            get { return FirstName + " " + LastName; }
            set
            {
                var parts = value.Split(new[] { ' ' }, 2);
                FirstName = (parts.Length >= 1) ? parts[0] : String.Empty;
                LastName = (parts.Length >= 2) ? parts[0] : String.Empty;
            }
        }

        #endregion IUser members

        #region FormattableObject members

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("AppName", AppName);
            yield return GKeyValuePair.Create("Login", Login);
            yield return GKeyValuePair.Create("FullName", FirstName + " " + LastName);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return AppName;
            yield return Login;
        }

        #endregion FormattableObject members
    }
}