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

using Finsa.Caravan.Common.Core;
using Finsa.Caravan.Common.Security.Models;
using PommaLabs.Thrower;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Exposes basic role management APIs.
    /// </summary>
    /// <remarks>ASP.NET Identity ROLES are mapped to Caravan security GROUPS.</remarks>
    sealed class CaravanRoleStore : ICaravanRoleStore
    {
        /// <summary>
        ///   Inizializza lo store.
        /// </summary>
        /// <param name="appName">Nome dell'applicativo Caravan.</param>
        /// <param name="securityRepository">Il repository della sicurezza di Caravan.</param>
        public CaravanRoleStore(string appName, ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            AppName = appName;
            SecurityRepository = securityRepository;
        }

        /// <summary>
        ///   Il nome dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        public string AppName { get; }

        /// <summary>
        ///   Il repository della sicurezza di Caravan.
        /// </summary>
        public ICaravanSecurityRepository SecurityRepository { get; }

        #region IQueryableRoleStore members

        /// <summary>
        ///   IQueryable roles.
        /// </summary>
        public IQueryable<SecGroup> Roles => SecurityRepository.GetGroupsAsync(AppName).Result.AsQueryable();

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting
        ///   unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Nulla, perché questo store non apre connessioni.
        }

        /// <summary>
        ///   Creates a new role.
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        public async Task CreateAsync(SecGroup role)
        {
            await SecurityRepository.AddGroupAsync(AppName, role);
        }

        /// <summary>
        ///   Updates a role.
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        public async Task UpdateAsync(SecGroup role)
        {
            await SecurityRepository.UpdateGroupByIdAsync(AppName, role.Id, new SecGroupUpdates
            {
                Name = role.Name
            });
        }

        /// <summary>
        ///   Deletes a role.
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        public async Task DeleteAsync(SecGroup role)
        {
            await SecurityRepository.RemoveGroupByIdAsync(AppName, role.Id);
        }

        /// <summary>
        ///   Finds a role by ID.
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        public async Task<SecGroup> FindByIdAsync(long roleId) => await SecurityRepository.GetGroupByIdAsync(AppName, roleId);

        /// <summary>
        ///   Finds a role by name.
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        public async Task<SecGroup> FindByNameAsync(string roleName) => await SecurityRepository.GetGroupByNameAsync(AppName, roleName);

        #endregion IQueryableRoleStore members
    }
}
