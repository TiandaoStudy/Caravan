using Finsa.Caravan.Common.Models.Logging.Exceptions;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Models.Security.Exceptions;
using Finsa.Caravan.Common.Security;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Diagnostics;
using System;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class AbstractSecurityRepository<TSec> : ICaravanSecurityRepository where TSec : AbstractSecurityRepository<TSec>
    {
        public SecApp CurrentApp { get; private set; }

        public SecUser CurrentUser { get; private set; }

        #region Apps

        public SecApp[] GetApps()
        {
            return GetAppsInternal();
        }

        public SecApp GetApp(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            var app = GetAppInternal(appName.ToLowerInvariant());
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
                app.Name = app.Name.ToLowerInvariant();
                if (!AddAppInternal(app))
                {
                    throw new SecAppExistingException();
                }
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx);
                throw;
            }
        }

        #endregion Apps

        #region Groups

        public SecGroup[] GetGroups(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetGroupsInternal(appName.ToLowerInvariant(), null);
        }

        public SecGroup GetGroupByName(string appName, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(groupName);
            var group = GetGroupsInternal(appName.ToLowerInvariant(), groupName.ToLowerInvariant()).FirstOrDefault();
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
                newGroup.Name = newGroup.Name.ToLowerInvariant();
                if (!AddGroupInternal(appName.ToLowerInvariant(), newGroup))
                {
                    throw new SecGroupExistingException();
                }
                CaravanDataSource.Logger.LogWarnAsync<TSec>("ADDED GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
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
                if (!RemoveGroupInternal(appName.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecGroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);
                }
                CaravanDataSource.Logger.LogWarnAsync<TSec>("REMOVED GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void UpdateGroup(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(groupName);
            Raise<ArgumentException>.If(groupUpdates.Name.HasValue && String.IsNullOrWhiteSpace(groupUpdates.Name.Value));

            const string logCtx = "Updating a group";

            try
            {
                appName = appName.ToLowerInvariant();
                groupName = groupName.ToLowerInvariant();
                if (groupUpdates.Name.HasValue)
                {
                    groupUpdates.Name = Option.Some(groupUpdates.Name.Value.ToLowerInvariant());
                }

                if (!UpdateGroupInternal(appName, groupName, groupUpdates))
                {
                    throw new SecGroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);
                }

                CaravanDataSource.Logger.LogWarnAsync<TSec>("UPDATED GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        #endregion Groups

        #region Users

        public SecUser[] GetUsers(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetUsersInternal(appName.ToLowerInvariant(), null, null);
        }

        public SecUser GetUserByLogin(string appName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);

            appName = appName.ToLowerInvariant();
            userLogin = userLogin.ToLowerInvariant();

            var user = GetUsersInternal(appName, userLogin, null).FirstOrDefault();
            if (user == null)
            {
                throw new SecUserNotFoundException();
            }
            return user;
        }

        public SecUser GetUserByEmail(string appName, string userEmail)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userEmail);

            appName = appName.ToLowerInvariant();
            userEmail = userEmail.ToLowerInvariant();

            var user = GetUsersInternal(appName, null, userEmail).FirstOrDefault();
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
                newUser.Login = newUser.Login.ToLowerInvariant();
                if (!AddUserInternal(appName.ToLowerInvariant(), newUser))
                {
                    throw new SecUserExistingException();
                }
                CaravanDataSource.Logger.LogWarnAsync<TSec>("ADDED USER", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
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
                if (!RemoveUserInternal(appName.ToLowerInvariant(), userLogin))
                {
                    throw new SecUserNotFoundException();
                }
                CaravanDataSource.Logger.LogWarnAsync<TSec>("REMOVED USER", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        public void UpdateUser(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            Raise<ArgumentNullException>.If(userUpdates.Login.HasValue && String.IsNullOrWhiteSpace(userUpdates.Login.Value));

            const string logCtx = "Updating an user";

            try
            {
                appName = appName.ToLowerInvariant();
                userLogin = userLogin.ToLowerInvariant();
                if (userUpdates.Login.HasValue)
                {
                    userUpdates.Login = Option.Some(userUpdates.Login.Value.ToLowerInvariant());
                }

                if (!UpdateUserInternal(appName, userLogin, userUpdates))
                {
                    throw new SecUserNotFoundException();
                }

                CaravanDataSource.Logger.LogWarnAsync<TSec>("UPDATED USER", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
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
                if (!AddUserToGroupInternal(appName.ToLowerInvariant(), userLogin.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecUserExistingException();
                }
                CaravanDataSource.Logger.LogWarnAsync<TSec>("ADDED USER TO GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
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
                if (!RemoveUserFromGroupInternal(appName.ToLowerInvariant(), userLogin.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecUserNotFoundException();
                }
                CaravanDataSource.Logger.LogWarnAsync<TSec>("REMOVED USER FROM GROUP", context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogErrorAsync<AbstractSecurityRepository<TSec>>(ex, logCtx, appName: appName);
                throw;
            }
        }

        #endregion Users

        #region Contexts

        public SecContext[] GetContexts(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetContextsInternal(appName.ToLowerInvariant());
        }

        #endregion Contexts

        #region Objects

        public SecObject[] GetObjects(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetObjectsInternal(appName.ToLowerInvariant(), null);
        }

        public SecObject[] GetObjects(string appName, string contextName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            return GetObjectsInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant());
        }

        #endregion Objects

        #region Entries

        public SecEntry[] GetEntries(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetEntriesInternal(appName.ToLowerInvariant(), null, null, null);
        }

        public SecEntry[] GetEntries(string appName, string contextName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), null, null);
        }

        public SecEntry[] GetEntriesForUser(string appName, string contextName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), null, userLogin.ToLowerInvariant());
        }

        public SecEntry[] GetEntriesForObject(string appName, string contextName, string objectName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(objectName);
            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), null);
        }

        public SecEntry[] GetEntriesForObjectAndUser(string appName, string contextName, string objectName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(objectName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), userLogin.ToLowerInvariant());
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
                secContext.Name = secContext.Name.ToLowerInvariant();
                secObject.Name = secObject.Name.ToLowerInvariant();
                if (userLogin != null)
                {
                    userLogin = userLogin.ToLowerInvariant();
                }
                if (groupName != null)
                {
                    groupName = groupName.ToLowerInvariant();
                }
                if (!AddEntryInternal(appName.ToLowerInvariant(), secContext, secObject, userLogin, groupName))
                {
                    throw new LogEntryExistingException();
                }
                CaravanDataSource.Logger.LogWarnAsync<TSec>(String.Format(logShort, secObject.Name, secContext.Name, userLogin ?? groupName), context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogWarnAsync<TSec>(ex, logCtx, appName: appName);
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
                    userLogin = userLogin.ToLowerInvariant();
                }
                if (groupName != null)
                {
                    groupName = groupName.ToLowerInvariant();
                }
                RemoveEntryInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), userLogin, groupName);
                CaravanDataSource.Logger.LogWarnAsync<TSec>(String.Format(logShort, objectName.ToLowerInvariant(), contextName.ToLowerInvariant(), userLogin ?? groupName), context: logCtx, appName: appName);
            }
            catch (Exception ex)
            {
                CaravanDataSource.Logger.LogWarnAsync<TSec>(ex, logCtx, appName: appName);
                throw;
            }
        }

        #endregion Entries

        #region Abstract Methods

        protected abstract SecApp[] GetAppsInternal();

        protected abstract SecApp GetAppInternal(string appName);

        protected abstract bool AddAppInternal(SecApp app);

        protected abstract SecGroup[] GetGroupsInternal(string appName, string groupName);

        protected abstract bool AddGroupInternal(string appName, SecGroup newGroup);

        protected abstract bool RemoveGroupInternal(string appName, string groupName);

        protected abstract bool UpdateGroupInternal(string appName, string groupName, SecGroupUpdates groupUpdates);

        protected abstract SecUser[] GetUsersInternal(string appName, string userLogin, string userEmail);

        protected abstract bool AddUserInternal(string appName, SecUser newUser);

        protected abstract bool RemoveUserInternal(string appName, string userLogin);

        protected abstract bool UpdateUserInternal(string appName, string userLogin, SecUserUpdates userUpdates);

        protected abstract bool AddUserToGroupInternal(string appName, string userLogin, string groupName);

        protected abstract bool RemoveUserFromGroupInternal(string appName, string userLogin, string groupName);

        protected abstract SecContext[] GetContextsInternal(string appName);

        protected abstract SecObject[] GetObjectsInternal(string appName, string contextName);

        protected abstract SecEntry[] GetEntriesInternal(string appName, string contextName, string objectName, string userLogin);

        protected abstract bool AddEntryInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

        protected abstract bool RemoveEntryInternal(string appName, string contextName, string objectName, string userLogin, string groupName);

        #endregion Abstract Methods
    }
}