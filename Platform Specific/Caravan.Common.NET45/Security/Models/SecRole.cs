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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Finsa.Caravan.Common.Core;
using Microsoft.AspNet.Identity;
using PommaLabs.Thrower;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, DataContract]
    public class SecRole : EquatableObject<SecRole>, IRole<int>
    {
        /// <summary>
        ///   ID of the role.
        /// </summary>
        [DataMember(Order = 0)]
        public int Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string AppName { get; set; }

        [DataMember(Order = 3)]
        public string GroupName { get; set; }

        [DataMember(Order = 4)]
        public string Description { get; set; }

        [DataMember(Order = 5)]
        public string Notes { get; set; }

        #region ASP.NET Identity

        /// <summary>
        ///   Name of the role.
        /// </summary>
        string IRole<int>.Name
        {
            get { return ToIdentityRoleName(GroupName, Name); }
            set
            {
                var tuple = FromIdentityRoleName(value);
                GroupName = tuple.Item1;
                Name = tuple.Item2;
            }
        }

        /// <summary>
        ///   Aggrega gruppo e ruolo in una stringa unica che rappresenta il ruolo ASP.NET.
        /// </summary>
        /// <param name="groupName">Nome del gruppo.</param>
        /// <param name="roleName">Nome del ruolo.</param>
        /// <returns>Gruppo e ruolo aggregati in una stringa unica che rappresenta il ruolo ASP.NET.</returns>
        public static string ToIdentityRoleName(string groupName, string roleName)
        {
            RaiseArgumentException.If(groupName.Contains("/"), ErrorMessages.InvalidGroupName, nameof(groupName));
            RaiseArgumentException.If(roleName.Contains("/"), ErrorMessages.InvalidRoleName, nameof(roleName));
            return $"{groupName}/{roleName}";
        }

        /// <summary>
        ///   Decodifica gruppo e ruolo dalla stringa di ruolo ASP.NET.
        /// </summary>
        /// <param name="identityRoleName">L'aggregazione di gruppo e ruolo.</param>
        /// <returns>Una tupla con il nome del gruppo e il nome del ruolo.</returns>
        public static GTuple2<string> FromIdentityRoleName(string identityRoleName)
        {
            RaiseArgumentNullException.IfIsNull(identityRoleName, nameof(identityRoleName));
            var split = identityRoleName.Split('/');
            return GTuple.Create<string>(split[0], split[1]);
        }

        #endregion

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(Id), Id.ToString(CultureInfo.InvariantCulture));
            yield return KeyValuePair.Create(nameof(Name), Name);
            yield return KeyValuePair.Create(nameof(AppName), AppName);
            yield return KeyValuePair.Create(nameof(GroupName), GroupName);
            yield return KeyValuePair.Create(nameof(Description), Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
        }
    }
}
