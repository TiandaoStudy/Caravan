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

using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.WebApi.Identity.Models
{
    /// <summary>
    ///   Rappresenta il risultato di un'operazione di autorizzazione.
    /// </summary>
    [Serializable, DataContract]
    public struct AuthorizationResult<TPayload>
    {
        /// <summary>
        ///   Indica se la richiesta di autorizzazione sia andata a buon fine o meno.
        /// </summary>
        [DataMember]
        public bool Authorized { get; set; }

        /// <summary>
        ///   Se la richiesta non è stata autorizzata, questo campo può contenere la motivazione del rifiuto.
        /// </summary>
        [DataMember]
        public string AuthorizationDeniedReason { get; set; }

        /// <summary>
        ///   Se la richiesta non è stata autorizzata, questo campo può contenere la motivazione
        ///   estesa del rifiuto.
        /// </summary>
        public Exception AuthorizationDeniedException { get; set; }

        /// <summary>
        ///   Un oggetto allegato all'esito della validazione.
        /// </summary>
        public TPayload Payload { get; set; }
    }
}
