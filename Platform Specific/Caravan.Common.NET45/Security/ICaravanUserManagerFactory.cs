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

using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Gestisce la creazione di UserManager specifici per un dato applicativo Caravan.
    /// </summary>
    public interface ICaravanUserManagerFactory
    {
        /// <summary>
        ///   Il repository della sicurezza di Caravan.
        /// </summary>
        ICaravanSecurityRepository SecurityRepository { get; }

        /// <summary>
        ///   Restituisce uno UserManager specifico per l'applicativo Caravan corrente.
        /// </summary>
        /// <returns>Uno UserManager specifico per l'applicativo Caravan corrente.</returns>
        Task<CaravanUserManager> CreateAsync();

        /// <summary>
        ///   Restituisce uno UserManager specifico per un dato applicativo Caravan.
        /// </summary>
        /// <param name="appName">Il nome dell'applicativo Caravan.</param>
        /// <returns>Uno UserManager specifico per un dato applicativo Caravan.</returns>
        Task<CaravanUserManager> CreateAsync(string appName);
    }
}