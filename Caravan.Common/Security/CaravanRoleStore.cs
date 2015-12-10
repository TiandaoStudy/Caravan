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
        /// <param name="appId">Identificativo dell'applicativo Caravan.</param>
        /// <param name="appName">Nome dell'applicativo Caravan.</param>
        /// <param name="securityRepository">Il repository della sicurezza di Caravan.</param>
        public CaravanRoleStore(long appId, string appName, ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            AppName = appName;
            SecurityRepository = securityRepository;
        }

        /// <summary>
        ///   L'identificativo dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        public long AppId { get; }

        /// <summary>
        ///   Il nome dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        public string AppName { get; }

        /// <summary>
        ///   Stringa da usare come segnaposto quando si vogliono indicare tutti i ruoli.
        /// </summary>
        public string AllRolesPlaceholder { get; } = "*";

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
            try
            {
                // Verifico che esista il gruppo Caravan a cui apparterrà il ruolo.
                await SecurityRepository.GetGroupByNameAsync(AppName, role.GroupName);
            }
            catch (SecGroupNotFoundException)
            {
                // Se no, lo creo.
                await SecurityRepository.AddGroupAsync(AppName, new SecGroup
                {
                    Name = role.GroupName
                });
            }
            // Dopodiché, sicuro che il gruppo ci sia, aggiungo anche il ruolo.
            await SecurityRepository.AddRoleAsync(AppName, role.GroupName, role);
        }

        /// <summary>
        ///   Updates a role.
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        public Task UpdateAsync(SecRole role) => SecurityRepository.UpdateRoleAsync(AppName, role.GroupName, role.Name, new SecRoleUpdates
        {
            Name = role.Name,
            Description = role.Description,
            Notes = role.Notes
        });

        /// <summary>
        ///   Deletes a role.
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        public Task DeleteAsync(SecRole role) => SecurityRepository.RemoveRoleAsync(AppName, role.GroupName, role.Name);

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

        #region ICaravanRoleStore members

        /// <summary>
        ///   Trova tutti gli utenti che hanno un determinato ruolo e gruppo.
        /// 
        ///   Se al posto del ruolo si inserisce <see cref="AllRolesPlaceholder"/>, vengono
        ///   restituiti tutti gli utenti che appartengono al gruppo, indipendentemente dal ruolo svolto.
        /// </summary>
        /// <param name="identityRoleName">
        ///   Il nome del ruolo su identity, composto nel formato "gruppo/ruolo". Se si passa come
        ///   parametro "gruppo/ <see cref="AllRolesPlaceholder"/>", verranno presi tutti gli utenti
        ///   che appartengono al gruppo dato.
        /// </param>
        /// <returns>Tutti gli utenti che appartengono ad un determinato ruolo e gruppo.</returns>
        public async Task<IQueryable<SecUser>> FindUsersInRoleAsync(string identityRoleName)
        {
            var tuple = SecRole.FromIdentityRoleName(identityRoleName);
            return (tuple.Item2 == AllRolesPlaceholder)
                ? await SecurityRepository.GetUsersInGroup(AppName, tuple.Item1)
                : await SecurityRepository.GetUsersInRole(AppName, tuple.Item1, tuple.Item2);
        }

        /// <summary>
        ///   Trova tutti gli utenti che hanno un determinato ruolo e gruppo.
        /// 
        ///   Se al posto del ruolo si inserisce <see cref="AllRolesPlaceholder"/>, vengono
        ///   restituiti tutti gli utenti che appartengono al gruppo, indipendentemente dal ruolo svolto.
        /// </summary>
        /// <param name="groupName">Il nome del gruppo su Caravan.</param>
        /// <param name="roleName">
        ///   Il nome del ruolo su Caravan, oppure <see cref="AllRolesPlaceholder"/> per prenderli tutti.
        /// </param>
        /// <returns>Tutti gli utenti che appartengono ad un determinato ruolo e gruppo.</returns>
        public async Task<IQueryable<SecUser>> FindUsersInRoleAsync(string groupName, string roleName)
        {
            return (roleName == AllRolesPlaceholder)
                ? await SecurityRepository.GetUsersInGroup(AppName, groupName)
                : await SecurityRepository.GetUsersInRole(AppName, groupName, roleName);
        }

        #endregion
    }
}
