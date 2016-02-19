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

using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Il repository tramite cui accedere alla parte di sicurezza di Caravan.
    /// </summary>
    public interface ICaravanSecurityRepository : IDisposable
    {
        /// <summary>
        ///   L'oggetto usato per la validazione di indirizzi email, numeri di telefono e altro.
        /// </summary>
        ICaravanSecurityValidator Validator { get; }

        #region Apps

        /// <summary>
        ///   </summary>
        /// <returns></returns>
        Task<SecApp[]> GetAppsAsync();

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        /// <exception cref="SecAppNotFoundException">There is no app with given <paramref name="appName"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        Task<SecApp> GetAppAsync(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="app"></param>
        /// <exception cref="SecAppExistingException">
        ///   There is already an app with given <paramref name="app.Name"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="app"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="app.Name"/> is null or empty.</exception>
        Task<int> AddAppAsync(SecApp app);

        #endregion Apps

        #region Groups

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        Task<SecGroup[]> GetGroupsAsync(string appName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="groupId"/> are null or empty.
        /// </exception>
        Task<SecGroup> GetGroupByIdAsync(string appName, int groupId);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="groupName"/> are null or empty.
        /// </exception>
        Task<SecGroup> GetGroupByNameAsync(string appName, string groupName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="newGroup"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="newGroup.Name"/> are null or empty.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="newGroup"/> is null.</exception>
        Task<int> AddGroupAsync(string appName, SecGroup newGroup);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="groupName"/> are null or empty.
        /// </exception>
        Task RemoveGroupAsync(string appName, string groupName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <param name="groupUpdates"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="groupName"/> or
        ///   <paramref name="groupUpdates.Name"/> are null or empty.
        /// </exception>
        Task UpdateGroupAsync(string appName, string groupName, SecGroupUpdates groupUpdates);

        /// <summary>
        ///   Recupera tutti gli utenti che hanno un determinato gruppo.
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task<IQueryable<SecUser>> QueryUsersInGroupAsync(string appName, string groupName);

        #endregion Groups

        #region Roles

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        Task<SecRole[]> GetRolesAsync(string appName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        Task<SecRole[]> GetRolesAsync(string appName, string groupName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="roleId"/> are null or empty.
        /// </exception>
        Task<SecRole> GetRoleByIdAsync(string appName, int roleId);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="roleName"/> are null or empty.
        /// </exception>
        Task<SecRole> GetRoleByNameAsync(string appName, string groupName, string roleName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <param name="newRole"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="newRole.Name"/> are null or empty.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="newRole"/> is null.</exception>
        Task<int> AddRoleAsync(string appName, string groupName, SecRole newRole);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> or <paramref name="roleName"/> are null or empty.
        /// </exception>
        Task RemoveRoleAsync(string appName, string groupName, string roleName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <param name="roleUpdates"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="roleName"/> or
        ///   <paramref name="roleUpdates.Name"/> are null or empty.
        /// </exception>
        Task UpdateRoleAsync(string appName, string groupName, string roleName, SecRoleUpdates roleUpdates);

        /// <summary>
        ///   Recupera tutti gli utenti che hanno un determinato ruolo (e gruppo).
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<IQueryable<SecUser>> QueryUsersInRoleAsync(string appName, string groupName, string roleName);

        #endregion Roles

        #region Users

        /// <summary>
        ///   Apre una query verso il repository degli utenti.
        /// </summary>
        /// <param name="appName">Il nome dell'applicativo per cui si desiderano gli utenti.</param>
        /// <returns>Una query verso il repository degli utenti per l'applicativo dato.</returns>
        Task<IQueryable<SecUser>> QueryUsersAsync(string appName);

        /// <summary>
        ///   Recupera un utente cercandolo per ID.
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        /// <exception cref="SecUserNotFoundException">
        ///   An user with given login does not exist.
        /// </exception>
        Task<SecUser> GetUserByIdAsync(string appName, long userId);

        /// <summary>
        ///   Recupera un utente cercandolo per login.
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        /// <exception cref="SecUserNotFoundException">
        ///   An user with given login does not exist.
        /// </exception>
        Task<SecUser> GetUserByLoginAsync(string appName, string userLogin);

        /// <summary>
        ///   Recupera un utente cercandolo per indirizzo email.
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        /// <exception cref="SecUserNotFoundException">
        ///   An user with given email address does not exist.
        /// </exception>
        Task<SecUser> GetUserByEmailAsync(string appName, string userEmail);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="newUser"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        /// <exception cref="SecUserExistingException">
        ///   An user with given login already exists.
        /// </exception>
        Task<long> AddUserAsync(string appName, SecUser newUser);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        Task RemoveUserAsync(string appName, string userLogin);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <param name="userUpdates"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        Task UpdateUserAsync(string appName, string userLogin, SecUserUpdates userUpdates);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="userLogin"/> or
        ///   <paramref name="roleName"/> are null or empty.
        /// </exception>
        Task AddUserToRoleAsync(string appName, string userLogin, string groupName, string roleName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="userLogin"/> or
        ///   <paramref name="roleName"/> are null or empty.
        /// </exception>
        Task RemoveUserFromRoleAsync(string appName, string userLogin, string groupName, string roleName);

        Task<long> AddUserClaimAsync(string appName, string userLogin, SecClaim claim);

        Task RemoveUserClaimAsync(string appName, string userLogin, string serializedClaimHash);

        #endregion Users

        #region Contexts

        Task<SecContext[]> GetContextsAsync(string appName);

        #endregion Contexts

        #region Objects

        Task<SecObject[]> GetObjectsAsync(string appName);

        Task<SecObject[]> GetObjectsAsync(string appName, string contextName);

        Task<SecObject[]> GetObjectsForUserAsync(string appName, string userLogin);

        Task<SecObject[]> GetObjectsForContextAndUserAsync(string appName, string contextName, string userLogin);

        #endregion Objects

        #region Entries

        Task<SecEntry[]> GetEntriesAsync(string appName);

        Task<SecEntry[]> GetEntriesAsync(string appName, string contextName);

        Task<SecEntry[]> GetEntriesForUserAsync(string appName, string contextName, string userLogin);

        Task<SecEntry[]> GetEntriesForObjectAsync(string appName, string contextName, string objectName);

        Task<SecEntry[]> GetEntriesForObjectAndUserAsync(string appName, string contextName, string objectName, string userLogin);

        Task<long> AddEntryAsync(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName, string roleName);

        Task RemoveEntryAsync(string appName, string contextName, string objectName, string userLogin, string groupName, string roleName);

        #endregion Entries
    }
}
