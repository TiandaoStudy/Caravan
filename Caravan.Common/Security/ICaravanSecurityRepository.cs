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
using System.Threading.Tasks;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;

namespace Finsa.Caravan.Common.Security
{
    public interface ICaravanSecurityRepository
    {
        SecApp CurrentApp { get; }

        SecUser CurrentUser { get; }

        #region Apps

        /// <summary>
        ///   </summary>
        /// <returns></returns>
        SecApp[] GetApps();

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        /// <exception cref="SecAppNotFoundException">There is no app with given <paramref name="appName"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        SecApp GetApp(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="app"></param>
        /// <exception cref="SecAppExistingException">
        ///   There is already an app with given <paramref name="appName"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="app"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="app.Name"/> is null or empty.</exception>
        void AddApp(SecApp app);

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
        Task<SecGroup> GetGroupByIdAsync(string appName, long groupId);

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
        Task<long> AddGroupAsync(string appName, SecGroup newGroup);

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
        /// <param name="groupId"></param>
        /// <param name="groupUpdates"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="groupId"/> or
        ///   <paramref name="groupUpdates.Name"/> are null or empty.
        /// </exception>
        Task UpdateGroupByIdAsync(string appName, long groupId, SecGroupUpdates groupUpdates);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="groupName"></param>
        /// <param name="groupUpdates"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="groupName"/> or
        ///   <paramref name="groupUpdates.Name"/> are null or empty.
        /// </exception>
        Task UpdateGroupByNameAsync(string appName, string groupName, SecGroupUpdates groupUpdates);

        #endregion Groups

        #region Users

        /// <summary>
        ///   La lista di tutti gli utenti.
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        SecUser[] GetUsers(string appName);

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
        SecUser GetUserByLogin(string appName, string userLogin);

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
        SecUser GetUserByEmail(string appName, string userEmail);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="newUser"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        /// <exception cref="SecUserExistingException">
        ///   An user with given login already exists.
        /// </exception>
        void AddUser(string appName, SecUser newUser);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        void RemoveUser(string appName, string userLogin);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <param name="userUpdates"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        void UpdateUser(string appName, string userLogin, SecUserUpdates userUpdates);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <param name="groupName"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="userLogin"/> or
        ///   <paramref name="groupName"/> are null or empty.
        /// </exception>
        void AddUserToGroup(string appName, string userLogin, string groupName);

        /// <summary>
        ///   </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <param name="groupName"></param>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/>, <paramref name="userLogin"/> or
        ///   <paramref name="groupName"/> are null or empty.
        /// </exception>
        void RemoveUserFromGroup(string appName, string userLogin, string groupName);

        #endregion Users

        #region Contexts

        SecContext[] GetContexts(string appName);

        #endregion Contexts

        #region Objects

        SecObject[] GetObjects(string appName);

        SecObject[] GetObjects(string appName, string contextName);

        #endregion Objects

        #region Entries

        SecEntry[] GetEntries(string appName);

        SecEntry[] GetEntries(string appName, string contextName);

        SecEntry[] GetEntriesForUser(string appName, string contextName, string userLogin);

        SecEntry[] GetEntriesForObject(string appName, string contextName, string objectName);

        SecEntry[] GetEntriesForObjectAndUser(string appName, string contextName, string objectName, string userLogin);

        void AddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

        void RemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName);

        #endregion Entries
    }
}