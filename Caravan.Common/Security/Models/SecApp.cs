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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.CodeServices.Common;
using Finsa.Caravan.Common.Logging.Models;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecApp : EquatableObject<SecApp>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string Name { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Description { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public SecUser[] Users { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public SecGroup[] Groups { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public SecContext[] Contexts { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public LogSetting[] LogSettings { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(Name), Name);
            yield return KeyValuePair.Create(nameof(Description), Description);
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Name;
        }
    }
}