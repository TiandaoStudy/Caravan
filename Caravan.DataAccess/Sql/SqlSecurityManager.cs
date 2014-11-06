using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Sql
{
   public sealed class SqlSecurityManager : SecurityManagerBase<SqlSecurityManager>
   {
      #region Apps

      protected override IList<SecApp> GetApps(string appName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var q = ctx.SecApps.Include("Users.Groups").Include("Groups.Users").Include("Contexts.Objects").Include(a => a.LogSettings);
            if (appName != null)
            {
               q = q.Where(a => a.Name == appName);
            }
            return q.OrderBy(a => a.Name).ToLogAndList();
         }
      }

      protected override bool DoAddApp(SecApp app)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var added = false;
            if (!ctx.SecApps.Any(a => a.Name == app.Name))
            {
               app.Id = (ctx.SecApps.Max(a => (long?) a.Id) ?? -1) + 1;
               ctx.SecApps.Add(app);
               added = true;
            }
            ctx.SaveChanges();
            return added;
         }
      }

      #endregion

      #region Groups

      protected override IList<SecGroup> GetGroups(string appName, string groupName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var q = ctx.SecGroups.Include(g => g.App).Include("Users.Groups");
            if (appName != null)
            {
               q = q.Where(g => g.App.Name == appName);
            }
            if (groupName != null)
            {
               q = q.Where(g => g.Name == groupName);
            }
            return q.OrderBy(g => g.App.Name).ThenBy(g => g.Name).ToList();
         }
      }

      protected override bool DoAddGroup(string appName, SecGroup newGroup)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var added = false;
               var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => a.Id).First();
               if (!ctx.SecGroups.Any(g => g.AppId == appId && g.Name == newGroup.Name))
               {
                  newGroup.Id = (ctx.SecGroups.Where(g => g.AppId == appId).Max(g => (long?) g.Id) ?? -1) + 1;
                  newGroup.AppId = appId;
                  ctx.SecGroups.Add(newGroup);
                  added = true;
               }
               ctx.SaveChanges();
               return added;
            }
            catch
            {
               trx.Rollback();
               // TODO LOG
               throw;
            }
         }
      }

      protected override bool DoRemoveGroup(string appName, string groupName)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var removed = false;
               var grp = ctx.SecGroups.FirstOrDefault(g => g.App.Name == appName && g.Name == groupName);
               if (grp != null)
               {
                  ctx.SecGroups.Remove(grp);
                  removed = true;
               }
               ctx.SaveChanges();
               return removed;
            }
            catch
            {
               trx.Rollback();
               throw;
            }
         }
      }

      protected override bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var updated = false;
               var grp = ctx.SecGroups.FirstOrDefault(g => g.App.Name == appName && g.Name == groupName);
               if (grp != null)
               {
                  grp.Name = newGroup.Name;
                  grp.Description = newGroup.Description;
                  grp.IsAdmin = newGroup.IsAdmin;
                  updated = true;
               }
               ctx.SaveChanges();
               return updated;
            }
            catch
            {
               trx.Rollback();
               throw;
            }
         }
      }

      #endregion

      #region Users

      protected override IList<SecUser> GetUsers(string appName, string userLogin)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var q = ctx.SecUsers.Include(u => u.App).Include("Groups.Users");
            if (appName != null)
            {
               q = q.Where(u => u.App.Name == appName);
            }
            if (userLogin != null)
            {
               q = q.Where(u => u.Login == userLogin);
            }
            return q.OrderBy(u => u.App.Name).ThenBy(u => u.Login).ToList();
         }
      }

      protected override bool DoAddUser(string appName, SecUser newUser)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var added = false;
               var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => a.Id).First();
               if (!ctx.SecUsers.Any(u => u.AppId == appId && u.Login == newUser.Login))
               {
                  newUser.Id = (ctx.SecUsers.Where(u => u.AppId == appId).Max(us => (long?) us.Id) ?? -1) + 1;
                  newUser.AppId = appId;
                  ctx.SecUsers.Add(newUser);
                  added = true;
               }
               ctx.SaveChanges();
               return added;
            }
            catch
            {
               trx.Rollback();
               throw;
            }
         }
      }

      protected override bool DoRemoveUser(string appName, string userLogin)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var removed = false;
               var user = ctx.SecUsers.FirstOrDefault(us => us.App.Name == appName && us.Login == userLogin);
               if (user != null)
               {
                  ctx.SecUsers.Remove(user);
                  removed = true;
               }
               ctx.SaveChanges();
               return removed;
            }
            catch
            {
               trx.Rollback();
               // Db.Logger
               throw;
            }
         }
      }

      protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var updated = false;
               var user = ctx.SecUsers.FirstOrDefault(us => us.App.Name == appName && us.Login == userLogin);
               if (user != null)
               {
                  user.FirstName = newUser.FirstName;
                  user.LastName = newUser.LastName;
                  user.Email = newUser.Email;
                  user.Login = newUser.Login;
                  user.Active = newUser.Active;
                  updated = true;
               }
               ctx.SaveChanges();
               return updated;
            }
            catch
            {
               trx.Rollback();
               throw;
            }
         }
      }

      #endregion

      #region Contexts

      protected override IList<SecContext> GetContexts(string appName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var q = ctx.SecContexts.Include(c => c.Objects);
            if (appName != null)
            {
               q = q.Where(c => c.App.Name == appName);
            }
            return q.OrderBy(c => c.App.Name).ThenBy(c => c.Name).ToList();
         }
      }

      #endregion

      #region Objects

      protected override IList<SecObject> GetObjects(string appName, string contextName)
      {
         throw new NotImplementedException();
      }

      #endregion

      #region Entries

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectType, string userLogin, string[] groupNames)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var q = ctx.SecEntries.Include(e => e.App).Include(e => e.Context).Include(e => e.Object).Include(e => e.User).Include(e => e.Group);
            if (appName != null)
            {
               q = q.Where(e => e.App.Name == appName);
            }
            if (contextName != null)
            {
               q = q.Where(e => e.Context.Name == contextName);
            }
            if (objectType != null)
            {
               q = q.Where(e => e.Object.Type == objectType);
            }
            if (userLogin != null)
            {
               q = q.Where(e => e.User.Login == userLogin);
            }
            if (groupNames.Length == 0)
            {
               q = q.Where(e => groupNames.Contains(e.Group.Name));
            }
            return q.OrderBy(e => e.Object.Name).ToList();
         }
      }

      protected override bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => a.Id).First();
               var dbContext = ctx.SecContexts.FirstOrDefault(c => c.AppId == appId && c.Name == secContext.Name);
               if (dbContext == null)
               {
                  secContext.Id = (ctx.SecContexts.Where(o => o.AppId == appId).Max(o => (long?) o.Id) ?? -1) + 1;
                  secContext.AppId = appId;
                  ctx.SecContexts.Add(secContext);
                  dbContext = secContext;
               }
               else
               {
                  dbContext.Description = secContext.Description;
               }
               var dbObject = ctx.SecObjects.FirstOrDefault(o => o.AppId == appId && o.ContextId == dbContext.Id && o.Name == secObject.Name);
               if (dbObject == null)
               {
                  secObject.Id = (ctx.SecObjects.Where(o => o.AppId == appId && o.ContextId == dbContext.Id).Max(o => (long?) o.Id) ?? -1) + 1;
                  secObject.AppId = appId;
                  secObject.ContextId = dbContext.Id;
                  ctx.SecObjects.Add(secObject);
                  dbObject = secObject;
               }
               else
               {
                  dbObject.Description = secObject.Description;
                  dbObject.Type = secObject.Type;
               }
               long? userId = null;
               if (!String.IsNullOrWhiteSpace(userLogin))
               {
                  userId = ctx.SecUsers.Where(u => u.AppId == appId && u.Login == userLogin).Select(u => (long?) u.Id).FirstOrDefault();
               }
               long? groupId = null;
               if (!String.IsNullOrWhiteSpace(groupName))
               {
                  groupId = ctx.SecGroups.Where(g => g.AppId == appId && g.Name == groupName).Select(g => (long?) g.Id).FirstOrDefault();
               }
               var added = false;
               if (!ctx.SecEntries.Any(e => e.AppId == appId && e.UserId == userId && e.GroupId == groupId && e.ContextId == dbContext.Id && e.ObjectId == dbObject.Id))
               {
                  var secEntry = new SecEntry
                  {
                     Id = (ctx.SecEntries.Where(e => e.AppId == appId).Max(e => (long?) e.Id) ?? -1) + 1,
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
               return added;
            }
            catch
            {
               trx.Rollback();
               throw;
            }
         }
      }

      protected override bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => a.Id).First();
               var entry = ctx.SecEntries.FirstOrDefault(e => e.AppId == appId && e.Context.Name == contextName && e.Object.Name == objectName && (e.User == null || e.User.Login == userLogin) && (e.Group == null || e.Group.Name == groupName));
               var removed = false;
               if (entry != null)
               {
                  ctx.SecEntries.Remove(entry);
                  removed = true;
               }
               ctx.SaveChanges();
               return removed;
            }
            catch
            {
               trx.Rollback();
               throw;
            }
         }
      }

      #endregion
   }
}
