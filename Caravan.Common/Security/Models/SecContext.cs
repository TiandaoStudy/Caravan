﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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
    public class SecContext : EquatableObject<SecContext>
    {
        [DataMember(Order = 0)]
        public int Id { get; set; }

        [DataMember(Order = 1)]
        public string AppName { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string Description { get; set; }

        [DataMember(Order = 4)]
        public IEnumerable<SecObject> Objects { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(Id), Id.ToString(CultureInfo.InvariantCulture));
            yield return KeyValuePair.Create(nameof(Name), Name);
            yield return KeyValuePair.Create(nameof(AppName), AppName);
            yield return KeyValuePair.Create(nameof(Description), Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
        }
    }
}