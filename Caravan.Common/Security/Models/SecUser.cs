// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecUser : EquatableObject<SecUser>, IUser<string>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string AppName { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Login { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string PasswordHash { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public bool Active { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string FirstName { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public string LastName { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public string Email { get; set; }

        [JsonProperty(Order = 7), DataMember(Order = 7)]
        public bool EmailConfirmed { get; set; }

        [JsonProperty(Order = 8), DataMember(Order = 8)]
        public string PhoneNumber { get; set; }

        [JsonProperty(Order = 9), DataMember(Order = 9)]
        public bool PhoneNumberConfirmed { get; set; }

        [JsonProperty(Order = 10), DataMember(Order = 10)]
        public SecGroup[] Groups { get; set; }

        [JsonProperty(Order = 11), DataMember(Order = 11)]
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
                FirstName = (parts.Length >= 1) ? parts[0] : string.Empty;
                LastName = (parts.Length >= 2) ? parts[0] : string.Empty;
            }
        }

        #endregion IUser members

        #region FormattableObject members

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(AppName), AppName);
            yield return KeyValuePair.Create(nameof(Login), Login);
            yield return KeyValuePair.Create(nameof(UserName), UserName);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return AppName;
            yield return Login;
        }

        #endregion FormattableObject members
    }
}