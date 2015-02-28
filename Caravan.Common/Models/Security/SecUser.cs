using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.Caravan.Common.Serialization.Converters;
using Newtonsoft.Json;
using PommaLabs;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract]
    public class SecUser : EquatableObject<SecUser>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Login { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2), JsonConverter(typeof(IntToBoolConverter))]
        public bool Active { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public string HashedPassword { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string FirstName { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public string LastName { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public string Email { get; set; }

        [JsonProperty(Order = 7), DataMember(Order = 7)]
        public SecGroup[] Groups { get; set; }

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
    }
}