using AutoMapper;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Core;
using Finsa.CodeServices.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;

namespace Finsa.Caravan.DataAccess.Sql
{
    internal sealed class SqlSecurityRepository : AbstractSecurityRepository<SqlSecurityRepository>
    {
        public SqlSecurityRepository(ICaravanLog log)
            : base(log)
        {
        }

        #region Apps

        protected override Task<SecApp[]> GetAppsAsyncInternal(string appName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecApps
                    .Include(a => a.Users)
                    .Include(a => a.Groups)
                    .Include("Contexts.Objects")
                    .Include(a => a.LogSettings);

                if (appName != null)
                {
                    q = q.Where(a => a.Name == appName);
                }

                return Task.FromResult(q.AsEnumerable()
                    .Select(Mapper.Map<SecApp>)
                    .ToArray());
            }
        }

        protected override async Task AddAppAsyncInternal(SecApp app)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                if (await ctx.SecApps.AnyAsync(a => a.Name == app.Name))
                {
                    throw new SecAppExistingException(app.Name);
                }
                var sqlApp = ctx.SecApps.Add(new SqlSecApp
                {
                    Name = app.Name,
                    Description = app.Description
                });
                await ctx.SaveChangesAsync();
                app.Id = sqlApp.Id;
            }
        }

        #endregion Apps

        #region Groups

        protected override async Task<SecGroup[]> GetGroupsInternal(string appName, string groupName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecGroups
                    .Include(g => g.App)
                    .Include(g => g.Users)
                    .Include(g => g.Roles)
                    .Where(g => g.App.Id == appId);

                if (groupName != null)
                {
                    q = q.Where(g => g.Name == groupName);
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecGroup>)
                    .ToArray();
            }
        }

        protected override async Task AddGroupInternal(string appName, SecGroup newGroup)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var added = false;
                if (!ctx.SecGroups.Any(g => g.AppId == appId && g.Name == newGroup.Name))
                {
                    ctx.SecGroups.Add(new SqlSecGroup
                    {
                        AppId = appId,
                        Description = newGroup.Description ?? string.Empty,
                        Name = newGroup.Name,
                        Notes = newGroup.Notes ?? string.Empty
                    });
                    added = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task RemoveGroupInternal(string appName, string groupName)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var removed = false;
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var grp = ctx.SecGroups.FirstOrDefault(g => g.AppId == appId && g.Name == groupName);
                if (grp != null)
                {
                    ctx.SecGroups.Remove(grp);
                    removed = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task UpdateGroupInternal(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var updated = false;
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var grp = ctx.SecGroups.FirstOrDefault(g => g.App.Id == appId && g.Name == groupName);
                if (grp != null)
                {
                    groupUpdates.Name.Do(x =>
                    {
                        if (grp.Name != x && ctx.SecGroups.Any(g => g.AppId == grp.AppId && g.Name == x))
                        {
                            throw new SecGroupExistingException(appName, groupName);
                        }
                        grp.Name = x;
                    });
                    groupUpdates.Description.Do(x => grp.Description = x);
                    groupUpdates.Notes.Do(x => grp.Notes = x);
                    updated = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        #endregion Groups

        #region Users

        protected override async Task<SecUser[]> GetUsersInternal(string appName, string userLogin, string userEmail)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var q = ctx.SecUsers.Include(u => u.App).Include(u => u.Groups).Where(u => u.AppId == appId);
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

        protected override async Task AddUserInternal(string appName, SecUser newUser)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var added = false;
                if (!ctx.SecUsers.Any(u => u.AppId == appId && u.Login == newUser.Login))
                {
                    ctx.SecUsers.Add(new SqlSecUser
                    {
                        AppId = appId,
                        Active = newUser.Active,
                        Email = newUser.Email,
                        FirstName = newUser.FirstName,
                        PasswordHash = newUser.PasswordHash,
                        LastName = newUser.LastName,
                        Login = newUser.Login
                    });
                    added = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task RemoveUserInternal(string appName, string userLogin)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var removed = false;
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = ctx.SecUsers.FirstOrDefault(us => us.App.Id == appId && us.Login == userLogin);
                if (user != null)
                {
                    ctx.SecUsers.Remove(user);
                    removed = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task UpdateUserInternal(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var updated = false;
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = ctx.SecUsers.FirstOrDefault(u => u.App.Id == appId && u.Login == userLogin);
                if (user != null)
                {
                    userUpdates.Login.Do(x =>
                    {
                        if (userLogin != x && ctx.SecUsers.Any(u => u.AppId == user.AppId && u.Login == x))
                        {
                            throw new SecUserExistingException(appName, userLogin);
                        }
                        user.Login = x;
                    });
                    userUpdates.FirstName.Do(x => user.FirstName = x);
                    userUpdates.LastName.Do(x => user.LastName = x);
                    userUpdates.Email.Do(x => user.Email = x);
                    userUpdates.Active.Do(x => user.Active = x);
                    updated = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task AddUserToGroupInternal(string appName, string userLogin, string groupName)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);
                var added = false;
                if (!group.Users.Contains(user))
                {
                    group.Users.Add(user);
                    added = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task RemoveUserFromGroupInternal(string appName, string userLogin, string groupName)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                var group = await GetGroupByNameAsync(ctx, appId, appName, groupName);
                var removed = false;
                if (group.Users.Contains(user))
                {
                    group.Users.Remove(user);
                    removed = true;
                }
                await ctx.SaveChangesAsync();
            }
        }

        #endregion Users

        #region Contexts

        protected override async Task<SecContext[]> GetContextsInternal(string appName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var q = ctx.SecContexts.Include(c => c.Objects);

                q = q.Where(c => c.App.Id == appId);

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecContext>)
                    .ToArray();
            }
        }

        #endregion Contexts

        #region Objects

        protected override async Task<SecObject[]> GetObjectsInternal(string appName, string contextName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecObjects
                    .Include(o => o.SecEntries);

                if (appName != null)
                {
                    var appId = await GetAppIdByNameAsync(ctx, appName);
                    q = q.Where(o => o.Context.AppId == appId);
                }

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

        protected override async Task<SecEntry[]> GetEntriesInternal(string appName, string contextName, string objectName, string userLogin)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);

                var q = ctx.SecEntries
                    .Include(e => e.Object.Context)
                    .Include(e => e.User)
                    .Include(e => e.Group);

                if (appName != null)
                {
                    q = q.Where(e => e.Object.Context.AppId == appId);
                }
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
                    var groupIds = user.Groups.Select(g => g.Id).ToList();
                    q = q.Where(e => e.UserId == user.Id || groupIds.Contains(e.Group.Id));
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecEntry>)
                    .ToArray();
            }
        }

        protected override async Task<long> AddEntryInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                var dbContext = ctx.SecContexts.FirstOrDefault(c => c.AppId == appId && c.Name == secContext.Name);
                if (dbContext == null)
                {
                    dbContext = new SqlSecContext
                    {
                        AppId = appId,
                        Name = secContext.Name,
                        Description = secContext.Description
                    };
                    ctx.SecContexts.Add(dbContext);
                    await ctx.SaveChangesAsync(); // Required, we need the generated context ID.
                }
                else
                {
                    dbContext.Description = secContext.Description;
                }
                var dbObject = ctx.SecObjects.FirstOrDefault(o => o.Context.AppId == appId && o.ContextId == dbContext.Id && o.Name == secObject.Name);
                if (dbObject == null)
                {
                    dbObject = new SqlSecObject
                    {
                        ContextId = dbContext.Id,
                        Name = secObject.Name,
                        Description = secObject.Description,
                        Type = secObject.Type
                    };
                    ctx.SecObjects.Add(dbObject);
                    await ctx.SaveChangesAsync(); // Required, we need the generated context ID.
                }
                else
                {
                    dbObject.Description = secObject.Description;
                    dbObject.Type = secObject.Type;
                }
                var q = ctx.SecEntries.Where(e => e.Object.Context.AppId == appId && e.Object.ContextId == dbContext.Id && e.ObjectId == dbObject.Id);
                long? userId = null;
                if (!string.IsNullOrWhiteSpace(userLogin))
                {
                    userId = ctx.SecUsers.Where(u => u.AppId == appId && u.Login == userLogin).Select(u => (long?) u.Id).FirstOrDefault();
                    q = q.Where(e => e.UserId == userId);
                }
                int? groupId = null;
                if (!string.IsNullOrWhiteSpace(groupName))
                {
                    groupId = ctx.SecGroups.Where(g => g.AppId == appId && g.Name == groupName).Select(g => (int?) g.Id).FirstOrDefault();
                    q = q.Where(e => e.GroupId == groupId);
                }

                if (q.Any())
                {
                    
                    //added = true;
                }

                var secEntry = ctx.SecEntries.Add(new SqlSecEntry
                {
                    UserId = userId,
                    GroupId = groupId,
                    ObjectId = dbObject.Id
                });

                await ctx.SaveChangesAsync();
                return secEntry.Id;
            }
        }

        protected override async Task RemoveEntryInternal(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                var appId = await GetAppIdByNameAsync(ctx, appName);
                //var entry = ctx.SecEntries.FirstOrDefault(e => e.AppId == appId && e.Context.Name == contextName && e.Object.Name == objectName && (e.User == null || e.User.Login == userLogin) && (e.Group == null || e.Group.Name == groupName));

                var q = ctx.SecEntries.Include(e => e.Object).Include(e => e.User).Include(e => e.Group);

                if (userLogin != null)
                {
                    var user = await GetUserByLoginAsync(ctx, appId, appName, userLogin);
                    var groupIds = user.Groups.Select(g => g.Id).ToList();
                    q = q.Where(e => e.UserId == user.Id || groupIds.Contains(e.Group.Id));
                }
                var removed = false;
                if (q != null)
                {
                    ctx.SecEntries.Remove(q.First());
                    removed = true;
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
            var group = await ctx.SecGroups
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.AppId == appId && g.Name == groupName);

            if (group == null)
            {
                throw new SecGroupNotFoundException(appName, groupName);
            }

            return group;
        }

        private static async Task<SqlSecUser> GetUserByLoginAsync(SqlDbContext ctx, int appId, string appName, string userLogin)
        {
            var user = await ctx.SecUsers
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(u => u.AppId == appId && u.Login == userLogin);

            if (user == null)
            {
                throw new SecUserNotFoundException(appName, userLogin);
            }

            return user;
        }

        #endregion Private Methods
    }
}
