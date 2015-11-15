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
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using PommaLabs.Thrower;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Exposes basic role management APIs.
    /// </summary>
    /// <remarks>ASP.NET Identity ROLES are mapped to Caravan security GROUPS and ROLES.</remarks>
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
        public IQueryable<SecRole> Roles => SecurityRepository.GetRolesAsync(AppName).Result.AsQueryable();

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
        public async Task CreateAsync(SecRole role)
        {
            var tuple = SecRole.FromIdentityRoleName(role);
            await SecurityRepository.AddRoleAsync(AppName, tuple.Item1, role);
        }

        /// <summary>
        ///   Updates a role.
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        public async Task UpdateAsync(SecRole role)
        {
            var tuple = SecRole.FromIdentityRoleName(role);
            await SecurityRepository.UpdateRoleAsync(AppName, tuple.Item1, tuple.Item2, new SecRoleUpdates
            {
                Name = role.Name,
                Description = role.Description,
                Notes = role.Notes
            });
        }

        /// <summary>
        ///   Deletes a role.
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        public async Task DeleteAsync(SecRole role)
        {
            var tuple = SecRole.FromIdentityRoleName(role);
            await SecurityRepository.RemoveRoleAsync(AppName, tuple.Item1, tuple.Item2);
        }

        /// <summary>
        ///   Finds a role by ID.
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        public async Task<SecRole> FindByIdAsync(int roleId)
        {
            try
            {
                return await SecurityRepository.GetRoleByIdAsync(AppName, roleId);
            }
            catch (SecRoleNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        ///   Finds a role by ASP.NET role name.
        /// </summary>
        /// <param name="identityRoleName"/>
        /// <returns/>
        public async Task<SecRole> FindByNameAsync(string identityRoleName)
        {
            var tuple = SecRole.FromIdentityRoleName(identityRoleName);
            try
            {
                return await SecurityRepository.GetRoleByNameAsync(AppName, tuple.Item1, tuple.Item2);
            }
            catch (Exception ex) when (ex is SecGroupNotFoundException || ex is SecRoleNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        ///   Finds a role by Caravan group name and role name.
        /// </summary>
        /// <param name="groupName"/>
        /// <param name="roleName"/>
        /// <returns/>
        public async Task<SecRole> FindByNameAsync(string groupName, string roleName)
        {
            try
            {
                return await SecurityRepository.GetRoleByNameAsync(AppName, groupName, roleName);
            }
            catch (Exception ex) when (ex is SecGroupNotFoundException || ex is SecRoleNotFoundException)
            {
                return null;
            }
        }

        #endregion IQueryableRoleStore members
    }
}
