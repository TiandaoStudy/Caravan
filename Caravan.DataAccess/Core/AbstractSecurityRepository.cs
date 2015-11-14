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
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class AbstractSecurityRepository<TSec> : ICaravanSecurityRepository 
        where TSec : AbstractSecurityRepository<TSec>
    {
        protected AbstractSecurityRepository(ICaravanLog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            Log = log;
        }  

        public SecApp CurrentApp { get; private set; }

        public SecUser CurrentUser { get; private set; }

        protected ICaravanLog Log { get; }

        #region Apps

        public async Task<SecApp[]> GetAppsAsync()
        {
            const string logCtx = "Retrieving all APPs";

            try
            {
                return await GetAppsAsyncInternal(null);
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecApp[]);
            }
        }

        public async Task<SecApp> GetAppAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));

            appName = appName?.ToLowerInvariant();
            var logCtx = $"Retrieving {appName} APP";

            try
            {
                var apps = await GetAppsAsyncInternal(appName);
                if (apps.Length == 0)
                {
                    throw new SecAppNotFoundException(appName);
                }
                return apps[0];
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecApp);
            }
        }

        public async Task<int> AddAppAsync(SecApp app)
        {
            // Preconditions
            RaiseArgumentNullException.IfIsNull(app, nameof(app));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(app.Name), ErrorMessages.NullOrWhiteSpaceAppName, nameof(app.Name));

            app.Name = app.Name.ToLowerInvariant();
            var logCtx = $"Adding new '{app.Name}' APP";

            try
            {
                await AddAppAsyncInternal(app);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Added new '{app.Name}' APP",
                    Context = logCtx
                });
                return app.Id;
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(int);
            }
        }

        #endregion Apps

        #region Groups

        public async Task<SecGroup[]> GetGroupsAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));

            appName = appName?.ToLowerInvariant();
            var logCtx = $"Retrieving all GROUPs from '{appName}' APP";

            try
            {
                return await GetGroupsInternal(appName, null);
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecGroup[]);
            }
        }

        public async Task<SecGroup> GetGroupByNameAsync(string appName, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));

            appName = appName?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            var logCtx = $"Retrieving '{groupName}' GROUP from '{appName}' APP";

            try
            {
                var groups = await GetGroupsInternal(appName, groupName);
                if (groups.Length == 0)
                {
                    throw new SecGroupNotFoundException(appName, groupName);
                }
                return groups[0];
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecGroup);
            }
        }

        public async Task<int> AddGroupAsync(string appName, SecGroup newGroup)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentNullException.IfIsNull(newGroup, nameof(newGroup));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(newGroup.Name), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(newGroup.Name));

            appName = appName?.ToLowerInvariant();
            newGroup.Name = newGroup.Name.ToLowerInvariant();
            var logCtx = $"Adding new '{newGroup.Name}' GROUP to '{appName}' APP";

            try
            {
                await AddGroupInternal(appName, newGroup);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Added new '{newGroup.Name}' GROUP to '{appName}' APP",
                    Context = logCtx
                });
                return newGroup.Id;
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(int);
            }
        }

        public async Task RemoveGroupByNameAsync(string appName, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));

            appName = appName?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            var logCtx = $"Removing '{groupName}' GROUP from '{appName}' APP";

            try
            {
                await RemoveGroupInternal(appName, groupName);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Removed '{groupName}' GROUP from '{appName}' APP",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task UpdateGroupByNameAsync(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));
            RaiseArgumentNullException.IfIsNull(groupUpdates, nameof(groupUpdates));
            RaiseArgumentException.If(groupUpdates.Name.HasValue && string.IsNullOrWhiteSpace(groupUpdates.Name.Value));

            appName = appName?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            var logCtx = $"Updating '{groupName}' GROUP in '{appName}' APP";

            try
            {
                groupUpdates.Name.Do(x => groupUpdates.Name = x.ToLowerInvariant());
                await UpdateGroupInternal(appName, groupName, groupUpdates);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Updated '{groupName}' GROUP in '{appName}' APP",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        #endregion Groups

        #region Users

        public async Task<SecUser[]> GetUsersAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));

            appName = appName?.ToLowerInvariant();
            var logCtx = $"Retrieving all USERs from '{appName}' APP";

            try
            {
                return await GetUsersInternal(appName, null, null);
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecUser[]);
            }
        }

        public async Task<SecUser> GetUserByLoginAsync(string appName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));

            appName = appName?.ToLowerInvariant();
            userLogin = userLogin?.ToLowerInvariant();
            var logCtx = $"Retrieving '{userLogin}' USER from '{appName}' APP";

            try
            {
                var users = await GetUsersInternal(appName, userLogin, null);
                if (users.Length == 0)
                {
                    throw new SecUserNotFoundException(appName, userLogin);
                }
                return users[0];
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecUser);
            }
        }

        public async Task<SecUser> GetUserByEmailAsync(string appName, string userEmail)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userEmail), ErrorMessages.NullOrWhiteSpaceUserEmail, nameof(userEmail));

            appName = appName?.ToLowerInvariant();
            userEmail = userEmail?.ToLowerInvariant();
            var logCtx = $"Retrieving '{userEmail}' USER from '{appName}' APP";

            try
            {
                var users = await GetUsersInternal(appName, null, userEmail);
                if (users.Length == 0)
                {
                    throw new SecUserNotFoundException(appName, userEmail);
                }
                return users[0];
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecUser);
            }
        }

        public async Task<long> AddUserAsync(string appName, SecUser newUser)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentNullException.IfIsNull(newUser, nameof(newUser));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(newUser.Login), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(newUser.Login));

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
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(int);
            }
        }

        public async Task RemoveUserAsync(string appName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));

            const string logCtx = "Removing an USER";

            try
            {
                if (!RemoveUserInternal(appName.ToLowerInvariant(), userLogin))
                {
                    throw new SecUserNotFoundException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"REMOVED USER '{userLogin}' FROM '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task UpdateUserByLoginAsync(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));
            RaiseArgumentNullException.IfIsNull(userUpdates, nameof(userUpdates));
            RaiseArgumentException.If(userUpdates.Login.HasValue && string.IsNullOrWhiteSpace(userUpdates.Login.Value), ErrorMessages.NullOrWhiteSpaceUserLogin);

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
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task AddUserToGroupAsync(string appName, string userLogin, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));

            const string logCtx = "Adding an USER to a GROUP";

            try
            {
                if (!AddUserToGroupInternal(appName.ToLowerInvariant(), userLogin.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecUserExistingException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"ADDED USER '{userLogin}' TO GROUP '{groupName}' IN '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task RemoveUserFromGroupAsync(string appName, string userLogin, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));

            const string logCtx = "Removing an USER from a GROUP";

            try
            {
                if (!RemoveUserFromGroupInternal(appName.ToLowerInvariant(), userLogin.ToLowerInvariant(), groupName.ToLowerInvariant()))
                {
                    throw new SecUserNotFoundException();
                }
                Log.Warn(() => new LogMessage { ShortMessage = $"REMOVED USER '{userLogin}' FROM GROUP '{groupName}' IN '{appName}'", Context = logCtx });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        #endregion Users

        #region Contexts

        public Task<SecContext[]> GetContextsAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            return GetContextsInternal(appName.ToLowerInvariant());
        }

        #endregion Contexts

        #region Objects

        public Task<SecObject[]> GetObjectsAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            return GetObjectsInternal(appName.ToLowerInvariant(), null);
        }

        public Task<SecObject[]> GetObjectsAsync(string appName, string contextName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            return GetObjectsInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant());
        }

        #endregion Objects

        #region Entries

        public Task<SecEntry[]> GetEntriesAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            return GetEntriesInternal(appName.ToLowerInvariant(), null, null, null);
        }

        public Task<SecEntry[]> GetEntriesAsync(string appName, string contextName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), null, null);
        }

        public Task<SecEntry[]> GetEntriesForUserAsync(string appName, string contextName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));

            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), null, userLogin.ToLowerInvariant());
        }

        public Task<SecEntry[]> GetEntriesForObjectAsync(string appName, string contextName, string objectName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            Raise<ArgumentException>.IfIsEmpty(objectName);

            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), null);
        }

        public Task<SecEntry[]> GetEntriesForObjectAndUserAsync(string appName, string contextName, string objectName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(objectName), ErrorMessages.NullOrWhiteSpaceObjectName, nameof(objectName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));

            return GetEntriesInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), userLogin.ToLowerInvariant());
        }

        public Task<long> AddEntryAsync(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentNullException.IfIsNull(secContext, nameof(secContext));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(secContext.Name), ErrorMessages.NullOrWhiteSpaceContextName, nameof(secContext.Name));
            RaiseArgumentNullException.IfIsNull(secObject, nameof(secObject));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(secObject.Name), ErrorMessages.NullOrWhiteSpaceObjectName, nameof(secObject.Name));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin) && string.IsNullOrWhiteSpace(groupName));
            RaiseArgumentException.If(!string.IsNullOrWhiteSpace(userLogin) && groupName != null);
            RaiseArgumentException.If(!string.IsNullOrWhiteSpace(groupName) && userLogin != null);

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
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public Task RemoveEntryAsync(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(objectName), ErrorMessages.NullOrWhiteSpaceObjectName, nameof(objectName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin) && string.IsNullOrWhiteSpace(groupName));
            RaiseArgumentException.If(!string.IsNullOrWhiteSpace(userLogin) && groupName != null);
            RaiseArgumentException.If(!string.IsNullOrWhiteSpace(groupName) && userLogin != null);

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
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        #endregion Entries

        #region Abstract Methods

        protected abstract Task<SecApp[]> GetAppsAsyncInternal(string appName);

        protected abstract Task AddAppAsyncInternal(SecApp app);

        protected abstract Task<SecGroup[]> GetGroupsInternal(string appName, string groupName);

        protected abstract Task AddGroupInternal(string appName, SecGroup newGroup);

        protected abstract Task RemoveGroupInternal(string appName, string groupName);

        protected abstract Task UpdateGroupInternal(string appName, string groupName, SecGroupUpdates groupUpdates);

        protected abstract Task<SecUser[]> GetUsersInternal(string appName, string userLogin, string userEmail);

        protected abstract Task AddUserInternal(string appName, SecUser newUser);

        protected abstract Task RemoveUserInternal(string appName, string userLogin);

        protected abstract Task UpdateUserInternal(string appName, string userLogin, SecUserUpdates userUpdates);

        protected abstract Task AddUserToGroupInternal(string appName, string userLogin, string groupName);

        protected abstract Task RemoveUserFromGroupInternal(string appName, string userLogin, string groupName);

        protected abstract Task<SecContext[]> GetContextsInternal(string appName);

        protected abstract Task<SecObject[]> GetObjectsInternal(string appName, string contextName);

        protected abstract Task<SecEntry[]> GetEntriesInternal(string appName, string contextName, string objectName, string userLogin);

        protected abstract Task AddEntryInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

        protected abstract Task RemoveEntryInternal(string appName, string contextName, string objectName, string userLogin, string groupName);

        #endregion Abstract Methods
    }
}