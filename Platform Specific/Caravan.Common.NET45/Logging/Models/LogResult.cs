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

namespace Finsa.Caravan.Common.Logging.Models
{
    /// <summary>
    ///   Rappresenta il risultato di un'operazione di log.
    /// </summary>
    [Serializable, DataContract]
    public struct LogResult
    {
        /// <summary>
        ///   Oggetto statico che rappresenta l'assenza di errori.
        /// </summary>
        public static LogResult Success { get; } = new LogResult
        {
            Succeeded = true
        };

        /// <summary>
        ///   Il risultato complessivo dell'operazione di log.
        /// </summary>
        [DataMember(Order = 0)]
        public bool Succeeded { get; set; }

        /// <summary>
        ///   Eventuale eccezione emersa durante l'operazione di log.
        /// </summary>
        [DataMember(Order = 1)]
        public Exception Exception { get; set; }

        /// <summary>
        ///   Genera un risultato negativo con l'errore indicato.
        /// </summary>
        /// <param name="cause">L'errore.</param>
        /// <returns>Un risultato negativo con l'errore indicato.</returns>
        public static LogResult Failure(string cause) => new LogResult
        {
            Succeeded = false,
            Exception = new Exception(cause)
        };

        /// <summary>
        ///   Genera un risultato negativo con l'eccezione indicata.
        /// </summary>
        /// <param name="exception">L'eccezione.</param>
        /// <returns>Un risultato negativo con l'eccezione indicata.</returns>
        public static LogResult Failure(Exception exception) => new LogResult
        {
            Succeeded = false,
            Exception = exception
        };
    }
}
