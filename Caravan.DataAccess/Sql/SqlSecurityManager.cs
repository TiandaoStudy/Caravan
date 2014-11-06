﻿using System;
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

      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            return (from a in ctx.SecApps.Include("Users.Groups").Include("Groups.Users").Include("Contexts.Objects").Include(a => a.LogSettings)
                    where appName == null || a.Name == appName
                    orderby a.Name
                    select a).ToLogAndList();
         }
      }

      protected override SecApp DoAddApp(SecApp app)
      {
         using (var ctx = Db.CreateWriteContext())
         {
            app.Id = (ctx.SecApps.Max(a => (long?) a.Id) ?? -1) + 1;
            ctx.SecApps.Add(app);
            ctx.SaveChanges();
            return app;
         }
      }

      #endregion

      #region Groups

      protected override IEnumerable<SecGroup> GetGroups(string appName, string groupName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            return (from g in ctx.SecGroups.Include(g => g.App).Include("Users.Groups")
                    where appName == null || g.App.Name == appName
                    where groupName == null || g.Name == groupName
                    orderby g.Name, g.App.Name
                    select g).ToList();
         }
      }

      protected override void DoAddGroup(string appName, SecGroup newGroup)
      {
         using (var ctx = Db.CreateWriteContext())
         using (var trx = ctx.BeginTransaction())
         {
            if (!ctx.SecGroups.Any(g => g.App.Name == appName.ToLower() && g.Name == newGroup.Name.ToLower()))
            {
               var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => a.Id).First();
               var secGroup = new SecGroup
               {
                  Id = (ctx.SecGroups.Where(g => g.AppId == appId).Max(g => (long?)g.Id) ?? -1) + 1,
                  AppId = appId,
                  App = newGroup.App,
                  Name= newGroup.Name,
                  Description= newGroup.Description,
                  IsAdmin= newGroup.IsAdmin
               };
               ctx.SecGroups.Add(secGroup);
            }
            ctx.SaveChanges();
            trx.Commit();
         }

      }

      protected override void DoRemoveGroup(string appName, string groupName)
      {
         using (var ctx = Db.CreateWriteContext())
         using (var trx = ctx.BeginTransaction())
         {
            var grp = ctx.SecGroups.FirstOrDefault(g => g.App.Name == appName.ToLower() && g.Name == groupName.ToLower());
            if (grp != null)
            {
               ctx.SecGroups.Remove(grp);
            }
            ctx.SaveChanges();
            trx.Commit();
         }
      }

      protected override void DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         using (var ctx = Db.CreateWriteContext())
         using (var trx = ctx.BeginTransaction())
         {
            var grp = ctx.SecGroups.FirstOrDefault(g => g.App.Name == appName.ToLower() && g.Name == groupName.ToLower());
            Raise<InvalidOperationException>.IfIsNull(grp, ErrorMessages.Sql_SqlSecurityManager_MissingGroup);
            grp.Name = newGroup.Name;
            grp.Description = newGroup.Description;
            grp.IsAdmin = newGroup.IsAdmin;
            // Users???
            ctx.SaveChanges();
            trx.Commit();
         }
      }

      #endregion

      #region Users

      protected override IEnumerable<SecUser> GetUsers(string appName, string userLogin)
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
            return q.OrderBy(u => u.Login).ThenBy(u => u.App.Name).ToList();
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
               var user = ctx.SecUsers.FirstOrDefault(us => us.App.Name == appName.ToLower() && us.Login == userLogin.ToLower());
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
               var user = ctx.SecUsers.FirstOrDefault(us => us.App.Name == appName.ToLower() && us.Login == userLogin.ToLower());
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

      protected override IEnumerable<SecContext> GetContexts(string appName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            return (from c in ctx.SecContexts.Include(c => c.Objects)
                    where appName == null || c.App.Name == appName
                    orderby c.Name, c.App.Name
                    select c).ToList();
         }
      }

      #endregion

      #region Objects

      protected override IEnumerable<SecObject> GetObjects(string appName, string contextName)
      {
         throw new NotImplementedException();
      }

      #endregion

      #region Entries

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectType, string userLogin, string[] groupNames)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var noGroups = groupNames.Length == 0;
            return (from e in ctx.SecEntries.Include(e => e.App).Include(e => e.Context).Include(e => e.Object).Include(e => e.User).Include(e => e.Group)
                    where appName == null || e.App.Name == appName
                    where contextName == null || e.Context.Name == contextName
                    where objectType == null || e.Object.Name == objectType
                    where userLogin == null || e.User.Login == userLogin
                    where noGroups || groupNames.Contains(e.Group.Name)
                    orderby e.Object.Name
                    select e).ToList();
         }
      }

      protected override void DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         using (var ctx = Db.CreateWriteContext())
         using (var trx = ctx.BeginTransaction())
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
            }
            ctx.SaveChanges();
            trx.Commit();
         }
      }

      protected override void DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         using (var ctx = Db.CreateWriteContext())
         using (var trx = ctx.BeginTransaction())
         {
            var appId = ctx.SecApps.Where(a => a.Name == appName).Select(a => a.Id).First();
            var entry = ctx.SecEntries.FirstOrDefault(e => e.AppId == appId && e.Context.Name == contextName && e.Object.Name == objectName && (e.User == null || e.User.Login == userLogin) && (e.Group == null || e.Group.Name == groupName));
            if (entry != null)
            {
               ctx.SecEntries.Remove(entry);
            }
            ctx.SaveChanges();
            trx.Commit();
         }
      }

      #endregion
   }
}
