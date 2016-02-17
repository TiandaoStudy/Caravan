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

        [IgnoreDataMember]
        public string PasswordHash { get; set; }

        [DataMember(Order = 3)]
        public bool Active { get; set; }

        [DataMember(Order = 4)]
        public string FirstName { get; set; }

        [DataMember(Order = 5)]
        public string LastName { get; set; }

        [DataMember(Order = 6)]
        public string Email { get; set; }

        [DataMember(Order = 7)]
        public bool EmailConfirmed { get; set; }

        [DataMember(Order = 8)]
        public string PhoneNumber { get; set; }

        [DataMember(Order = 9)]
        public bool PhoneNumberConfirmed { get; set; }

        [DataMember(Order = 10)]
        public int AccessFailedCount { get; set; }

        [DataMember(Order = 11)]
        public bool LockoutEnabled { get; set; }

        [DataMember(Order = 12)]
        public DateTime LockoutEndDate { get; set; }

        [IgnoreDataMember]
        public string SecurityStamp { get; set; }

        [DataMember(Order = 13)]
        public bool TwoFactorAuthenticationEnabled { get; set; }

        /// <summary>
        ///   Il file contenente l'immagine corrispondente all'utente. Può non essere specificato.
        /// </summary>
        [DataMember(Order = 14)]
        public byte[] AvatarFile { get; set; }

        /// <summary>
        ///   L'estensione del file contenente l'immagine corrispondente all'utente. Può non essere
        ///   specificata se il file <see cref="AvatarFile"/> non è presente; altrimenti, entrambi
        ///   sono presenti.
        ///   
        ///   La stringa è comprensiva del punto iniziale (es: ".jpg").
        /// </summary>
        [DataMember(Order = 15)]
        public string AvatarFileExtension { get; set; }

        /// <summary>
        ///   I gruppi a cui appartiene l'utente.
        /// </summary>
        [DataMember(Order = 16)]
        public IEnumerable<SecGroup> Groups { get; set; }

        /// <summary>
        ///   I ruoli ricoperti dall'utente.
        /// </summary>
        [DataMember(Order = 17)]
        public IEnumerable<SecRole> Roles { get; set; }

        /// <summary>
        ///   I "security claim" dell'utente.
        /// </summary>
        [DataMember(Order = 18)]
        public IEnumerable<SecClaim> Claims { get; set; }

        #region IUser members

        /// <summary>
        ///   Unique username.
        /// </summary>
        [IgnoreDataMember]
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
            yield return KeyValuePair.Create(nameof(IUser.UserName), FirstName + " " + LastName);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
        }

        #endregion FormattableObject members
    }
}