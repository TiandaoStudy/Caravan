﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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

using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using Finsa.CodeServices.Common;
using PommaLabs.Thrower;
using System;
using System.Globalization;
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
            const string logCtx = "Retrieving all applications";

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
            var logCtx = $"Retrieving application '{appName}'";

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
            var logCtx = $"Adding new application '{app.Name}'";

            try
            {
                await AddAppAsyncInternal(app);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Added new application '{app.Name}'",
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
            var logCtx = $"Retrieving all groups from application '{appName}'";

            try
            {
                return await GetGroupsAsyncInternal(appName, null, null);
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecGroup[]);
            }
        }

        public async Task<SecGroup> GetGroupByIdAsync(string appName, int groupId)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));

            appName = appName?.ToLowerInvariant();
            var logCtx = $"Retrieving group #{groupId} from application '{appName}'";

            try
            {
                var groups = await GetGroupsAsyncInternal(appName, groupId, null);
                if (groups.Length == 0)
                {
                    throw new SecGroupNotFoundException(appName, groupId.ToString(CultureInfo.InvariantCulture));
                }
                return groups[0];
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecGroup);
            }
        }

        public async Task<SecGroup> GetGroupByNameAsync(string appName, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));

            appName = appName?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            var logCtx = $"Retrieving group '{groupName}' from application '{appName}'";

            try
            {
                var groups = await GetGroupsAsyncInternal(appName, null, groupName);
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
            var logCtx = $"Adding new group '{newGroup.Name}' to application '{appName}'";

            try
            {
                await AddGroupAsyncInternal(appName, newGroup);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Added new group '{newGroup.Name}' to application '{appName}'",
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

        public async Task RemoveGroupAsync(string appName, string groupName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));

            appName = appName?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            var logCtx = $"Removing group '{groupName}' from application '{appName}'";

            try
            {
                await RemoveGroupAsyncInternal(appName, groupName);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Removed group '{groupName}' from application '{appName}'",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task UpdateGroupAsync(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));
            RaiseArgumentNullException.IfIsNull(groupUpdates, nameof(groupUpdates));
            RaiseArgumentException.If(groupUpdates.Name.HasValue && string.IsNullOrWhiteSpace(groupUpdates.Name.Value));

            appName = appName?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            var logCtx = $"Updating group '{groupName}' in application '{appName}'";

            try
            {
                groupUpdates.Name.Do(x => groupUpdates.Name = x.ToLowerInvariant());
                await UpdateGroupAsyncInternal(appName, groupName, groupUpdates);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Updated group '{groupName}' in application '{appName}'",
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
            var logCtx = $"Retrieving all users from application '{appName}'";

            try
            {
                return await GetUsersAsyncInternal(appName, null, null, null);
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecUser[]);
            }
        }

        public async Task<SecUser> GetUserByIdAsync(string appName, long userId)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));

            appName = appName?.ToLowerInvariant();
            var logCtx = $"Retrieving user #{userId} from application '{appName}'";

            try
            {
                var users = await GetUsersAsyncInternal(appName, userId, null, null);
                if (users.Length == 0)
                {
                    throw new SecUserNotFoundException(appName, userId.ToString(CultureInfo.InvariantCulture));
                }
                return users[0];
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(SecUser);
            }
        }

        public async Task<SecUser> GetUserByLoginAsync(string appName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));

            appName = appName?.ToLowerInvariant();
            userLogin = userLogin?.ToLowerInvariant();
            var logCtx = $"Retrieving user '{userLogin}' from application '{appName}'";

            try
            {
                var users = await GetUsersAsyncInternal(appName, null, userLogin, null);
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
            var logCtx = $"Retrieving user '{userEmail}' from application '{appName}'";

            try
            {
                var users = await GetUsersAsyncInternal(appName, null, null, userEmail);
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

            appName = appName?.ToLowerInvariant();
            newUser.Login = newUser.Login.ToLowerInvariant();
            var logCtx = $"Adding new user '{newUser.Login}' to application '{appName}'";

            try
            {
                await AddUserAsyncInternal(appName, newUser);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Added new user '{newUser.Login}' to application '{appName}'",
                    Context = logCtx
                });
                return newUser.Id;
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

            appName = appName?.ToLowerInvariant();
            userLogin = userLogin?.ToLowerInvariant();
            var logCtx = $"Removing user '{userLogin}' from application '{appName}'";

            try
            {
                await RemoveUserAsyncInternal(appName, userLogin);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Removed user '{userLogin}' from application '{appName}'",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task UpdateUserAsync(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));
            RaiseArgumentNullException.IfIsNull(userUpdates, nameof(userUpdates));
            RaiseArgumentException.If(userUpdates.Login.HasValue && string.IsNullOrWhiteSpace(userUpdates.Login.Value), ErrorMessages.NullOrWhiteSpaceUserLogin);

            appName = appName?.ToLowerInvariant();
            userLogin = userLogin?.ToLowerInvariant();
            var logCtx = $"Updating user '{userLogin}' in application '{appName}'";

            try
            {
                userUpdates.Login.Do(x => userUpdates.Login = x.ToLowerInvariant());
                await UpdateUserAsyncInternal(appName, userLogin, userUpdates);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Updated user '{userLogin}' in application '{appName}'",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task RemoveUserFromGroupAsync(string appName, string userLogin, string groupName)
        {
            
        }

        public async Task AddUserToRoleAsync(string appName, string userLogin, string groupName, string roleName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(roleName), ErrorMessages.NullOrWhiteSpaceRoleName, nameof(roleName));

            appName = appName?.ToLowerInvariant();
            userLogin = userLogin?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            roleName = roleName?.ToLowerInvariant();
            var logCtx = $"Adding user '{userLogin}' to group '{groupName}' and role '{roleName}' in application '{appName}'";

            try
            {
                await AddUserToRoleAsyncInternal(appName, userLogin, groupName, roleName);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Added user '{userLogin}' to group '{groupName}' in application '{appName}'",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task RemoveUserFromRoleAsync(string appName, string userLogin, string groupName, string roleName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(groupName), ErrorMessages.NullOrWhiteSpaceGroupName, nameof(groupName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(roleName), ErrorMessages.NullOrWhiteSpaceRoleName, nameof(roleName));

            appName = appName?.ToLowerInvariant();
            userLogin = userLogin?.ToLowerInvariant();
            groupName = groupName?.ToLowerInvariant();
            roleName = roleName?.ToLowerInvariant();
            var logCtx = $"Removing user '{userLogin}' from group '{groupName}' and role '{roleName}' in application '{appName}'";

            try
            {
                await RemoveUserFromRoleAsyncInternal(appName, userLogin, groupName, roleName);
                Log.Warn(new LogMessage
                {
                    ShortMessage = $"Removed user '{userLogin}' from group '{groupName}' in application '{appName}'",
                    Context = logCtx
                });
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public Task<long> AddUserClaimAsync(string appName, string userLogin, SecClaim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserClaimAsync(string appName, string userLogin, string serializedClaim)
        {
            throw new NotImplementedException();
        }

        #endregion Users

        #region Roles

        public Task<SecRole[]> GetRolesAsync(string appName)
        {
            throw new NotImplementedException();
        }

        public Task<SecRole[]> GetRolesAsync(string appName, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<SecRole> GetRoleByIdAsync(string appName, long roleId)
        {
            throw new NotImplementedException();
        }

        public Task<SecRole> GetRoleByNameAsync(string appName, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<SecRole> GetRoleByNameAsync(string appName, string groupName, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRoleAsync(string appName, string groupName, SecRole newRole)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRoleAsync(string appName, string groupName, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoleAsync(string appName, string groupName, string roleName, SecRoleUpdates roleUpdates)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Contexts

        public Task<SecContext[]> GetContextsAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            return GetContextsAsyncInternal(appName.ToLowerInvariant());
        }

        #endregion Contexts

        #region Objects

        public Task<SecObject[]> GetObjectsAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            return GetObjectsAsyncInternal(appName.ToLowerInvariant(), null);
        }

        public Task<SecObject[]> GetObjectsAsync(string appName, string contextName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            return GetObjectsAsyncInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant());
        }

        #endregion Objects

        #region Entries

        public Task<SecEntry[]> GetEntriesAsync(string appName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            return GetEntriesAsyncInternal(appName.ToLowerInvariant(), null, null, null);
        }

        public Task<SecEntry[]> GetEntriesAsync(string appName, string contextName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            return GetEntriesAsyncInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), null, null);
        }

        public Task<SecEntry[]> GetEntriesForUserAsync(string appName, string contextName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));

            return GetEntriesAsyncInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), null, userLogin.ToLowerInvariant());
        }

        public Task<SecEntry[]> GetEntriesForObjectAsync(string appName, string contextName, string objectName)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            Raise<ArgumentException>.IfIsEmpty(objectName);

            return GetEntriesAsyncInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), null);
        }

        public Task<SecEntry[]> GetEntriesForObjectAndUserAsync(string appName, string contextName, string objectName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(contextName), ErrorMessages.NullOrWhiteSpaceContextName, nameof(contextName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(objectName), ErrorMessages.NullOrWhiteSpaceObjectName, nameof(objectName));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(userLogin), ErrorMessages.NullOrWhiteSpaceUserLogin, nameof(userLogin));

            return GetEntriesAsyncInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), userLogin.ToLowerInvariant());
        }

        public async Task<long> AddEntryAsync(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName, string roleName)
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
                var entryId = await AddEntryAsyncInternal(appName.ToLowerInvariant(), secContext, secObject, userLogin, groupName, roleName);
                Log.Warn(() => new LogMessage
                {
                    ShortMessage = $"Security entry for object '{secObject.Name}' in context '{secContext.Name}' has been added for '{userLogin ?? groupName}'",
                    Context = logCtx
                });
                return entryId;
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
                return default(long);
            }
        }

        public async Task RemoveEntryAsync(string appName, string contextName, string objectName, string userLogin, string groupName, string roleName)
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
                await RemoveEntryAsyncInternal(appName.ToLowerInvariant(), contextName.ToLowerInvariant(), objectName.ToLowerInvariant(), userLogin, groupName, roleName);
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

        protected abstract Task<SecGroup[]> GetGroupsAsyncInternal(string appName, int? groupId, string groupName);

        protected abstract Task AddGroupAsyncInternal(string appName, SecGroup newGroup);

        protected abstract Task RemoveGroupAsyncInternal(string appName, string groupName);

        protected abstract Task UpdateGroupAsyncInternal(string appName, string groupName, SecGroupUpdates groupUpdates);

        protected abstract Task<SecUser[]> GetUsersAsyncInternal(string appName, long? userId, string userLogin, string userEmail);

        protected abstract Task AddUserAsyncInternal(string appName, SecUser newUser);

        protected abstract Task RemoveUserAsyncInternal(string appName, string userLogin);

        protected abstract Task UpdateUserAsyncInternal(string appName, string userLogin, SecUserUpdates userUpdates);

        protected abstract Task AddUserToRoleAsyncInternal(string appName, string userLogin, string groupName, string roleName);

        protected abstract Task RemoveUserFromRoleAsyncInternal(string appName, string userLogin, string groupName, string roleName);

        protected abstract Task<SecContext[]> GetContextsAsyncInternal(string appName);

        protected abstract Task<SecObject[]> GetObjectsAsyncInternal(string appName, string contextName);

        protected abstract Task<SecEntry[]> GetEntriesAsyncInternal(string appName, string contextName, string objectName, string userLogin);

        protected abstract Task<long> AddEntryAsyncInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName, string roleName);

        protected abstract Task RemoveEntryAsyncInternal(string appName, string contextName, string objectName, string userLogin, string groupName, string roleName);

        #endregion Abstract Methods
    }
}
