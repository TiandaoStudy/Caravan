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

using Finsa.Caravan.Common.Identity.Models;
using IdentityServer3.Core.Services;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Identity
{
    /// <summary>
    ///   Ridefinisce la funzione che recupera i client in modo che restituisca anche l'informazione
    ///   sul nome dell'applicativo Caravan associato.
    /// </summary>
    public interface ICaravanClientStore : IClientStore
    {
        /// <summary>
        ///   Finds a Caravan identity client by ID.
        /// </summary>
        /// <param name="clientId">The Caravan identity client ID.</param>
        /// <returns>The Caravan identity client.</returns>
        new Task<IdnClient> FindClientByIdAsync(string clientId);
    }
}
