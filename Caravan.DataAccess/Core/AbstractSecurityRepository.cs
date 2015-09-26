using Finsa.Caravan.Common.Security;
using Finsa.CodeServices.Common;
using PommaLabs.Thrower;
using System;
using System.Linq;
using Finsa.Caravan.Common.Logging.Exceptions;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security.Models;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class AbstractSecurityRepository<TSec> : ICaravanSecurityRepository where TSec : AbstractSecurityRepository<TSec>
    {
        protected ICaravanLog Log { get; } = ServiceProvider.FetchLog<TSec>();

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

            const string logCtx = "Adding a new APP";

            try
            {
                app.Name = app.Name.ToLowerInvariant();
                if (!AddAppInternal(app))
                {
                    throw new SecAppExistingException();
                }
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
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

            const string logCtx = "Adding a new GROUP";

            try
            {
                newGroup.Name = newGroup.Name.ToLowerInvariant();
                if (!AddGroupInternal(appName.ToLowerInvariant(), newGroup))
                {
                    throw new SecGroupExistingException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"ADDED GROUP '{newGroup.Name}' TO '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public void RemoveGroup(string appName, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(groupName);

            const string logCtx = "Removing a GROUP";

            try
            {
                if (!RemoveGroupInternal(appName.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecGroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"REMOVED GROUP '{groupName}' FROM '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public void UpdateGroup(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(groupName);
            Raise<ArgumentNullException>.IfIsNull(groupUpdates);
            Raise<ArgumentException>.If(groupUpdates.Name.HasValue && string.IsNullOrWhiteSpace(groupUpdates.Name.Value));

            const string logCtx = "Updating a GROUP";

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
                Log.Warn(() => new LogMessage { ShortMessage = $"UPDATED GROUP '{groupName}' IN '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
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

            const string logCtx = "Adding a new USER";

            try
            {
                newUser.Login = newUser.Login.ToLowerInvariant();
                if (!AddUserInternal(appName.ToLowerInvariant(), newUser))
                {
                    throw new SecUserExistingException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"ADDED USER '{newUser.Login}' TO '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public void RemoveUser(string appName, string userLogin)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);

            const string logCtx = "Removing an USER";

            try
            {
                if (!RemoveUserInternal(appName.ToLowerInvariant(), userLogin))
                {
                    throw new SecUserNotFoundException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"REMOVED USER '{userLogin}' FROM '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public void UpdateUser(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            Raise<ArgumentNullException>.IfIsNull(userUpdates);
            Raise<ArgumentException>.If(userUpdates.Login.HasValue && string.IsNullOrWhiteSpace(userUpdates.Login.Value));

            const string logCtx = "Updating an USER";

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
                Log.Warn(() => new LogMessage { ShortMessage = $"UPDATED USER '{userLogin}' IN '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public void AddUserToGroup(string appName, string userLogin, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            Raise<ArgumentException>.IfIsEmpty(groupName);

            const string logCtx = "Adding an USER to a GROUP";

            try
            {
                if (!AddUserToGroupInternal(appName.ToLowerInvariant(), userLogin.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecUserExistingException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"ADDED USER '{userLogin}' TO GROUP '{groupName}' IN '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public void RemoveUserFromGroup(string appName, string userLogin, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(userLogin);
            Raise<ArgumentException>.IfIsEmpty(groupName);

            const string logCtx = "Removing an USER from a GROUP";

            try
            {
                if (!RemoveUserFromGroupInternal(appName.ToLowerInvariant(), userLogin.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecUserNotFoundException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"REMOVED USER '{userLogin}' FROM GROUP '{groupName}' IN '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
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
            Raise<ArgumentException>.If(string.IsNullOrWhiteSpace(userLogin) && string.IsNullOrWhiteSpace(groupName));
            Raise<ArgumentException>.If(!string.IsNullOrWhiteSpace(userLogin) && groupName != null);
            Raise<ArgumentException>.If(!string.IsNullOrWhiteSpace(groupName) && userLogin != null);

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
                Log.Warn(() => new LogMessage
                {
                    ShortMessage = $"Security entry for object '{secObject.Name}' in context '{secContext.Name}' has been added for '{userLogin ?? groupName}'",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public void RemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfIsEmpty(contextName);
            Raise<ArgumentException>.IfIsEmpty(objectName);
            Raise<ArgumentException>.If(string.IsNullOrWhiteSpace(userLogin) && string.IsNullOrWhiteSpace(groupName));
            Raise<ArgumentException>.If(!string.IsNullOrWhiteSpace(userLogin) && groupName != null);
            Raise<ArgumentException>.If(!string.IsNullOrWhiteSpace(groupName) && userLogin != null);

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
                Log.Warn(() => new LogMessage
                {
                    ShortMessage = $"Security entry for object '{objectName.ToLowerInvariant()}' in context '{contextName.ToLowerInvariant()}' has been removed for '{userLogin ?? groupName}'",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Exception(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
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