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
using System.Net;
using System.Runtime.Serialization;

namespace Finsa.Caravan.WebApi.Filters.Models
{
    /// <summary>
    ///   Modella la risposta di errore restituita dai servizi.
    /// </summary>
    [Serializable, DataContract]
    public sealed class HttpExceptionResponse
    {
        /// <summary>
        ///   Codice di errore della response, replicato qui per facilità di lettura.
        /// </summary>
        [DataMember]
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        ///   Il messaggio di errore destinato allo sviluppatore.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        ///   Il messaggio di errore destinato all'utente del servizio o dell'applicativo.
        /// </summary>
        [DataMember]
        public string UserMessage { get; set; }

        /// <summary>
        ///   Un codice di errore gestito convenzionalmente dall'applicativo.
        /// </summary>
        [DataMember]
        public object ErrorCode { get; set; }
    }
}
