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

using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.WebApi.Identity.Models
{
    /// <summary>
    ///   I tipi di errore riscontrabili durante la validazione dell'accesso.
    /// </summary>
    [Serializable, DataContract(Name = nameof(AuthorizationErrorContext))]
    public enum AuthorizationErrorContext
    {
        /// <summary>
        ///   L'access token non è presente nella richiesta HTTP.
        /// </summary>
        [EnumMember]
        MissingAccessToken,

        /// <summary>
        ///   L'access token presente nella richiesta HTTP non è valido per qualche motivo.
        /// </summary>
        [EnumMember]
        InvalidAccessToken,

        /// <summary>
        ///   La richiesta HTTP è stata reputata non valida per un qualche motivo.
        /// </summary>
        [EnumMember]
        InvalidRequest
    }
}
