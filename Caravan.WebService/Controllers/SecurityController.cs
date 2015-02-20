using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebApi.Models.Security;
using LinqToQuerystring.WebApi;

namespace Caravan.WebService.Controllers
{
    [RoutePrefix("security")]
    public sealed class SecurityController : ApiController
    {

        #region App

        /// <summary>
        /// Returns all the applications
        /// </summary>
        /// <returns>All the applications</returns>
        [Route("", Name = "GetApps"), LinqToQueryable]
        public IQueryable<LinkedSecApp> GetApps()
        {
            return Db.Security.Apps().AsQueryable().Select(a => new LinkedSecApp(a, Url));
        }

        /// <summary>
        /// Returns all details of the specified application
        /// </summary>
        /// <param name="appName">The application to show</param>
        /// <returns>All the details of the specified application</returns>
        [Route("{appName}")]
        public SecApp GetApp(string appName)
        {
            return Db.Security.App(appName);
        }

        /// <summary>
        /// Insert a new application 
        /// </summary>
        /// <param name="app">The application to insert</param>
        [Route("")]
        public void PutApp([FromBody]SecApp app)
        {
            Db.Security.AddApp(app);
        }

        #endregion

        #region User
        /// <summary>
        /// Returns all the users of the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns>All the users of the specified application</returns>
        [Route("{appName}/users"), LinqToQueryable]
        public IQueryable<SecUser> GetUsers(string appName)
        {
            return Db.Security.Users(appName).AsQueryable();
        }

        /// <summary>
        /// Add a new user in the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="user">The new user to add</param>
        [Route("{appName}/users")]
        public void PutUser(string appName, [FromBody] SecUser user)
        {
           Db.Security.AddUser(appName,user);
        }

        /// <summary>
        /// Update the user with the specified userLogin in the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="user">The user containing element to update </param>
        [Route("{appName}/users/{userLogin}")]
        public void PostUser(string appName, string userLogin,[FromBody] SecUser user)
        {
            Db.Security.UpdateUser(appName, userLogin, user);
        }

        /// <summary>
        /// Delete the user with the specified user login from the specified application
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="userLogin">the user login</param>
        [Route("{appName}/users/{userLogin}")]
        public void DeleteUser(string appName, string userLogin)
        {
            Db.Security.RemoveUser(appName, userLogin);
        }

        /// <summary>
        /// Add a user to a the specified group of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login of the user to add</param>
        /// <param name="groupName">The group name</param>
        [Route("{appName}/users/{userLogin}/{groupName}")]
        public void PutUserToGroup(string appName, string userLogin,string groupName)
        {
            Db.Security.AddUserToGroup(appName,userLogin,groupName);
        }

        /// <summary>
        /// Delete the user with the specified login from the specified group
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="userLogin">The user login</param>
        /// <param name="groupName">The group name</param>
        /// <exception cref="NotImplementedException"></exception>
        [Route("{appName}/users/{userLogin}/{groupName}")]
        public void DeleteUserToGroup(string appName, string userLogin, string groupName)
        {
            Db.Security.RemoveUserFromGroup(appName,userLogin,groupName);
        }

        #endregion

        #region Group
        /// <summary>
        /// Returns all groups of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns></returns>
        [Route("{appName}/groups"),LinqToQueryable]
        public IQueryable<SecGroup> GetGroups(string appName)
        {
            return Db.Security.Groups(appName).AsQueryable();
        }
        
        /// <summary>
        /// Return details on the specified group belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <returns></returns>
        [Route("{appName}/groups/{groupName}"), LinqToQueryable]
        public SecGroup GetGroup(string appName, string groupName)
        {
            return Db.Security.Group(appName, groupName);
        }

        /// <summary>
        /// Add a new group to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="group">Group to add</param>
        [Route("{appName}/groups")]
        public void PutGroup(string appName,[FromBody] SecGroup group)
        {
            Db.Security.AddGroup(appName,group);
        }

        /// <summary>
        /// Remove the group specified from the application specified 
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        [Route("{appName}/groups/{groupName}")]
        public void DeleteGroup(string appName, string groupName)
        {
            Db.Security.RemoveGroup(appName, groupName);
        }

        /// <summary>
        /// Update the specified group 
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="groupName">The group name</param>
        /// <param name="group">The group containing element/s to update</param>
        [Route("{appName}/groups/{groupName}")]
        public void PostGroup(string appName, string groupName, [FromBody] SecGroup group)
        {
            Db.Security.UpdateGroup(appName,groupName,group);
        }   

#endregion

        #region Context

        /// <summary>
        /// Returns all contexts of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All contexts of the specified application</returns>
        [Route("{appName}/contexts")]
        public IQueryable<SecContext> GetContexts(string appName)
        {
            return Db.Security.Contexts(appName).AsQueryable();
        } 

        #endregion

        #region Objects

        /// <summary>
        ///  Returns all objects belong to the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All objects belong to the specified application</returns>
        [Route("{appaName}/objects"), LinqToQueryable]
        public IQueryable<SecObject> GetObjects(string appName)
        {
            return Db.Security.Objects(appName).AsQueryable();
        }

        /// <summary>
        /// Returns all objects in the specified context 
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="contextName">the context name</param>
        /// <returns>All objects in the specified context</returns>
        [Route("{appaName}/objects"), LinqToQueryable]
        public IQueryable<SecObject> GetObjects(string appName, string contextName)
        {
            return Db.Security.Objects(appName,contextName).AsQueryable();
        } 

        #endregion

        #region Entries

        /// <summary>
        /// Returns all entries of the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>All entries of the specified application</returns>
        [Route("{appName}/entries"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntries(string appName)
        {
            return Db.Security.Entries(appName).AsQueryable();
        }

        /// <summary>
        /// Returns all the entries of the specified context
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntries(string appName,string contextName)
        {
            return Db.Security.Entries(appName,contextName).AsQueryable();
        }


        /// <summary>
        /// Returns all the entries of the specified user
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="userLogin">The user login name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/{userLogin}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntries(string appName, string contextName, string userLogin)
        {
            return Db.Security.Entries(appName, contextName, userLogin).AsQueryable();
        }

        /// <summary>
        /// Returns all the entries of the specified object
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/{objectName}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntriesForObject(string appName, string contextName, string objectName)
        {
            return Db.Security.EntriesForObject(appName, contextName, objectName).AsQueryable();
        }

        /// <summary>
        /// Returns all the entries of the specified object
        /// </summary>
        /// <param name="appName">the application name</param>
        /// <param name="contextName">The context name</param>
        /// <param name="objectName">The object name</param>
        /// <param name="userLogin">The user login</param>
        /// <returns></returns>
        [Route("{appName}/entries/{contextName}/{objectName}/{userLogin}"), LinqToQueryable]
        public IQueryable<SecEntry> GetEntriesForObject(string appName, string contextName, string objectName, string userLogin)
        {
            return Db.Security.EntriesForObject(appName, contextName, objectName, userLogin).AsQueryable();
        }

        /// <summary>
        /// Add a new entry in the specified application withe the specified parameters
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="entry">The entry to add</param>
        [Route("{appName}/entries/")]
        public void PutEntry(string appName, [FromBody] SecEntry entry)
        {
            var userLogin = (entry.User == null) ? null : entry.User.Login;
            var groupName = (entry.Group == null) ? null : entry.Group.Name;
            Db.Security.AddEntry(appName, entry.Context,entry.Object,userLogin,groupName);
        }

        /// <summary>
        /// Delete the specified entry from the application
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
            Db.Security.RemoveEntry(appName,contextname,objectName,userLogin,groupName);
        }

        #endregion
    }
}
