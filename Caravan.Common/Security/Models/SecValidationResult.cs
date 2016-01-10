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
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Models
{
    /// <summary>
    ///   Rappresenta il risultato di un'operazione di validazione sulla componente di sicurezza di Caravan.
    /// </summary>
    [Serializable, DataContract]
    public sealed class SecValidationResult
    {
        /// <summary>
        ///   Oggetto statico che rappresenta l'assenza di errori.
        /// </summary>
        private static readonly string[] NoErrors = new string[0];

        /// <summary>
        ///   Oggetto statico che rappresenta l'assenza di errori.
        /// </summary>
        public static SecValidationResult Success { get; } = new SecValidationResult
        {
            Succeeded = true,
            Errors = NoErrors
        };

        /// <summary>
        ///   Il risultato complessivo dell'operazione di validazione.
        /// </summary>
        [DataMember(Order = 0)]
        public bool Succeeded { get; private set; }

        /// <summary>
        ///   Eventuali errori rinvenuti durante la validazione.
        /// </summary>
        [DataMember(Order = 1)]
        public IList<string> Errors { get; private set; }

        /// <summary>
        ///   Genera un risultato negativo con l'errore indicato.
        /// </summary>
        /// <param name="error">L'errore rinvenuto.</param>
        /// <returns>Un risultato negativo con l'errore indicato.</returns>
        public static SecValidationResult Failure(string error) => new SecValidationResult
        {
            Succeeded = false,
            Errors = GTuple.Create(error)
        };

        /// <summary>
        ///   Genera un risultato negativo con gli errori indicati.
        /// </summary>
        /// <param name="errors">Gli errori rinvenuti.</param>
        /// <returns>Un risultato negativo con gli errori indicati.</returns>
        public static SecValidationResult Failure(IList<string> errors) => new SecValidationResult
        {
            Succeeded = false,
            Errors = errors
        };
    }
}
