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
using System.Linq;
using System.Web.Http;
using Finsa.Caravan.WebApi.FilterAttributes;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller che si occupa della gestione della sicurezza.
    /// </summary>
    [RoutePrefix("security"), AuthorizeForCaravan]
    public sealed partial class SecurityController : ApiController
    {
        readonly ICaravanSecurityRepository _securityRepository;

        /// <summary>
        ///   Inizializza il controller con l'istanza della sicurezza di Caravan.
        /// </summary>
        public SecurityController(ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            _securityRepository = securityRepository;
        }

        #region App

        /// <summary>
        ///   Returns all the applications
        /// </summary>
        /// <returns>All the applications</returns>
        [Route("", Name = "GetApps")]
        public IQueryable<LinkedSecApp> GetApps()
        {
            return _securityRepository.GetApps().AsQueryable().Select(a => new LinkedSecApp(a, Url));
        }

        /// <summary>
        ///   Returns all details of the specified application
        /// </summary>
        /// <param name="appName">The application to show</param>
        /// <returns>All the details of the specified application</returns>
        [Route("{appName}")]
        public SecApp GetApp(string appName)
        {
            return _securityRepository.GetApp(appName);
        }

        /// <summary>
        ///   Insert a new application
        /// </summary>
        /// <param name="app">The application to insert</param>
        [Route("")]
        public void PostApp([FromBody] SecApp app)
        {
            _securityRepository.AddApp(app);
        }

        #endregion App

        #region User

        /// <summary>
        ///   Returns all the users of the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns>All the users of the specified application</returns>
        [Route("{appName}/users")]
        public IQueryable<SecUser> GetUsers(string appName)
        {
            return _securityRepository.GetUsers(appName).AsQueryable();
        }

        /// <summary>
        ///   Add a new user in the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="user">The new user to add</param>
        [Route("{appName}/users")]
        public void PostUser(string appName, [FromBody] SecUser user)
        {
            _securityRepository.AddUser(appName, user);
        }

        /// <summary>
        ///   Update the user with the specified userLogin in the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="userUpdates">The user containing element to update</param>
        [Route("{appName}/users/{userLogin}")]
        public void PutUser(string appName, string userLogin, [FromBody] SecUserUpdates userUpdates)
        {
            _securityRepository.UpdateUser(appName, userLogin, userUpdates);
        }

        /// <summary>
        ///   Delete the user with the specified user login from the specified application
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="userLogin">the user login</param>
        [Route("{appName}/users/{userLogin}")]
        public void DeleteUser(string appName, string userLogin)
        {
            _securityRepository.RemoveUser(appName, userLogin);
        }

        /// <summary>
        ///   Add a user to a the specified group of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login of the user to add</param>
        /// <param name="groupName">The group name</param>
        [Route("{appName}/users/{userLogin}/{groupName}")]
        public void PostUserToGroup(string appName, string userLogin, string groupName)
        {
            _securityRepository.AddUserToGroup(appName, userLogin, groupName);
        }

        /// <summary>
        ///   Delete the user with the specified login from the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="groupName">The group name</param>
        /// <exception cref="NotImplementedException"></exception>
        [Route("{appName}/users/{userLogin}/{groupName}")]
        public void DeleteUserFromGroup(string appName, string userLogin, string groupName)
        {
            _securityRepository.RemoveUserFromGroup(appName, userLogin, groupName);
        }

        #endregion User

        #region Group

        /// <summary>
        ///   Returns all groups of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns></returns>
        [Route("{appName}/groups")]
        public IQueryable<SecGroup> GetGroups(string appName)
        {
            return _securityRepository.GetGroups(appName).AsQueryable();
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <returns></returns>
        [Route("{appName}/groups/{groupName}")]
        public SecGroup GetGroup(string appName, string groupName)
        {
            return _securityRepository.GetGroupByName(appName, groupName);
        }

        /// <summary>
        ///   Add a new group to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="group">Group to add</param>
        [Route("{appName}/groups")]
        public void PostGroup(string appName, [FromBody] SecGroup group)
        {
            _securityRepository.AddGroup(appName, group);
        }

        /// <summary>
        ///   Remove the group specified from the application specified
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        [Route("{appName}/groups/{groupName}")]
        public void DeleteGroup(string appName, string groupName)
        {
            _securityRepository.RemoveGroup(appName, groupName);
        }

        /// <summary>
        ///   Update the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <param name="groupUpdates">The group containing element/s to update</param>
        [Route("{appName}/groups/{groupName}")]
        public void PutGroup(string appName, string groupName, [FromBody] SecGroupUpdates groupUpdates)
        {
            _securityRepository.UpdateGroup(appName, groupName, groupUpdates);
        }

        #endregion Group

        #region Context

        /// <summary>
        ///   Returns all contexts of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All contexts of the specified application</returns>
        [Route("{appName}/contexts")]
        public IQueryable<SecContext> GetContexts(string appName)
        {
            return _securityRepository.GetContexts(appName).AsQueryable();
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
        public SecObject[] GetObjects(string appName, string contextName)
        {
            return (contextName == null)
                ? _securityRepository.GetObjects(appName)
                : _securityRepository.GetObjects(appName, contextName);
        }

        #endregion Objects

        #region Entries

        /// <summary>
        ///   Returns all entries of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All entries of the specified application</returns>
        [Route("{appName}/entries")]
        public IQueryable<SecEntry> GetEntries(string appName)
        {
            return _securityRepository.GetEntries(appName).AsQueryable();
        }

        /// <summary>
        ///   Returns all the entries of the specified context
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}")]
        public IQueryable<SecEntry> GetEntries(string appName, string contextName)
        {
            return _securityRepository.GetEntries(appName, contextName).AsQueryable();
        }

        /// <summary>
        ///   Returns all the entries of the specified user
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="userLogin">The user login name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/forUser/{userLogin}")]
        public IQueryable<SecEntry> GetEntriesForUser(string appName, string contextName, string userLogin)
        {
            return _securityRepository.GetEntriesForUser(appName, contextName, userLogin).AsQueryable();
        }

        /// <summary>
        ///   Returns all the entries of the specified object
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/forObject/{objectName}")]
        public IQueryable<SecEntry> GetEntriesForObject(string appName, string contextName, string objectName)
        {
            return _securityRepository.GetEntriesForObject(appName, contextName, objectName).AsQueryable();
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
        public IQueryable<SecEntry> GetEntriesForObject(string appName, string contextName, string objectName, string userLogin)
        {
            return _securityRepository.GetEntriesForObjectAndUser(appName, contextName, objectName, userLogin).AsQueryable();
        }

        /// <summary>
        ///   Add a new entry in the specified application withe the specified parameters
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="entry">The entry to add</param>
        [Route("{appName}/entries/")]
        public void PostEntry(string appName, [FromBody] SecEntryForAdd entry)
        {
            _securityRepository.AddEntry(appName, entry.Context, entry.Object, entry.UserLogin, entry.GroupName);
        }

        /// <summary>
        ///   Delete the specified entry from the application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="contextname">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <param name="userLogin">The user login (must be null if the group name is present)</param>
        /// <param name="groupName">The group name (must be null if the user login is present)</param>
        [Route("{appName}/entries/{contextName}/{objectName}")]
        public void DeleteEntry(string appName, string contextname, string objectName, string userLogin,
            string groupName)
        {
            _securityRepository.RemoveEntry(appName, contextname, objectName, userLogin, groupName);
        }

        public sealed class SecEntryForAdd
        {
            public SecContext Context { get; set; }

            public SecObject Object { get; set; }

            public string UserLogin { get; set; }

            public string GroupName { get; set; }
        }

        #endregion Entries
    }
}
