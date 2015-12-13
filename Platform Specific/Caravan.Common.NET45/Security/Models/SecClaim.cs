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

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, DataContract]
    public class SecClaim : EquatableObject<SecClaim>
    {
        [DataMember(Order = 0)]
        public long Id { get; set; }

        [DataMember(Order = 2)]
        public string UserLogin { get; set; }

        [DataMember(Order = 3)]
        public string Hash { get; set; }

        [DataMember(Order = 4)]
        public string Claim { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(Id), Id.ToString(CultureInfo.InvariantCulture));
            yield return KeyValuePair.Create(nameof(UserLogin), UserLogin);
            yield return KeyValuePair.Create(nameof(Hash), Hash);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
        }
    }
}
