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

using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.WebApi.Filters;
using Finsa.Caravan.WebApi.Models.Security;
using Finsa.CodeServices.Common;
using Microsoft.AspNet.Identity;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller che si occupa della gestione della sicurezza.
    /// </summary>
    [RoutePrefix("security"), OAuth2Authorize]
    public sealed class SecurityController : ApiController
    {
        private readonly ICaravanSecurityRepository _securityRepository;
        private readonly ICaravanUserManagerFactory _userManagerFactory;
        private readonly ICaravanRoleManagerFactory _roleManagerFactory;

        /// <summary>
        ///   Inizializza il controller con l'istanza della sicurezza di Caravan.
        /// </summary>
        public SecurityController(ICaravanSecurityRepository securityRepository, ICaravanUserManagerFactory userManagerFactory, ICaravanRoleManagerFactory roleManagerFactory)
        {
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            RaiseArgumentNullException.IfIsNull(userManagerFactory, nameof(userManagerFactory));
            RaiseArgumentNullException.IfIsNull(roleManagerFactory, nameof(roleManagerFactory));
            _securityRepository = securityRepository;
            _userManagerFactory = userManagerFactory;
            _roleManagerFactory = roleManagerFactory;
        }

        #region Apps

        /// <summary>
        ///   Returns all the applications
        /// </summary>
        /// <returns>All the applications</returns>
        [Route("", Name = "GetApps")]
        public async Task<IEnumerable<LinkedSecApp>> GetApps()
        {
            var apps = await _securityRepository.GetAppsAsync();
            return apps.Select(a => new LinkedSecApp(a, Url));
        }

        /// <summary>
        ///   Returns all details of the specified application
        /// </summary>
        /// <param name="appName">The application to show</param>
        /// <returns>All the details of the specified application</returns>
        [Route("{appName}")]
        public async Task<LinkedSecApp> GetApp(string appName)
        {
            var app = await _securityRepository.GetAppAsync(appName);
            return new LinkedSecApp(app, Url);
        }

        /// <summary>
        ///   Insert a new application
        /// </summary>
        /// <param name="app">The application to insert</param>
        [Route("")]
        public async Task PostApp([FromBody] SecApp app)
        {
            await _securityRepository.AddAppAsync(app);
        }

        #endregion Apps

        #region Users

        /// <summary>
        ///   Returns all the users of the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns>All the users of the specified application</returns>
        [Route("{appName}/users")]
        public async Task<IEnumerable<SecUser>> GetUsers(string appName)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                return userManager.Users;
            }
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userId">The group name</param>
        /// <returns></returns>
        [Route("{appName}/users/{userId:long}")]
        public async Task<SecUser> GetUserById(string appName, long userId)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                return await userManager.FindByIdAsync(userId);
            }
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The group name</param>
        /// <returns></returns>
        [Route("{appName}/users/byLogin/{userLogin}")]
        public async Task<SecUser> GetUserByLogin(string appName, string userLogin)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                return await userManager.FindByNameAsync(userLogin);
            }
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userEmail">The group name</param>
        /// <returns></returns>
        [Route("{appName}/users/byEmail/{userEmail}")]
        public async Task<SecUser> GetUserByEmail(string appName, string userEmail)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                return await userManager.FindByEmailAsync(userEmail);
            }
        }

        /// <summary>
        ///   Add a new user in the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="user">The new user to add</param>
        [Route("{appName}/users")]
        public async Task<IdentityResult> PostUser(string appName, [FromBody] SecUser user)
        {
            var password = user.PasswordHash;
            user.PasswordHash = string.Empty;
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                return await userManager.CreateAsync(user, password);
            }
        }

        /// <summary>
        ///   Update the user with the specified userLogin in the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="userUpdates">The user containing element to update</param>
        [Route("{appName}/users/{userLogin}")]
        public async Task<IdentityResult> PutUser(string appName, string userLogin, [FromBody] SecUserUpdates userUpdates)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindByNameAsync(userLogin);
                userUpdates.FirstName.Do(x => user.FirstName = x);
                userUpdates.LastName.Do(x => user.LastName = x);
                return await userManager.UpdateAsync(user);
            }
        }

        /// <summary>
        ///   Delete the user with the specified user login from the specified application
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="userLogin">the user login</param>
        [Route("{appName}/users/{userLogin}")]
        public async Task<IdentityResult> DeleteUser(string appName, string userLogin)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindByNameAsync(userLogin);
                return await userManager.DeleteAsync(user);
            }
        }

        /// <summary>
        ///   Add a user to a the specified group of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login of the user to add</param>
        /// <param name="groupName">The group name</param>
        /// <param name="roleName">The role name</param>
        [Route("{appName}/users/{userLogin}/{groupName}/{roleName}")]
        public async Task<IdentityResult> PostUserToRole(string appName, string userLogin, string groupName, string roleName)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindByNameAsync(userLogin);
                var identityRoleName = SecRole.ToIdentityRoleName(groupName, roleName);
                return await userManager.AddToRoleAsync(user.Id, groupName, roleName);
            }
        }

        /// <summary>
        ///   Delete the user with the specified login from the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="groupName">The group name</param>
        /// <param name="roleName">The role name</param>
        /// <exception cref="NotImplementedException"></exception>
        [Route("{appName}/users/{userLogin}/{groupName}/{roleName}")]
        public async Task<IdentityResult> DeleteUserFromRole(string appName, string userLogin, string groupName, string roleName)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindByNameAsync(userLogin);
                var identityRoleName = SecRole.ToIdentityRoleName(groupName, roleName);
                return await userManager.RemoveFromRoleAsync(user.Id, identityRoleName);
            }
        }

        #endregion Users

        #region Groups

        /// <summary>
        ///   Returns all groups of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns></returns>
        [Route("{appName}/groups")]
        public async Task<IEnumerable<SecGroup>> GetGroups(string appName)
        {
            return await _securityRepository.GetGroupsAsync(appName);
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupId">The group name</param>
        /// <returns></returns>
        [Route("{appName}/groups/{groupId:int}")]
        public async Task<SecGroup> GetGroupById(string appName, int groupId)
        {
            return await _securityRepository.GetGroupByIdAsync(appName, groupId);
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <returns></returns>
        [Route("{appName}/groups/{groupName}")]
        public async Task<SecGroup> GetGroupByName(string appName, string groupName)
        {
            return await _securityRepository.GetGroupByNameAsync(appName, groupName);
        }

        /// <summary>
        ///   Add a new group to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="group">Group to add</param>
        [Route("{appName}/groups")]
        public async Task<int> PostGroup(string appName, [FromBody] SecGroup group)
        {
            return await _securityRepository.AddGroupAsync(appName, group);
        }

        /// <summary>
        ///   Update the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <param name="groupUpdates">The group containing element/s to update</param>
        [Route("{appName}/groups/{groupName}")]
        public async Task PutGroupByName(string appName, string groupName, [FromBody] SecGroupUpdates groupUpdates)
        {
            await _securityRepository.UpdateGroupAsync(appName, groupName, groupUpdates);
        }

        /// <summary>
        ///   Remove the group specified from the application specified
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        [Route("{appName}/groups/{groupName}")]
        public async Task DeleteGroupByName(string appName, string groupName)
        {
            await _securityRepository.RemoveGroupAsync(appName, groupName);
        }

        #endregion Groups

        #region Roles

        /// <summary>
        ///   Returns all groups of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns></returns>
        [Route("{appName}/roles")]
        public async Task<IEnumerable<SecRole>> GetRoles(string appName)
        {
            using (var roleManager = await _roleManagerFactory.CreateAsync(appName))
            {
                return roleManager.Roles;
            }
        }

        /// <summary>
        ///   Returns all groups of the specified application
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="groupName">The group name.</param>
        /// <returns></returns>
        [Route("{appName}/roles/{groupName}")]
        public async Task<IEnumerable<SecRole>> GetRoles(string appName, string groupName)
        {
            using (var roleManager = await _roleManagerFactory.CreateAsync(appName))
            {
                return roleManager.Roles.Where(r => r.GroupName == groupName);
            }
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="roleId">The group name</param>
        /// <returns></returns>
        [Route("{appName}/roles/{roleId:int}")]
        public async Task<SecRole> GetRoleById(string appName, int roleId)
        {
            using (var roleManager = await _roleManagerFactory.CreateAsync(appName))
            {
                return await roleManager.FindByIdAsync(roleId);
            }
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <returns></returns>
        [Route("{appName}/roles/{groupName}/{roleName}")]
        public async Task<SecRole> GetRoleByName(string appName, string groupName, string roleName)
        {
            using (var roleManager = await _roleManagerFactory.CreateAsync(appName))
            {
                var identityRoleName = SecRole.ToIdentityRoleName(groupName, roleName);
                return await roleManager.FindByNameAsync(identityRoleName);
            }
        }

        /// <summary>
        ///   Add a new group to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <param name="role">Group to add</param>
        [Route("{appName}/roles/{groupName}")]
        public async Task<IdentityResult> PostRole(string appName, string groupName, [FromBody] SecRole role)
        {
            using (var roleManager = await _roleManagerFactory.CreateAsync(appName))
            {
                role.GroupName = groupName;
                return await roleManager.CreateAsync(role);
            }
        }

        /// <summary>
        ///   Update the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <param name="roleName">The role name</param>
        /// <param name="roleUpdates">The group containing element/s to update</param>
        [Route("{appName}/roles/{groupName}/{roleName}")]
        public async Task<IdentityResult> PutRole(string appName, string groupName, string roleName, [FromBody] SecRoleUpdates roleUpdates)
        {
            using (var roleManager = await _roleManagerFactory.CreateAsync(appName))
            {
                var identityRoleName = SecRole.ToIdentityRoleName(groupName, roleName);
                var role = await roleManager.FindByNameAsync(identityRoleName);
                roleUpdates.Name.Do(x => role.Name = x);
                roleUpdates.Description.Do(x => role.Description = x);
                roleUpdates.Notes.Do(x => role.Notes = x);
                return await roleManager.CreateAsync(role);
            }
        }

        /// <summary>
        ///   Remove the group specified from the application specified
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <param name="roleName">The role name</param>
        [Route("{appName}/roles/{groupName}/{roleName}")]
        public async Task<IdentityResult> DeleteRole(string appName, string groupName, string roleName)
        {
            using (var roleManager = await _roleManagerFactory.CreateAsync(appName))
            {
                var identityRoleName = SecRole.ToIdentityRoleName(groupName, roleName);
                var role = await roleManager.FindByNameAsync(identityRoleName);
                return await roleManager.DeleteAsync(role);
            }
        }

        #endregion Roles

        #region Contexts

        /// <summary>
        ///   Returns all contexts of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All contexts of the specified application</returns>
        [Route("{appName}/contexts")]
        public async Task<SecContext[]> GetContexts(string appName)
        {
            return await _securityRepository.GetContextsAsync(appName);
        }

        #endregion Contexts

        #region Objects

        /// <summary>
        ///   Restituisce tutti gli oggetti nel contesto specificato, se presente.
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="contextName">The optional context name</param>
        /// <returns>All objects in the specified context</returns>
        [Route("{appaName}/objects/{contextName?}")]
        public Task<SecObject[]> GetObjects(string appName, string contextName) => (contextName == null)
            ? _securityRepository.GetObjectsAsync(appName)
            : _securityRepository.GetObjectsAsync(appName, contextName);

        /// <summary>
        ///   Restituisce tutti gli oggetti nel contesto specificato, se presente.
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="contextName">The optional context name</param>
        /// <returns>All objects in the specified context</returns>
        [Route("{appaName}/objects/forUser/{contextName}/{userLogin}")]
        public Task<SecObject[]> GetObjectsForContextAndUser(string appName, string contextName, string userLogin) => 
            _securityRepository.GetObjectsForContextAndUserAsync(appName, contextName, userLogin);

        #endregion Objects

        #region Entries

        /// <summary>
        ///   Returns all entries of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All entries of the specified application</returns>
        [Route("{appName}/entries")]
        public async Task<SecEntry[]> GetEntries(string appName)
        {
            return await _securityRepository.GetEntriesAsync(appName);
        }

        /// <summary>
        ///   Returns all the entries of the specified context
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}")]
        public async Task<SecEntry[]> GetEntries(string appName, string contextName)
        {
            return await _securityRepository.GetEntriesAsync(appName, contextName);
        }

        /// <summary>
        ///   Returns all the entries of the specified user
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="userLogin">The user login name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/forUser/{userLogin}")]
        public async Task<SecEntry[]> GetEntriesForUser(string appName, string contextName, string userLogin)
        {
            return await _securityRepository.GetEntriesForUserAsync(appName, contextName, userLogin);
        }

        /// <summary>
        ///   Returns all the entries of the specified object
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/forObject/{objectName}")]
        public async Task<SecEntry[]> GetEntriesForObject(string appName, string contextName, string objectName)
        {
            return await _securityRepository.GetEntriesForObjectAsync(appName, contextName, objectName);
        }

        /// <summary>
        ///   Returns all the entries of the specified object
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <param name="userLogin">The user login</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/forObject/{objectName}/forUser/{userLogin}")]
        public async Task<SecEntry[]> GetEntriesForObject(string appName, string contextName, string objectName, string userLogin)
        {
            return await _securityRepository.GetEntriesForObjectAndUserAsync(appName, contextName, objectName, userLogin);
        }

        /// <summary>
        ///   Add a new entry in the specified application withe the specified parameters
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="entry">The entry to add</param>
        [Route("{appName}/entries/")]
        public async Task PostEntry(string appName, [FromBody] SecEntryForAdd entry)
        {
            await _securityRepository.AddEntryAsync(appName, entry.Context, entry.Object, entry.UserLogin, entry.GroupName, entry.RoleName);
        }

        /// <summary>
        ///   Delete the specified entry from the application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="contextname">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <param name="userLogin">The user login (must be null if the group name is present)</param>
        /// <param name="groupName">The group name (must be null if the user login is present)</param>
        /// <param name="roleName">
        ///   The role name (must be null if the user login is present, group name should be
        ///   specified otherwise)
        /// </param>
        [Route("{appName}/entries/{contextName}/{objectName}")]
        public async Task DeleteEntry(string appName, string contextname, string objectName, string userLogin, string groupName, string roleName)
        {
            await _securityRepository.RemoveEntryAsync(appName, contextname, objectName, userLogin, groupName, roleName);
        }

        public sealed class SecEntryForAdd
        {
            public SecContext Context { get; set; }

            public SecObject Object { get; set; }

            public string UserLogin { get; set; }

            public string GroupName { get; set; }

            public string RoleName { get; set; }
        }

        #endregion Entries
    }
}
