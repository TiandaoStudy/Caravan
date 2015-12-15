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

using System.Net;

namespace Finsa.Caravan.WebApi.Models
{
    /// <summary>
    ///   Modella la risposta di errore restituita dai servizi.
    /// </summary>
    public sealed class HttpExceptionResponse
    {
        /// <summary>
        ///   Codice di errore della response, replicato qui per facilità di lettura.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        ///   Il messaggio di errore destinato allo sviluppatore.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///   Il messaggio di errore destinato all'utente del servizio o dell'applicativo.
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        ///   Un codice di errore gestito convenzionalmente dall'applicativo.
        /// </summary>
        public object ErrorCode { get; set; }
    }
}
