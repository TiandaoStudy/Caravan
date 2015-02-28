using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using AutoMapper;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Models.Security.Exceptions;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Drivers.Sql
{
    internal sealed class SqlSecurityManager : SecurityManagerBase<SqlSecurityManager>
    {
        #region Constants

        private const int TrueInt = 1;
        private const int FalseInt = 0;

        #endregion Constants

        #region Apps

        protected override IList<SecApp> GetApps()
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecApps.Include(a => a.Users).Include(a => a.Groups).Include("Contexts.Objects").Include(a => a.LogSettings);
                return q.OrderBy(a => a.Name)
                    .AsEnumerable()
                    .Select(Mapper.Map<SecApp>)
                    .ToList();
            }
        }

        protected override SecApp GetApp(string appName)
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

        protected override bool DoAddApp(SecApp app)
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

        protected override IList<SecGroup> GetGroups(string appName, string groupName)
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
                return q.OrderBy(g => g.App.Name)
                    .ThenBy(g => g.Name)
                    .AsEnumerable()
                    .Select(Mapper.Map<SecGroup>)
                    .ToList();
            }
        }

        protected override bool DoAddGroup(string appName, SecGroup newGroup)
        {
            using (var trx = new TransactionScope())
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
                        IsAdmin = newGroup.IsAdmin ? TrueInt : FalseInt,
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

        protected override bool DoRemoveGroup(string appName, string groupName)
        {
            using (var trx = new TransactionScope())
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

        protected override bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
        {
            using (var trx = new TransactionScope())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var updated = false;
                var appId = GetAppIdByName(ctx, appName);
                var grp = ctx.SecGroups.FirstOrDefault(g => g.App.Id == appId && g.Name == groupName);
                if (grp != null)
                {
                    if (grp.Name != newGroup.Name && ctx.SecGroups.Any(g => g.AppId == grp.AppId && g.Name == newGroup.Name))
                    {
                        throw new SecGroupExistingException();
                    }
                    grp.Name = newGroup.Name;
                    grp.Description = newGroup.Description ?? String.Empty;
                    grp.IsAdmin = newGroup.IsAdmin ? TrueInt : FalseInt;
                    grp.Notes = newGroup.Notes ?? String.Empty;
                    updated = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return updated;
            }
        }

        #endregion Groups

        #region Users

        protected override IList<SecUser> GetUsers(string appName, string userLogin)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                var q = ctx.SecUsers.Include(u => u.App).Include(u => u.Groups).Where(u => u.AppId == appId);
                if (userLogin != null)
                {
                    q = q.Where(u => u.Login == userLogin);
                }
                return q.OrderBy(u => u.App.Name)
                    .ThenBy(u => u.Login)
                    .AsEnumerable()
                    .Select(Mapper.Map<SecUser>)
                    .ToList();
            }
        }

        protected override bool DoAddUser(string appName, SecUser newUser)
        {
            using (var trx = new TransactionScope())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                var added = false;
                if (!ctx.SecUsers.Any(u => u.AppId == appId && u.Login == newUser.Login))
                {
                    ctx.SecUsers.Add(new SqlSecUser
                    {
                        AppId = appId,
                        Active = newUser.Active ? TrueInt : FalseInt,
                        Email = newUser.Email,
                        FirstName = newUser.FirstName,
                        HashedPassword = newUser.HashedPassword,
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

        protected override bool DoRemoveUser(string appName, string userLogin)
        {
            using (var trx = new TransactionScope())
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

        protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
        {
            using (var trx = new TransactionScope())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var updated = false;
                var appId = GetAppIdByName(ctx, appName);
                var user = ctx.SecUsers.FirstOrDefault(u => u.App.Id == appId && u.Login == userLogin);
                if (user != null)
                {
                    if (userLogin != newUser.Login && ctx.SecUsers.Any(u => u.AppId == user.AppId && u.Login == newUser.Login))
                    {
                        throw new SecUserExistingException();
                    }
                    user.FirstName = newUser.FirstName;
                    user.LastName = newUser.LastName;
                    user.Email = newUser.Email;
                    user.Login = newUser.Login;
                    user.Active = newUser.Active ? TrueInt : FalseInt;
                    updated = true;
                }
                ctx.SaveChanges();
                trx.Complete();
                return updated;
            }
        }

        protected override bool DoAddUserToGroup(string appName, string userLogin, string groupName)
        {
            using (var trx = new TransactionScope())
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

        protected override bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName)
        {
            using (var trx = new TransactionScope())
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

        protected override IList<SecContext> GetContexts(string appName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                var q = ctx.SecContexts.Include(c => c.Objects);

                q = q.Where(c => c.App.Id == appId);

                return q.OrderBy(c => c.App.Name)
                    .ThenBy(c => c.Name)
                    .AsEnumerable()
                    .Select(Mapper.Map<SecContext>)
                    .ToList();
            }
        }

        #endregion Contexts

        #region Objects

        protected override IList<SecObject> GetObjects(string appName, string contextName)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.SecObjects.Include(o => o.SecEntries);
                if (appName != null)
                {
                    q = q.Where(o => o.App.Name == appName);
                }
                if (contextName != null)
                {
                    q = q.Where(o => o.Context.Name == contextName);
                }
                return q.OrderBy(o => o.App.Name)
                    .ThenBy(o => o.Context.Name)
                    .ThenBy(o => o.Name)
                    .AsEnumerable()
                    .Select(Mapper.Map<SecObject>)
                    .ToList();
            }
        }

        #endregion Objects

        #region Entries

        protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectName, string userLogin)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var appId = GetAppIdByName(ctx, appName);

                var q = ctx.SecEntries.Include(e => e.App).Include(e => e.Context).Include(e => e.Object).Include(e => e.User).Include(e => e.Group);
                if (appName != null)
                {
                    q = q.Where(e => e.AppId == appId);
                }
                if (contextName != null)
                {
                    q = q.Where(e => e.Context.Name == contextName);
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
                return q.OrderBy(e => e.Object.Name)
                    .AsEnumerable()
                    .Select(Mapper.Map<SecEntry>)
                    .ToList();
            }
        }

        protected override bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            using (var trx = new TransactionScope())
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
                var dbObject = ctx.SecObjects.FirstOrDefault(o => o.AppId == appId && o.ContextId == dbContext.Id && o.Name == secObject.Name);
                if (dbObject == null)
                {
                    dbObject = new SqlSecObject
                    {
                        AppId = appId,
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
                var q = ctx.SecEntries.Where(e => e.AppId == appId && e.ContextId == dbContext.Id && e.ObjectId == dbObject.Id);
                long? userId = null;
                if (!String.IsNullOrWhiteSpace(userLogin))
                {
                    userId = ctx.SecUsers.Where(u => u.AppId == appId && u.Login == userLogin).Select(u => (long?) u.Id).FirstOrDefault();
                    q = q.Where(e => e.UserId == userId);
                }
                long? groupId = null;
                if (!String.IsNullOrWhiteSpace(groupName))
                {
                    groupId = ctx.SecGroups.Where(g => g.AppId == appId && g.Name == groupName).Select(g => (long?) g.Id).FirstOrDefault();
                    q = q.Where(e => e.GroupId == groupId);
                }
                var added = false;
                if (!q.Any())
                {
                    var secEntry = new SqlSecEntry
                    {
                        AppId = appId,
                        UserId = userId,
                        GroupId = groupId,
                        ContextId = dbContext.Id,
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

        protected override bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            using (var trx = new TransactionScope())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var appId = GetAppIdByName(ctx, appName);
                //var entry = ctx.SecEntries.FirstOrDefault(e => e.AppId == appId && e.Context.Name == contextName && e.Object.Name == objectName && (e.User == null || e.User.Login == userLogin) && (e.Group == null || e.Group.Name == groupName));

                var q = ctx.SecEntries.Include(e => e.App).Include(e => e.Context).Include(e => e.Object).Include(e => e.User).Include(e => e.Group);

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

        private static long GetAppIdByName(SqlDbContext ctx, string appName)
        {
            var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => (long?) a.Id).FirstOrDefault();
            if (appId == null)
            {
                throw new SecAppNotFoundException();
            }
            return appId.Value;
        }

        private static SqlSecGroup GetGroupByName(SqlDbContext ctx, long appId, string groupName)
        {
            var group = ctx.SecGroups.Include(g => g.Users).FirstOrDefault(g => g.AppId == appId && g.Name == groupName);
            if (group == null)
            {
                throw new SecGroupNotFoundException();
            }
            return group;
        }

        private static SqlSecUser GetUserByLogin(SqlDbContext ctx, long appId, string userLogin)
        {
            var user = ctx.SecUsers.FirstOrDefault(u => u.AppId == appId && u.Login == userLogin);
            if (user == null)
            {
                throw new SecUserNotFoundException();
            }
            return user;
        }

        private static SqlSecUser GetUserByLoginWithGroups(SqlDbContext ctx, long appId, string userLogin)
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