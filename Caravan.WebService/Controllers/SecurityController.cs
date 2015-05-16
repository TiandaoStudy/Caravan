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
using System.Linq;
using System.Web.Http;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebApi.Models.Security;
using LinqToQuerystring.WebApi;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller che si occupa della gestione della sicurezza.
    /// </summary>
    [RoutePrefix("security")]
    public sealed class SecurityController : ApiController
    {
        #region App

        /// <summary>
        ///   Returns all the applications
        /// </summary>
        /// <returns>All the applications</returns>
        [Route("", Name = "GetApps"), LinqToQueryable]
        public IQueryable<LinkedSecApp> GetApps()
        {
            return DataSource.Security.GetApps().AsQueryable().Select(a => new LinkedSecApp(a, Url));
        }

        /// <summary>
        ///   Returns all details of the specified application
        /// </summary>
        /// <param name="appName">The application to show</param>
        /// <returns>All the details of the specified application</returns>
        [Route("{appName}")]
        public SecApp GetApp(string appName)
        {
            return DataSource.Security.GetApp(appName);
        }

        /// <summary>
        ///   Insert a new application
        /// </summary>
        /// <param name="app">The application to insert</param>
        [Route("")]
        public void PostApp([FromBody] SecApp app)
        {
            DataSource.Security.AddApp(app);
        }

        #endregion App

        #region User

        /// <summary>
        ///   Returns all the users of the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns>All the users of the specified application</returns>
        [Route("{appName}/users"), LinqToQueryable]
        public IQueryable<SecUser> GetUsers(string appName)
        {
            return DataSource.Security.GetUsers(appName).AsQueryable();
        }

        /// <summary>
        ///   Add a new user in the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="user">The new user to add</param>
        [Route("{appName}/users")]
        public void PostUser(string appName, [FromBody] SecUser user)
        {
            DataSource.Security.AddUser(appName, user);
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
            DataSource.Security.UpdateUser(appName, userLogin, userUpdates);
        }

        /// <summary>
        ///   Delete the user with the specified user login from the specified application
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="userLogin">the user login</param>
        [Route("{appName}/users/{userLogin}")]
        public void DeleteUser(string appName, string userLogin)
        {
            DataSource.Security.RemoveUser(appName, userLogin);
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
            DataSource.Security.AddUserToGroup(appName, userLogin, groupName);
        }

        /// <summary>
        ///   Delete the user with the specified login from the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="groupName">The group name</param>
        /// <exception cref="NotImplementedException"></exception>
        [Route("{appName}/users/{userLogin}/{groupName}")]
        public void DeleteUserToGroup(string appName, string userLogin, string groupName)
        {
            DataSource.Security.RemoveUserFromGroup(appName, userLogin, groupName);
        }

        #endregion User

        #region Group

        /// <summary>
        ///   Returns all groups of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns></returns>
        [Route("{appName}/groups"), LinqToQueryable]
        public IQueryable<SecGroup> GetGroups(string appName)
        {
            return DataSource.Security.GetGroups(appName).AsQueryable();
        }

        /// <summary>
        ///   Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <returns></returns>
        [Route("{appName}/groups/{groupName}"), LinqToQueryable]
        public SecGroup GetGroup(string appName, string groupName)
        {
            return DataSource.Security.GetGroupByName(appName, groupName);
        }

        /// <summary>
        ///   Add a new group to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="group">Group to add</param>
        [Route("{appName}/groups")]
        public void PostGroup(string appName, [FromBody] SecGroup group)
        {
            DataSource.Security.AddGroup(appName, group);
        }

        /// <summary>
        ///   Remove the group specified from the application specified
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        [Route("{appName}/groups/{groupName}")]
        public void DeleteGroup(string appName, string groupName)
        {
            DataSource.Security.RemoveGroup(appName, groupName);
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
            DataSource.Security.UpdateGroup(appName, groupName, groupUpdates);
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
            return DataSource.Security.GetContexts(appName).AsQueryable();
        }

        #endregion Context

        #region Objects

        /// <summary>
        ///   Returns all objects belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All objects belong to the specified application</returns>
        [Route("{appaName}/objects"), LinqToQueryable]
        public IQueryable<SecObject> GetObjects(string appName)
        {
            return DataSource.Security.GetObjects(appName).AsQueryable();
        }

        /// <summary>
        ///   Returns all objects in the specified context
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="contextName">the context name</param>
        /// <returns>All objects in the specified context</returns>
        [Route("{appaName}/objects"), LinqToQueryable]
        public IQueryable<SecObject> GetObjects(string appName, string contextName)
        {
            return DataSource.Security.GetObjects(appName, contextName).AsQueryable();
        }

        #endregion Objects

        #region Entries

        /// <summary>
        ///   Returns all entries of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All entries of the specified application</returns>
        [Route("{appName}/entries"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntries(string appName)
        {
            return DataSource.Security.GetEntries(appName).AsQueryable();
        }

        /// <summary>
        ///   Returns all the entries of the specified context
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntries(string appName, string contextName)
        {
            return DataSource.Security.GetEntries(appName, contextName).AsQueryable();
        }

        /// <summary>
        ///   Returns all the entries of the specified user
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="userLogin">The user login name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/{userLogin}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntries(string appName, string contextName, string userLogin)
        {
            return DataSource.Security.GetEntriesForUser(appName, contextName, userLogin).AsQueryable();
        }

        /// <summary>
        ///   Returns all the entries of the specified object
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/{objectName}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntriesForObject(string appName, string contextName, string objectName)
        {
            return DataSource.Security.GetEntriesForObject(appName, contextName, objectName).AsQueryable();
        }

        /// <summary>
        ///   Returns all the entries of the specified object
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <param name="userLogin">The user login</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/{objectName}/{userLogin}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntriesForObject(string appName, string contextName, string objectName, string userLogin)
        {
            return DataSource.Security.GetEntriesForObjectAndUser(appName, contextName, objectName, userLogin).AsQueryable();
        }

        /// <summary>
        ///   Add a new entry in the specified application withe the specified parameters
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="entry">The entry to add</param>
        [Route("{appName}/entries/")]
        public void PostEntry(string appName, [FromBody] SecEntryForAdd entry)
        {
            DataSource.Security.AddEntry(appName, entry.Context, entry.Object, entry.UserLogin, entry.GroupName);
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
            DataSource.Security.RemoveEntry(appName, contextname, objectName, userLogin, groupName);
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