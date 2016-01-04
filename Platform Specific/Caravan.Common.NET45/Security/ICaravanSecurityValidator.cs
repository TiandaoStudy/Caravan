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

using Finsa.Caravan.Common.Security.Models;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Interfaccia che si occupa della validazione di indirizzi email, numeri di telefono e altro.
    /// </summary>
    public interface ICaravanSecurityValidator
    {
        /// <summary>
        ///   Valida parte delle proprietà dell'utente passato come parametro, utilizzando i metodi definiti da questa interfaccia.
        /// </summary>
        /// <param name="user">L'utente da validare.</param>
        /// <returns>Il risultato della validazione.</returns>
        Task<SecValidationResult> ValidateUserAsync(SecUser user);

        /// <summary>
        ///   Valida parte delle proprietà dell'utente passato come parametro, utilizzando i metodi definiti da questa interfaccia.
        /// </summary>
        /// <param name="userUpdates">L'utente da validare.</param>
        /// <returns>Il risultato della validazione.</returns>
        Task<SecValidationResult> ValidateUserAsync(SecUserUpdates userUpdates);

        /// <summary>
        ///   Valida l'indirizzo email passato come parametro.
        /// </summary>
        /// <param name="email">L'indirizzo email da validare.</param>
        /// <returns>Il risultato della validazione.</returns>
        Task<SecValidationResult> ValidateEmailAsync(string email);

        /// <summary>
        ///   Valida il numero di telefono passato come parametro.
        /// </summary>
        /// <param name="phoneNumber">Il numero di telefono da validare.</param>
        /// <returns>Il risultato della validazione.</returns>
        Task<SecValidationResult> ValidatePhoneNumberAsync(string phoneNumber);
    }
}
