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

using IdentityServer3.Core.Models;
using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Identity.Models
{
    /// <summary>
    ///   Ridefinisce il client in modo che presenti anche l'informazione sul nome dell'applicativo
    ///   Caravan associato.
    /// </summary>
    [Serializable, DataContract]
    public class IdnClient : Client
    {
        /// <summary>
        ///   Il nome dell'applicativo Caravan a cui appartiene il client.
        /// </summary>
        public string AppName { get; set; }
    }
}
