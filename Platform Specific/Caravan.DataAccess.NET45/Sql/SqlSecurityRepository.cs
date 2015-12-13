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

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;
using Finsa.CodeServices.Common;
using PommaLabs.Thrower;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql
{
    internal sealed class SqlSecurityRepository : AbstractSecurityRepository<SqlSecurityRepository>
    {
        #region Constants

        private const string UnspecifiedString = "...";

        #endregion Constants

        private readonly SqlDbContext _dbContext;
        private bool _disposed;

        public SqlSecurityRepository(ICaravanLog log, SqlDbContext dbContext)
            : base(log)
        {
            RaiseArgumentNullException.IfIsNull(dbContext, nameof(dbContext));
            _dbContext = dbContext;
        }

        public override void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            // NOTA BENE: Non chiudo il contesto perché mi viene passato al costruttore, farà la Dispose chi me lo passa.
            //_dbContext?.Dispose();

            _disposed = true;
        }

        #region Apps

        protected override async Task<SecApp[]> GetAppsAsyncInternal(string appName)
        {
            var q = _dbContext.SecApps
                .Include(a => a.Groups)
                .Include("Contexts.Objects")
                .Include(a => a.LogSettings);

            if (appName != null)
            {
                q = q.Where(a => a.Name == appName);
            }

            return await q.ProjectTo<SecApp>().ToArrayAsync();
        }

        protected override async Task AddAppAsyncInternal(SecApp app)
        {
            if (await _dbContext.SecApps.AnyAsync(a => a.Name == app.Name))
            {
                throw new SecAppExistingException(app.Name);
            }

            var sqlApp = _dbContext.SecApps.Add(new SqlSecApp
            {
                Name = app.Name,
                Description = app.Description ?? UnspecifiedString
            });

            await _dbContext.SaveChangesAsync();
            app.Id = sqlApp.Id;
        }

        #endregion Apps

        #region Groups

        protected override async Task<SecGroup[]> GetGroupsAsyncInternal(string appName, int? groupId, string groupName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecGroups
                    .Include(g => g.App)
                    .Include(g => g.Roles)
                    .Where(g => g.App.Id == appId);

                if (groupId != null)
                {
                    q = q.Where(g => g.Id == groupId);
                }
                if (groupName != null)
                {
                    q = q.Where(g => g.Name == groupName);
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecGroup>)
                    .ToArray();
            }
        }

        protected override async Task AddGroupAsyncInternal(string appName, SecGroup newGroup)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                if (await ctx.SecGroups.AnyAsync(g => g.AppId == appId && g.Name == newGroup.Name))
                {
                    throw new SecGroupExistingException(appName, newGroup.Name);
                }

                var sqlGroup = ctx.SecGroups.Add(new SqlSecGroup
                {
                    AppId = appId,
                    Name = newGroup.Name,
                    Description = newGroup.Description ?? UnspecifiedString,
                    Notes = newGroup.Notes ?? UnspecifiedString
                });

                await ctx.SaveChangesAsync();
                newGroup.Id = sqlGroup.Id;
            }
        }

        protected override async Task RemoveGroupAsyncInternal(string appName, string groupName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var sqlGroup = await GetGroupByNameAsync(ctx, appId, appName, groupName);

                // La chiamata sopra mi assicura che il gruppo ci sia.
                ctx.SecGroups.Remove(sqlGroup);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task UpdateGroupAsyncInternal(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var sqlGroup = await GetGroupByNameAsync(ctx, appId, appName, groupName);

                // La chiamata sopra mi assicura che il gruppo ci sia.
                groupUpdates.Name.Do(async x =>
                {
                    if (sqlGroup.Name != x && await ctx.SecGroups.AnyAsync(g => g.AppId == sqlGroup.AppId && g.Name == x))
                    {
                        throw new SecGroupExistingException(appName, x);
                    }
                    sqlGroup.Name = x;
                });
                groupUpdates.Description.Do(x => sqlGroup.Description = x);
                groupUpdates.Notes.Do(x => sqlGroup.Notes = x);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task<IQueryable<SecUser>> QueryUsersInGroupAsyncInternal(string appName, string groupName)
        {
            var appId = await GetAppIdByNameAsync(_dbContext, appName);
            var sqlGroup = await GetGroupByNameAsync(_dbContext, appId, appName, groupName);

            // La chiamata sopra mi assicura che il ruolo ci sia.
            return _dbContext.SecUsers
                .Where(u => u.AppId == appId)
                .Where(u => u.Roles.Any(r => r.GroupId == sqlGroup.Id))
                .ProjectTo<SecUser>();
        }

        #endregion Groups

        #region Roles

        protected override async Task<SecRole[]> GetRolesAsyncInternal(string appName, string groupName, string roleName, int? roleId)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecRoles
                    .Include(r => r.Group.App)
                    .Where(r => r.Group.App.Id == appId);

                if (groupName != null)
                {
                    var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);
                    q = q.Where(r => r.Id == group.Id);
                }
                if (roleName != null)
                {
                    q = q.Where(r => r.Name == roleName);
                }
                if (roleId != null)
                {
                    q = q.Where(r => r.Id == roleId);
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecRole>)
                    .ToArray();
            }
        }

        protected override async Task AddRoleAsyncInternal(string appName, string groupName, SecRole newRole)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);

                if (await ctx.SecRoles.AnyAsync(r => r.GroupId == group.Id && r.Name == newRole.Name))
                {
                    throw new SecRoleExistingException(appName, groupName, newRole.Name);
                }

                var sqlRole = ctx.SecRoles.Add(new SqlSecRole
                {
                    GroupId = group.Id,
                    Name = newRole.Name,
                    Description = newRole.Description ?? UnspecifiedString,
                    Notes = newRole.Notes ?? UnspecifiedString
                });

                await ctx.SaveChangesAsync();
                newRole.Id = sqlRole.Id;
            }
        }

        protected override async Task RemoveRoleAsyncInternal(string appName, string groupName, string roleName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);
                var sqlRole = await GetRoleByNameAsync(ctx, appName, group.Id, groupName, roleName);

                // La chiamata sopra mi assicura che il ruolo ci sia.
                ctx.SecRoles.Remove(sqlRole);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task UpdateRoleAsyncInternal(string appName, string groupName, string roleName, SecRoleUpdates roleUpdates)
        {
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);
                var sqlRole = await GetRoleByNameAsync(ctx, appName, group.Id, groupName, roleName);

                // La chiamata sopra mi assicura che il ruolo ci sia.
                roleUpdates.Name.Do(async x =>
                {
                    if (sqlRole.Name != x && await ctx.SecRoles.AnyAsync(r => r.GroupId == group.Id && r.Name == x))
                    {
                        throw new SecRoleExistingException(appName, groupName, x);
                    }
                    sqlRole.Name = x;
                });
                roleUpdates.Description.Do(x => sqlRole.Description = x);
                roleUpdates.Notes.Do(x => sqlRole.Notes = x);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task<IQueryable<SecUser>> QueryUsersInRoleAsyncInternal(string appName, string groupName, string roleName)
        {
            var appId = await GetAppIdByNameAsync(_dbContext, appName);
            var group = await GetGroupByNameAsync(_dbContext, appId, appName, groupName);
            var sqlRole = await GetRoleByNameAsync(_dbContext, appName, group.Id, groupName, roleName);

            // La chiamata sopra mi assicura che il ruolo ci sia.
            return _dbContext.SecUsers
                .Where(u => u.AppId == appId)
                .Where(u => u.Roles.Any(r => r.Id == sqlRole.Id))
                .ProjectTo<SecUser>();
        }

        #endregion Roles

        #region Users

        protected override async Task<SecUser[]> GetUsersAsyncInternal(string appName, long? userId, string userLogin, string userEmail)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecUsers
                    .Include(u => u.App)
                    .Include(u => u.Claims)
                    .Include("Roles.Group")
                    .Where(u => u.AppId == appId);

                if (userId != null)
                {
                    q = q.Where(u => u.Id == userId);
                }
                if (userLogin != null)
                {
                    q = q.Where(u => u.Login == userLogin);
                }
                if (userEmail != null)
                {
                    q = q.Where(u => u.Email == userEmail);
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecUser>)
                    .ToArray();
            }
        }

        protected override async Task<IQueryable<SecUser>> QueryUsersAsyncInternal(string appName)
        {
            var appId = await GetAppIdByNameAsync(_dbContext, appName);

            // La chiamata sopra mi assicura che il ruolo ci sia.
            return _dbContext.SecUsers
                .Where(u => u.AppId == appId)
                .ProjectTo<SecUser>();
        }

        protected override async Task AddUserAsyncInternal(string appName, SecUser newUser)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                if (await ctx.SecUsers.AnyAsync(u => u.AppId == appId && u.Login == newUser.Login))
                {
                    throw new SecUserExistingException(appName, newUser.Login);
                }

                var sqlUser = ctx.SecUsers.Add(new SqlSecUser
                {
                    AppId = appId,
                    Active = newUser.Active,
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    PasswordHash = newUser.PasswordHash,
                    Login = newUser.Login
                });

                await ctx.SaveChangesAsync();
                newUser.Id = sqlUser.Id;
            }
        }

        protected override async Task RemoveUserAsyncInternal(string appName, string userLogin)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var sqlUser = await GetUserByLoginAsync(ctx, appId, appName, userLogin);

                // La chiamata sopra mi assicura che l'utente ci sia.
                ctx.SecUsers.Remove(sqlUser);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task UpdateUserAsyncInternal(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var sqlUser = await GetUserByLoginAsync(ctx, appId, appName, userLogin);

                // La chiamata sopra mi assicura che l'utente ci sia.
                userUpdates.Login.Do(async x =>
                {
                    if (userLogin != x && await ctx.SecUsers.AnyAsync(u => u.AppId == sqlUser.AppId && u.Login == x))
                    {
                        throw new SecUserExistingException(appName, x);
                    }
                    sqlUser.Login = x;
                });
                userUpdates.PasswordHash.Do(x => sqlUser.PasswordHash = x);
                userUpdates.Active.Do(x => sqlUser.Active = x);
                userUpdates.FirstName.Do(x => sqlUser.FirstName = x);
                userUpdates.LastName.Do(x => sqlUser.LastName = x);
                userUpdates.Email.Do(x => sqlUser.Email = x);
                userUpdates.EmailConfirmed.Do(x => sqlUser.EmailConfirmed = x);
                userUpdates.PhoneNumber.Do(x => sqlUser.PhoneNumber = x);
                userUpdates.PhoneNumberConfirmed.Do(x => sqlUser.PhoneNumberConfirmed = x);
                userUpdates.SecurityStamp.Do(x => sqlUser.SecurityStamp = x);
                userUpdates.LockoutEnabled.Do(x => sqlUser.LockoutEnabled = x);
                userUpdates.LockoutEndDate.Do(x => sqlUser.LockoutEndDate = x.DateTime.ToUniversalTime());
                userUpdates.AccessFailedCount.Do(x => sqlUser.AccessFailedCount = x);
                userUpdates.TwoFactorAuthenticationEnabled.Do(x => sqlUser.TwoFactorAuthenticationEnabled = x);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task AddUserToRoleAsyncInternal(string appName, string userLogin, string groupName, string roleName)
        {
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);
                var role = await GetRoleByNameAsync(ctx, appName, group.Id, groupName, roleName);

                // Le chiamate sopra mi assicurano che utente, gruppo e ruolo esistano.
                await ctx.Entry(user).Collection(u => u.Roles).LoadAsync();
                if (user.Roles.Any(r => r.Id == role.Id))
                {
                    throw new SecUserExistingException(appName, groupName, roleName, userLogin);
                }
                user.Roles.Add(role);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task RemoveUserFromRoleAsyncInternal(string appName, string userLogin, string groupName, string roleName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);
                var role = await GetRoleByNameAsync(ctx, appName, group.Id, groupName, roleName);

                // Le chiamate sopra mi assicurano che utente, gruppo e ruolo esistano.
                await ctx.Entry(user).Collection(u => u.Roles).LoadAsync();
                user.Roles.Remove(role);

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task AddUserClaimAsyncInternal(string appName, string userLogin, SecClaim claim)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);

                // Le chiamate sopra mi assicurano che utente e applicazione esistano.
                var sqlClaim = ctx.SecClaims.Add(new SqlSecClaim
                {
                    UserId = user.Id,
                    Hash = claim.Hash,
                    Claim = claim.Claim
                });

                await ctx.SaveChangesAsync();
                claim.Id = sqlClaim.Id;
            }
        }

        protected override async Task RemoveUserClaimAsyncInternal(string appName, string userLogin, string serializedClaimHash)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);

                // Le chiamate sopra mi assicurano che utente e applicazione esistano.
                var claim = ctx.SecClaims.FirstOrDefault(c => c.UserId == user.Id && c.Hash == serializedClaimHash);
                ctx.SecClaims.Remove(claim);

                await ctx.SaveChangesAsync();
            }
        }

        #endregion Users

        #region Contexts

        protected override async Task<SecContext[]> GetContextsAsyncInternal(string appName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecContexts
                    .Include(c => c.Objects)
                    .Where(c => c.App.Id == appId);

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecContext>)
                    .ToArray();
            }
        }

        #endregion Contexts

        #region Objects

        protected override async Task<SecObject[]> GetObjectsAsyncInternal(string appName, string contextName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecObjects
                    .Include(o => o.SecEntries)
                    .Where(o => o.Context.AppId == appId);

                if (contextName != null)
                {
                    q = q.Where(o => o.Context.Name == contextName);
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecObject>)
                    .ToArray();
            }
        }

        #endregion Objects

        #region Entries

        protected override async Task<SecEntry[]> GetEntriesAsyncInternal(string appName, string contextName, string objectName, string userLogin)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecEntries
                    .Include(e => e.Object.Context)
                    .Include(e => e.User)
                    .Include(e => e.Group)
                    .Where(e => e.Object.Context.AppId == appId);

                if (contextName != null)
                {
                    q = q.Where(e => e.Object.Context.Name == contextName);
                }
                if (objectName != null)
                {
                    q = q.Where(e => e.Object.Name == objectName);
                }
                if (userLogin != null)
                {
                    var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                    var groupIds = user.Roles.Select(r => r.GroupId).Distinct().ToArray();
                    q = q.Where(e => e.UserId == user.Id || groupIds.Contains(e.Group.Id));
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecEntry>)
                    .ToArray();
            }
        }

        protected override async Task<long> AddEntryAsyncInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName, string roleName)
        {
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var dbContext = await ctx.SecContexts.FirstOrDefaultAsync(c => c.AppId == appId && c.Name == secContext.Name);
                if (dbContext == null)
                {
                    dbContext = ctx.SecContexts.Add(new SqlSecContext
                    {
                        AppId = appId,
                        Name = secContext.Name,
                        Description = secContext.Description
                    });
                    await ctx.SaveChangesAsync(); // Required, we need the generated context ID.
                }
                else
                {
                    dbContext.Description = secContext.Description;
                }

                var dbObject = await ctx.SecObjects.FirstOrDefaultAsync(o => o.Context.AppId == appId && o.ContextId == dbContext.Id && o.Name == secObject.Name);
                if (dbObject == null)
                {
                    dbObject = ctx.SecObjects.Add(new SqlSecObject
                    {
                        ContextId = dbContext.Id,
                        Name = secObject.Name,
                        Description = secObject.Description,
                        Type = secObject.Type
                    });
                    await ctx.SaveChangesAsync(); // Required, we need the generated context ID.
                }
                else
                {
                    dbObject.Description = secObject.Description;
                    dbObject.Type = secObject.Type;
                }

                var q = ctx.SecEntries
                    .Where(e => e.Object.Context.AppId == appId)
                    .Where(e => e.Object.ContextId == dbContext.Id && e.ObjectId == dbObject.Id);

                long? userId = null;
                if (userLogin != null)
                {
                    var sqlUser = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                    userId = sqlUser.Id;
                    q = q.Where(e => e.UserId == userId);
                }

                int? groupId = null;
                int? roleId = null;
                if (groupName != null)
                {
                    var sqlGroup = await GetGroupByNameAsync(ctx, appId, appName, groupName);

                    if (roleName != null)
                    {
                        var sqlRole = await GetRoleByNameAsync(ctx, appName, sqlGroup.Id, groupName, roleName);
                        roleId = sqlRole.Id;
                        q = q.Where(e => e.RoleId == roleId);
                    }
                    else
                    {
                        groupId = sqlGroup.Id;
                        q = q.Where(e => e.GroupId == groupId);
                    }
                }

                if (q.Any())
                {
                    throw new SecEntryExistingException(appName, userLogin, groupName, roleName, dbContext.Name, dbObject.Name);
                }

                var secEntry = ctx.SecEntries.Add(new SqlSecEntry
                {
                    UserId = userId,
                    GroupId = groupId,
                    RoleId = roleId,
                    ObjectId = dbObject.Id
                });

                await ctx.SaveChangesAsync();
                return secEntry.Id;
            }
        }

        protected override async Task RemoveEntryAsyncInternal(string appName, string contextName, string objectName, string userLogin, string groupName, string roleName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                //var entry = ctx.SecEntries.FirstOrDefault(e => e.AppId == appId && e.Context.Name == contextName && e.Object.Name == objectName && (e.User == null || e.User.Login == userLogin) && (e.Group == null || e.Group.Name == groupName));

                var q = ctx.SecEntries
                    .Include(e => e.Object)
                    .Include(e => e.User)
                    .Include(e => e.Group)
                    .Include(e => e.Role);

                if (userLogin != null)
                {
                    var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                    var groupIds = user.Roles.Select(r => r.GroupId).Distinct().ToArray();
                    q = q.Where(e => e.UserId == user.Id || groupIds.Contains(e.Group.Id));
                }
                if (q != null)
                {
                    ctx.SecEntries.Remove(q.First());
                }
                await ctx.SaveChangesAsync();
            }
        }

        #endregion Entries

        #region Private Methods

        private static async Task<int> GetAppIdByNameAsync(SqlDbContext ctx, string appName)
        {
            var appId = await ctx.SecApps
                .Where(a => a.Name == appName)
                .Select(a => (int?) a.Id)
                .FirstOrDefaultAsync();

            if (appId == null)
            {
                throw new SecAppNotFoundException(appName);
            }

            return appId.Value;
        }

        private static async Task<SqlSecGroup> GetGroupByNameAsync(SqlDbContext ctx, int appId, string appName, string groupName)
        {
            var group = await ctx.SecGroups.FirstOrDefaultAsync(g => g.AppId == appId && g.Name == groupName);
            if (group == null)
            {
                throw new SecGroupNotFoundException(appName, groupName);
            }
            return group;
        }

        private static async Task<SqlSecRole> GetRoleByNameAsync(SqlDbContext ctx, string appName, int groupId, string groupName, string roleName)
        {
            var role = await ctx.SecRoles.FirstOrDefaultAsync(r => r.GroupId == groupId && r.Name == roleName);
            if (role == null)
            {
                throw new SecRoleNotFoundException(appName, groupName, roleName);
            }
            return role;
        }

        private static async Task<SqlSecUser> GetUserByLoginAsync(SqlDbContext ctx, int appId, string appName, string userLogin)
        {
            var user = await ctx.SecUsers.FirstOrDefaultAsync(u => u.AppId == appId && u.Login == userLogin);
            if (user == null)
            {
                throw new SecUserNotFoundException(appName, userLogin);
            }
            return user;
        }

        #endregion Private Methods
    }
}