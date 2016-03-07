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
using System.Web;

namespace Finsa.Caravan.Common.Security.Models
{
    [Serializable, DataContract]
    public class SecSession : FormattableObject
    {
        [DataMember(Order = 0)]
        public string UserLogin { get; set; }

        [DataMember(Order = 1)]
        public string UserHostAddress { get; private set; }

        [DataMember(Order = 2)]
        public string UserHostName { get; private set; }

        [DataMember(Order = 3)]
        public DateTime LastVisit { get; set; }

        public void FillData()
        {
            if (HttpContext.Current == null)
            {
                UserHostAddress = UserHostName = "unknown";
            }
            else
            {
                UserHostAddress = HttpContext.Current.Request.UserHostAddress == "::1" ? "localhost" : HttpContext.Current.Request.UserHostAddress;
                UserHostName = HttpContext.Current.Request.UserHostName == "::1" ? "localhost" : HttpContext.Current.Request.UserHostAddress;
            }
            LastVisit = CaravanServiceProvider.Clock.UtcNow;
        }

        #region FormattableObject members

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(UserLogin), UserLogin);
            yield return KeyValuePair.Create(nameof(LastVisit), LastVisit.ToString(CultureInfo.InvariantCulture));
        }

        #endregion FormattableObject members
    }
}
