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
using Microsoft.AspNet.Identity;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Exposes user related APIs which will automatically save changes to the RoleStore.
    /// </summary>
    public sealed class CaravanGroupManager : RoleManager<SecGroup, long>
    {
        /// <summary>
        ///   Inizializza il gestore personalizzato.
        /// </summary>
        /// <param name="groupStore">Lo store per i gruppi.</param>
        public CaravanGroupManager(ICaravanGroupStore groupStore)
            : base(groupStore)
        {
        }
    }
}
