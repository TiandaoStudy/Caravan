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

using Finsa.CodeServices.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, DataContract]
    public class SecUser : EquatableObject<SecUser>, IUser<long>
    {
        /// <summary>
        ///   Unique key for the user.
        /// </summary>
        [DataMember(Order = 0)]
        public long Id { get; set; }

        [DataMember(Order = 1)]
        public string AppName { get; set; }

        [DataMember(Order = 2)]
        public string Login { get; set; }

        [DataMember(Order = 3)]
        public string PasswordHash { get; set; }

        [DataMember(Order = 4)]
        public bool Active { get; set; }

        [DataMember(Order = 5)]
        public string FirstName { get; set; }

        [DataMember(Order = 6)]
        public string LastName { get; set; }

        [DataMember(Order = 7)]
        public string Email { get; set; }

        [DataMember(Order = 8)]
        public bool EmailConfirmed { get; set; }

        [DataMember(Order = 9)]
        public string PhoneNumber { get; set; }

        [DataMember(Order = 10)]
        public bool PhoneNumberConfirmed { get; set; }

        [DataMember(Order = 11)]
        public int AccessFailedCount { get; set; }

        [DataMember(Order = 12)]
        public bool LockoutEnabled { get; set; }

        [DataMember(Order = 13)]
        public DateTime LockoutEndDate { get; set; }

        [IgnoreDataMember]
        public string SecurityStamp { get; set; }

        [DataMember(Order = 14)]
        public bool TwoFactorAuthenticationEnabled { get; set; }

        [DataMember(Order = 15)]
        public IEnumerable<SecGroup> Groups { get; set; }

        [DataMember(Order = 16)]
        public IEnumerable<SecRole> Roles { get; set; }

        [DataMember(Order = 17)]
        public IEnumerable<SecClaim> Claims { get; set; }

        #region IUser members

        /// <summary>
        ///   Unique username.
        /// </summary>
        public string UserName
        {
            get { return Login; }
            set { Login = value; }
        }

        #endregion IUser members

        #region FormattableObject members

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(Id), Id.ToString(CultureInfo.InvariantCulture));
            yield return KeyValuePair.Create(nameof(Login), Login);
            yield return KeyValuePair.Create(nameof(AppName), AppName);
            yield return KeyValuePair.Create(nameof(UserName), UserName);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
        }

        #endregion FormattableObject members
    }
}
