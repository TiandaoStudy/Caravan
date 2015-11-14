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
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Models
{
    /// <summary>
    ///   Rappresenta le modifiche che possono essere effettuate a una entità di
    ///   <see cref="SecUser"/>. Le due classi vanno tenute sincronizzate.
    /// </summary>
    [Serializable, DataContract]
    public class SecUserUpdates
    {
        [DataMember(Order = 0)]
        public Option<string> Login { get; set; }

        [DataMember(Order = 1)]
        public Option<string> PasswordHash { get; set; }

        [DataMember(Order = 2)]
        public Option<bool> Active { get; set; }

        [DataMember(Order = 3)]
        public Option<string> FirstName { get; set; }

        [DataMember(Order = 4)]
        public Option<string> LastName { get; set; }

        [DataMember(Order = 5)]
        public Option<string> Email { get; set; }

        [DataMember(Order = 6)]
        public Option<bool> EmailConfirmed { get; set; }

        [DataMember(Order = 7)]
        public Option<string> PhoneNumber { get; set; }

        [DataMember(Order = 8)]
        public Option<bool> PhoneNumberConfirmed { get; set; }
    }
}
