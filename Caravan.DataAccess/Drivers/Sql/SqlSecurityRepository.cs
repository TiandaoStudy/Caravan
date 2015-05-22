using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Models.Security.Exceptions;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.DataAccess.Drivers.Sql
{
    internal sealed class SqlSecurityRepository : AbstractSecurityRepository<SqlSecurityRepository>
    {
        #region Apps

        protected override SecApp[] GetAppsInternal()
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecApps.Include(a => a.Users).Include(a => a.Groups).Include("Contexts.Objects").Include(a => a.LogSettings);
                return q.AsEnumerable()
                    .Select(Mapper.Map<SecApp>)
                    .ToArray();
            }
        }

        protected override SecApp GetAppInternal(string appName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecApps.Include(a => a.Users).Include(a => a.Groups).Include("Contexts.Objects").Include(a => a.LogSettings);
                if (appName != null)
                {
                    q = q.Where(a => a.Name == appName);
                }
                return Mapper.Map<SecApp>(q.First());
            }
        }

        protected override bool AddAppInternal(SecApp app)
        {
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var added = false;
                if (!ctx.SecApps.Any(a => a.Name == app.Name))
                {
                    ctx.SecApps.Add(new SqlSecApp
                    {
                        Name = app.Name,
                        Description = app.Description
                    });
                    added = true;
                }
                ctx.SaveChanges();
                return added;
            }
        }

        #endregion Apps

        #region Groups

        protected override SecGroup[] GetGroupsInternal(string appName, string groupName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecGroups.Include(g => g.App).Include(g => g.Users);
                var appId = GetAppIdByName(ctx, appName);

                q = q.Where(g => g.App.Id == appId);

                if (groupName != null)
                {
                    q = q.Where(g => g.Name == groupName);
                }
                return q.AsEnumerable()
                    .Select(Mapper.Map<SecGroup>)
                    .ToArray();
            }
        }

        protected override bool AddGroupInternal(string appName, SecGroup newGroup)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                var added = false;
                if (!ctx.SecGroups.Any(g => g.AppId == appId && g.Name == newGroup.Name))
                {
                    ctx.SecGroups.Add(new SqlSecGroup
                    {
                        AppId = appId,
                        Description = newGroup.Description ?? String.Empty,
                        Name = newGroup.Name,
                        Notes = newGroup.Notes ?? String.Empty
                    });
                    added = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return added;
            }
        }

        protected override bool RemoveGroupInternal(string appName, string groupName)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var removed = false;
                var appId = GetAppIdByName(ctx, appName);
                var grp = ctx.SecGroups.FirstOrDefault(g => g.AppId == appId && g.Name == groupName);
                if (grp != null)
                {
                    ctx.SecGroups.Remove(grp);
                    removed = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return removed;
            }
        }

        protected override bool UpdateGroupInternal(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var updated = false;
                var appId = GetAppIdByName(ctx, appName);
                var grp = ctx.SecGroups.FirstOrDefault(g => g.App.Id == appId && g.Name == groupName);
                if (grp != null)
                {
                    groupUpdates.Name.Do(x =>
                    {
                        if (grp.Name != x && ctx.SecGroups.Any(g => g.AppId == grp.AppId && g.Name == x))
                        {
                            throw new SecGroupExistingException();
                        }
                        grp.Name = x;
                    });
                    groupUpdates.Description.Do(x => grp.Description = x);
                    groupUpdates.Notes.Do(x => grp.Notes = x);
                    updated = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return updated;
            }
        }

        #endregion Groups

        #region Users

        protected override SecUser[] GetUsersInternal(string appName, string userLogin, string userEmail)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = GetAppIdByName(ctx, appName);
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

        protected override bool AddUserInternal(string appName, SecUser newUser)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
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
                ctx.SaveChanges();
                trx.Complete();
                return added;
            }
        }

        protected override bool RemoveUserInternal(string appName, string userLogin)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var removed = false;
                var appId = GetAppIdByName(ctx, appName);
                var user = ctx.SecUsers.FirstOrDefault(us => us.App.Id == appId && us.Login == userLogin);
                if (user != null)
                {
                    ctx.SecUsers.Remove(user);
                    removed = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return removed;
            }
        }

        protected override bool UpdateUserInternal(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var updated = false;
                var appId = GetAppIdByName(ctx, appName);
                var user = ctx.SecUsers.FirstOrDefault(u => u.App.Id == appId && u.Login == userLogin);
                if (user != null)
                {
                    userUpdates.Login.Do(x =>
                    {
                        if (userLogin != x && ctx.SecUsers.Any(u => u.AppId == user.AppId && u.Login == x))
                        {
                            throw new SecUserExistingException();
                        }
                        user.Login = x;
                    });
                    userUpdates.FirstName.Do(x => user.FirstName = x);
                    userUpdates.LastName.Do(x => user.LastName = x);
                    userUpdates.Email.Do(x => user.Email = x);
                    userUpdates.Active.Do(x => user.Active = x);
                    updated = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return updated;
            }
        }

        protected override bool AddUserToGroupInternal(string appName, string userLogin, string groupName)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                var user = GetUserByLogin(ctx, appId, userLogin);
                var group = GetGroupByName(ctx, appId, groupName);
                var added = false;
                if (!group.Users.Contains(user))
                {
                    group.Users.Add(user);
                    added = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return added;
            }
        }

        protected override bool RemoveUserFromGroupInternal(string appName, string userLogin, string groupName)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                var user = GetUserByLogin(ctx, appId, userLogin);
                var group = GetGroupByName(ctx, appId, groupName);
                var removed = false;
                if (group.Users.Contains(user))
                {
                    group.Users.Remove(user);
                    removed = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return removed;
            }
        }

        #endregion Users

        #region Contexts

        protected override SecContext[] GetContextsInternal(string appName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                var q = ctx.SecContexts.Include(c => c.Objects);

                q = q.Where(c => c.App.Id == appId);

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecContext>)
                    .ToArray();
            }
        }

        #endregion Contexts

        #region Objects

        protected override SecObject[] GetObjectsInternal(string appName, string contextName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecObjects.Include(o => o.SecEntries);
                if (appName != null)
                {
                    q = q.Where(o => o.Context.App.Name == appName);
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

        protected override SecEntry[] GetEntriesInternal(string appName, string contextName, string objectName, string userLogin)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = GetAppIdByName(ctx, appName);

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
                    var user = GetUserByLoginWithGroups(ctx, appId, userLogin);
                    var groupIds = user.Groups.Select(g => g.Id).ToList();
                    q = q.Where(e => e.UserId == user.Id || groupIds.Contains(e.Group.Id));
                }

                return q.AsEnumerable()
                    .Select(Mapper.Map<SecEntry>)
                    .ToArray();
            }
        }

        protected override bool AddEntryInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
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
                    ctx.SaveChanges(); // Required, we need the generated context ID.
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
                    ctx.SaveChanges(); // Required, we need the generated context ID.
                }
                else
                {
                    dbObject.Description = secObject.Description;
                    dbObject.Type = secObject.Type;
                }
                var q = ctx.SecEntries.Where(e => e.Object.Context.AppId == appId && e.Object.ContextId == dbContext.Id && e.ObjectId == dbObject.Id);
                long? userId = null;
                if (!String.IsNullOrWhiteSpace(userLogin))
                {
                    userId = ctx.SecUsers.Where(u => u.AppId == appId && u.Login == userLogin).Select(u => (long?) u.Id).FirstOrDefault();
                    q = q.Where(e => e.UserId == userId);
                }
                int? groupId = null;
                if (!String.IsNullOrWhiteSpace(groupName))
                {
                    groupId = ctx.SecGroups.Where(g => g.AppId == appId && g.Name == groupName).Select(g => (int?) g.Id).FirstOrDefault();
                    q = q.Where(e => e.GroupId == groupId);
                }
                var added = false;
                if (!q.Any())
                {
                    var secEntry = new SqlSecEntry
                    {
                        UserId = userId,
                        GroupId = groupId,
                        ObjectId = dbObject.Id
                    };
                    ctx.SecEntries.Add(secEntry);
                    added = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return added;
            }
        }

        protected override bool RemoveEntryInternal(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                //var entry = ctx.SecEntries.FirstOrDefault(e => e.AppId == appId && e.Context.Name == contextName && e.Object.Name == objectName && (e.User == null || e.User.Login == userLogin) && (e.Group == null || e.Group.Name == groupName));

                var q = ctx.SecEntries.Include(e => e.Object).Include(e => e.User).Include(e => e.Group);

                if (userLogin != null)
                {
                    var user = GetUserByLoginWithGroups(ctx, appId, userLogin);
                    var groupIds = user.Groups.Select(g => g.Id).ToList();
                    q = q.Where(e => e.UserId == user.Id || groupIds.Contains(e.Group.Id));
                }
                var removed = false;
                if (q != null)
                {
                    ctx.SecEntries.Remove(q.First());
                    removed = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return removed;
            }
        }

        #endregion Entries

        #region Private Methods

        private static int GetAppIdByName(SqlDbContext ctx, string appName)
        {
            var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => (int?) a.Id).FirstOrDefault();
            if (appId == null)
            {
                throw new SecAppNotFoundException();
            }
            return appId.Value;
        }

        private static SqlSecGroup GetGroupByName(SqlDbContext ctx, int appId, string groupName)
        {
            var group = ctx.SecGroups.Include(g => g.Users).FirstOrDefault(g => g.AppId == appId && g.Name == groupName);
            if (group == null)
            {
                throw new SecGroupNotFoundException();
            }
            return group;
        }

        private static SqlSecUser GetUserByLogin(SqlDbContext ctx, int appId, string userLogin)
        {
            var user = ctx.SecUsers.FirstOrDefault(u => u.AppId == appId && u.Login == userLogin);
            if (user == null)
            {
                throw new SecUserNotFoundException();
            }
            return user;
        }

        private static SqlSecUser GetUserByLoginWithGroups(SqlDbContext ctx, int appId, string userLogin)
        {
            var user = ctx.SecUsers.Include(u => u.Groups).FirstOrDefault(u => u.AppId == appId && u.Login == userLogin);
            if (user == null)
            {
                throw new SecUserNotFoundException();
            }
            return user;
        }

        #endregion Private Methods
    }
}