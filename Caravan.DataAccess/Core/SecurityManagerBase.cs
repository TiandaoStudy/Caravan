using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Finsa.Caravan.Common.Models.Logging.Exceptions;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Models.Security.Exceptions;
using PommaLabs.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class SecurityManagerBase<TSec> : ISecurityManager where TSec : SecurityManagerBase<TSec>
    {
        public SecApp CurrentApp { get; private set; }

        public SecUser CurrentUser { get; private set; }

        #region Apps

        public IList<SecApp> Apps()
        {
            return GetApps();
        }

        public SecApp App(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            var app = GetApp(appName.ToLower());
            if (app == null)
            {
                throw new SecAppNotFoundException();
            }
            return app;
        }

        public void AddApp(SecApp app)
        {
            Raise<ArgumentNullException>.IfIsNull(app);
            Raise<ArgumentException>.IfIsEmpty(app.Name);

            const string logCtx = "Adding a new app";

            try
            {
                app.Name = app.Name.ToLower(CultureInfo.InvariantCulture);
                if (!DoAddApp(app))
                {
                    throw new SecAppExistingException();
                }
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx);
                throw;
            }
        }

        #endregion Apps

        #region Groups

        public IList<SecGroup> Groups(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetGroups(appName.ToLower(), null);
        }

        public SecGroup Group(string appName, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(groupName);
            var group = GetGroups(appName.ToLower(), groupName.ToLower()).FirstOrDefault();
            if (group == null)
            {
                throw new SecGroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);
            }
            return group;
        }

        public void AddGroup(string appName, SecGroup newGroup)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentNullException>.IfIsNull(newGroup);
            Raise<ArgumentException>.IfIsEmpty(newGroup.Name);

            const string logCtx = "Adding a new group";

            try
            {
                newGroup.Name = newGroup.Name.ToLower();
                if (!DoAddGroup(appName.ToLower(), newGroup))
                {
                    throw new SecGroupExistingException();
                }
                Db.Logger.LogWarnAsync<TSec>("ADDED GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void RemoveGroup(string appName, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(groupName);

            const string logCtx = "Removing a group";

            try
            {
                if (!DoRemoveGroup(appName.ToLower(), groupName.ToLower()))
                {
                    throw new SecGroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);
                }
                Db.Logger.LogWarnAsync<TSec>("REMOVED GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void UpdateGroup(string appName, string groupName, SecGroup newGroup)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(groupName);
            Raise<ArgumentNullException>.IfIsNull(newGroup);
            Raise<ArgumentException>.IfIsEmpty(newGroup.Name);

            const string logCtx = "Updating a group";

            try
            {
                newGroup.Name = newGroup.Name.ToLower();
                if (!DoUpdateGroup(appName.ToLower(), groupName.ToLower(), newGroup))
                {
                    throw new SecGroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);
                }
                Db.Logger.LogWarnAsync<TSec>("UPDATED GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        #endregion Groups

        #region Users

        public IList<SecUser> Users(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetUsers(appName.ToLower(), null);
        }

        public SecUser User(string appName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            var user = GetUsers(appName.ToLower(), userLogin.ToLower()).FirstOrDefault();
            if (user == null)
            {
                throw new SecUserNotFoundException();
            }
            return user;
        }

        public void AddUser(string appName, SecUser newUser)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentNullException>.IfIsNull(newUser);
            Raise<ArgumentException>.IfIsEmpty(newUser.Login);

            const string logCtx = "Adding a new user";

            try
            {
                newUser.Login = newUser.Login.ToLower();
                if (!DoAddUser(appName.ToLower(), newUser))
                {
                    throw new SecUserExistingException();
                }
                Db.Logger.LogWarnAsync<TSec>("ADDED USER", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void RemoveUser(string appName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);

            const string logCtx = "Removing an user";

            try
            {
                if (!DoRemoveUser(appName.ToLower(), userLogin))
                {
                    throw new SecUserNotFoundException();
                }
                Db.Logger.LogWarnAsync<TSec>("REMOVED USER", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void UpdateUser(string appName, string userLogin, SecUser newUser)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            Raise<ArgumentNullException>.IfIsNull(newUser);
            Raise<ArgumentException>.IfIsEmpty(newUser.Login);

            const string logCtx = "Updating an user";

            try
            {
                newUser.Login = newUser.Login.ToLower();
                if (!DoUpdateUser(appName.ToLower(), userLogin.ToLower(), newUser))
                {
                    throw new SecUserNotFoundException();
                }
                Db.Logger.LogWarnAsync<TSec>("UPDATED USER", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void AddUserToGroup(string appName, string userLogin, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            Raise<ArgumentException>.IfIsEmpty(groupName);

            const string logCtx = "Adding an user to a group";

            try
            {
                if (!DoAddUserToGroup(appName.ToLower(), userLogin.ToLower(), groupName.ToLower()))
                {
                    throw new SecUserExistingException();
                }
                Db.Logger.LogWarnAsync<TSec>("ADDED USER TO GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void RemoveUserFromGroup(string appName, string userLogin, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            Raise<ArgumentException>.IfIsEmpty(groupName);

            const string logCtx = "Removing an user from a group";

            try
            {
                if (!DoRemoveUserFromGroup(appName.ToLower(), userLogin.ToLower(), groupName.ToLower()))
                {
                    throw new SecUserNotFoundException();
                }
                Db.Logger.LogWarnAsync<TSec>("REMOVED USER FROM GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogErrorAsync<SecurityManagerBase<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        #endregion Users

        #region Contexts

        public IList<SecContext> Contexts(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetContexts(appName.ToLower());
        }

        #endregion Contexts

        #region Objects

        public IList<SecObject> Objects(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetObjects(appName.ToLower(), null);
        }

        public IList<SecObject> Objects(string appName, string contextName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            return GetObjects(appName.ToLower(), contextName.ToLower());
        }

        #endregion Objects

        #region Entries

        public IList<SecEntry> Entries(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetEntries(appName.ToLower(), null, null, null);
        }

        public IList<SecEntry> Entries(string appName, string contextName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            return GetEntries(appName.ToLower(), contextName.ToLower(), null, null);
        }

        public IList<SecEntry> Entries(string appName, string contextName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            return GetEntries(appName.ToLower(), contextName.ToLower(), null, userLogin.ToLower());
        }

        public IList<SecEntry> EntriesForObject(string appName, string contextName, string objectName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(objectName);
            return GetEntries(appName.ToLower(), contextName.ToLower(), objectName.ToLower(), null);
        }

        public IList<SecEntry> EntriesForObject(string appName, string contextName, string objectName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(objectName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            return GetEntries(appName.ToLower(), contextName.ToLower(), objectName.ToLower(), userLogin.ToLower());
        }

        public void AddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsNull(secContext);
            Raise<ArgumentException>.IfIsEmpty(secContext.Name);
            Raise<ArgumentException>.IfIsNull(secObject);
            Raise<ArgumentException>.IfIsEmpty(secObject.Name);
            Raise<ArgumentException>.If(String.IsNullOrWhiteSpace(userLogin) && String.IsNullOrWhiteSpace(groupName));
            Raise<ArgumentException>.If(!String.IsNullOrWhiteSpace(userLogin) && groupName != null);
            Raise<ArgumentException>.If(!String.IsNullOrWhiteSpace(groupName) && userLogin != null);

            const string logShort = "Security entry for object '{0}' in context '{1}' has been added for '{2}'";
            const string logCtx = "Adding a new security entry";

            try
            {
                secContext.Name = secContext.Name.ToLower();
                secObject.Name = secObject.Name.ToLower();
                if (userLogin != null)
                {
                    userLogin = userLogin.ToLower();
                }
                if (groupName != null)
                {
                    groupName = groupName.ToLower();
                }
                if (!DoAddEntry(appName.ToLower(), secContext, secObject, userLogin, groupName))
                {
                    throw new LogEntryExistingException();
                }
                Db.Logger.LogWarnAsync<TSec>(String.Format(logShort, secObject.Name, secContext.Name, userLogin ?? groupName), context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogWarnAsync<TSec>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void RemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(objectName);
            Raise<ArgumentException>.If(String.IsNullOrWhiteSpace(userLogin) && String.IsNullOrWhiteSpace(groupName));
            Raise<ArgumentException>.If(!String.IsNullOrWhiteSpace(userLogin) && groupName != null);
            Raise<ArgumentException>.If(!String.IsNullOrWhiteSpace(groupName) && userLogin != null);

            const string logShort = "Security entry for object '{0}' in context '{1}' has been removed for '{2}'";
            const string logCtx = "Removing a security entry";

            try
            {
                if (userLogin != null)
                {
                    userLogin = userLogin.ToLower();
                }
                if (groupName != null)
                {
                    groupName = groupName.ToLower();
                }
                DoRemoveEntry(appName.ToLower(), contextName.ToLower(), objectName.ToLower(), userLogin, groupName);
                Db.Logger.LogWarnAsync<TSec>(String.Format(logShort, objectName.ToLower(), contextName.ToLower(), userLogin ?? groupName), context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                Db.Logger.LogWarnAsync<TSec>(ex, logCtx, appName: appName);
                throw;
            }
        }

        #endregion Entries

        #region Abstract Methods

        protected abstract IList<SecApp> GetApps();

        protected abstract SecApp GetApp(string appName);

        protected abstract bool DoAddApp(SecApp app);

        protected abstract IList<SecGroup> GetGroups(string appName, string groupName);

        protected abstract bool DoAddGroup(string appName, SecGroup newGroup);

        protected abstract bool DoRemoveGroup(string appName, string groupName);

        protected abstract bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup);

        protected abstract IList<SecUser> GetUsers(string appName, string userLogin);

        protected abstract bool DoAddUser(string appName, SecUser newUser);

        protected abstract bool DoRemoveUser(string appName, string userLogin);

        protected abstract bool DoUpdateUser(string appName, string userLogin, SecUser newUser);

        protected abstract bool DoAddUserToGroup(string appName, string userLogin, string groupName);

        protected abstract bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName);

        protected abstract IList<SecContext> GetContexts(string appName);

        protected abstract IList<SecObject> GetObjects(string appName, string contextName);

        protected abstract IList<SecEntry> GetEntries(string appName, string contextName, string objectName, string userLogin);

        protected abstract bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

        protected abstract bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName);

        #endregion Abstract Methods
    }
}