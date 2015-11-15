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
using Finsa.Caravan.WebApi.Models.Security;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Finsa.Caravan.WebApi.Filters;
using Finsa.CodeServices.Common;
using Microsoft.AspNet.Identity;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller che si occupa della gestione della sicurezza.
    /// </summary>
    [RoutePrefix("security"), AuthorizeForCaravan]
    public sealed class SecurityController : ApiController
    {
        private readonly ICaravanSecurityRepository _securityRepository;
        private readonly ICaravanUserManagerFactory _userManagerFactory;
        private readonly ICaravanGroupManagerFactory _groupManagerFactory;

        /// <summary>
        ///   Inizializza il controller con l'istanza della sicurezza di Caravan.
        /// </summary>
        public SecurityController(ICaravanSecurityRepository securityRepository, ICaravanUserManagerFactory userManagerFactory, ICaravanGroupManagerFactory groupManagerFactory)
        {
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            RaiseArgumentNullException.IfIsNull(userManagerFactory, nameof(userManagerFactory));
            RaiseArgumentNullException.IfIsNull(groupManagerFactory, nameof(groupManagerFactory));
            _securityRepository = securityRepository;
            _userManagerFactory = userManagerFactory;
            _groupManagerFactory = groupManagerFactory;
        }

        #region App

        /// <summary>
        ///   Returns all the applications
        /// </summary>
        /// <returns>All the applications</returns>
        [Route("")]
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

        #endregion App

        #region User

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
        public async Task<SecUser> GetUserByLogin(string appName, long userId)
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
        [Route("{appName}/users/{userLogin}")]
        public async Task<SecUser> GetUserByLogin(string appName, string userLogin)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                return await userManager.FindByNameAsync(userLogin);
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
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                return await userManager.CreateAsync(user);
            }
        }

        /// <summary>
        ///   Update the user with the specified userLogin in the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="userUpdates">The user containing element to update</param>
        [Route("{appName}/users/{userLogin}")]
        public async Task<IdentityResult> PutUserByLogin(string appName, string userLogin, [FromBody] SecUserUpdates userUpdates)
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
        public async Task<IdentityResult> DeleteUserByLogin(string appName, string userLogin)
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
        [Route("{appName}/users/{userLogin}/{groupName}")]
        public async Task<IdentityResult> PostUserToGroup(string appName, string userLogin, string groupName)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindByNameAsync(userLogin);
                return await userManager.AddToRoleAsync(user.Id, groupName);
            }
        }

        /// <summary>
        ///   Delete the user with the specified login from the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="groupName">The group name</param>
        /// <exception cref="NotImplementedException"></exception>
        [Route("{appName}/users/{userLogin}/{groupName}")]
        public async Task<IdentityResult> DeleteUserFromGroup(string appName, string userLogin, string groupName)
        {
            using (var userManager = await _userManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindByNameAsync(userLogin);
                return await userManager.RemoveFromRoleAsync(user.Id, groupName);
            }
        }

        #endregion User

        #region Group

        /// <summary>
        ///   Returns all groups of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns></returns>
        [Route("{appName}/groups")]
        public async Task<IEnumerable<SecGroup>> GetGroups(string appName)
        {
            using (var groupManager = await _groupManagerFactory.CreateAsync(appName))
            {
                return groupManager.Roles;
            }
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
            using (var groupManager = await _groupManagerFactory.CreateAsync(appName))
            {
                return await groupManager.FindByIdAsync(groupId);
            }
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
            using (var groupManager = await _groupManagerFactory.CreateAsync(appName))
            {
                return await groupManager.FindByNameAsync(groupName);
            }
        }

        /// <summary>
        ///   Add a new group to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="group">Group to add</param>
        [Route("{appName}/groups")]
        public async Task<IdentityResult> PostGroup(string appName, [FromBody] SecGroup group)
        {
            using (var groupManager = await _groupManagerFactory.CreateAsync(appName))
            {
                return await groupManager.CreateAsync(group);
            }
        }

        /// <summary>
        ///   Update the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <param name="groupUpdates">The group containing element/s to update</param>
        [Route("{appName}/groups/{groupName}")]
        public async Task<IdentityResult> PutGroupByName(string appName, string groupName, [FromBody] SecGroupUpdates groupUpdates)
        {
            using (var groupManager = await _groupManagerFactory.CreateAsync(appName))
            {
                var group = await groupManager.FindByNameAsync(groupName);
                groupUpdates.Name.Do(x => group.Name = x);
                groupUpdates.Description.Do(x => group.Description = x);
                groupUpdates.Notes.Do(x => group.Notes = x);
                return await groupManager.CreateAsync(group);
            }
        }

        /// <summary>
        ///   Remove the group specified from the application specified
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        [Route("{appName}/groups/{groupName}")]
        public async Task<IdentityResult> DeleteGroupByName(string appName, string groupName)
        {
            using (var groupManager = await _groupManagerFactory.CreateAsync(appName))
            {
                var group = await groupManager.FindByNameAsync(groupName);
                return await groupManager.DeleteAsync(group);
            }
        }

        #endregion Group

        #region Context

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

        #endregion Context

        #region Objects

        /// <summary>
        ///   Returns all objects in the specified context
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="contextName">The optional context name</param>
        /// <returns>All objects in the specified context</returns>
        [Route("{appaName}/objects/{contextName?}")]
        public async Task<SecObject[]> GetObjects(string appName, string contextName)
        {
            return (contextName == null)
                ? await _securityRepository.GetObjectsAsync(appName)
                : await _securityRepository.GetObjectsAsync(appName, contextName);
        }

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
        /// <param name="roleName">The role name (must be null if the user login is present, group name should be specified otherwise)</param>
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
