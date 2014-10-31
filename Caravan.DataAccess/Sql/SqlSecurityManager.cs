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
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            return (from a in ctx.SecApps.Include("Users.Groups").Include("Groups.Users").Include("Contexts.Objects").Include(a => a.LogSettings)
                    where appName == null || a.Name == appName.ToLower()
                    orderby a.Name
                    select a).ToLogAndList();
         }
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName, string groupName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            return (from g in ctx.SecGroups.Include("Users.Groups")
                    where appName == null || g.App.Name == appName.ToLower()
                    where groupName == null || g.Name == groupName.ToLower()
                    orderby g.Name, g.App.Name
                    select g).ToList();
         }
      }

      protected override void DoAddGroup(string appName, SecGroup group)
      {
         throw new NotImplementedException();
      }

      protected override void DoRemoveGroup(string appName, string groupName)
      {
         using (var ctx = Db.CreateContext())
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
         using (var ctx = Db.CreateContext())
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

      protected override IEnumerable<SecUser> GetUsers(string appName, string userLogin)
      {
         using (var ctx = Db.CreateReadContext())
         {
            return (from u in ctx.SecUsers.Include("Groups.Users")
                    where appName == null || u.App.Name == appName.ToLower()
                    where userLogin == null || u.Login == userLogin.ToLower()
                    orderby u.Login, u.App.Name
                    select u).ToList();
         }
      }

      protected override IEnumerable<SecContext> GetContexts(string appName)
      {
         using (var ctx = Db.CreateReadContext())
         {
            return (from c in ctx.SecContexts.Include(c => c.Objects)
                    where appName == null || c.App.Name == appName.ToLower()
                    orderby c.Name, c.App.Name
                    select c).ToList();
         }
      }

      protected override IEnumerable<SecObject> GetObjects(string appName, string contextName)
      {
         throw new NotImplementedException();
      }

      #region Entries

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectType, string userLogin, string[] groupNames)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var noGroups = groupNames.Length == 0;
            return (from e in ctx.SecEntries.Include(e => e.App).Include(e => e.Context).Include(e => e.Object).Include(e => e.User).Include(e => e.User)
                    where appName == null || e.App.Name == appName
                    where contextName == null || e.Context.Name == contextName
                    where objectType == null || e.Object.Type == objectType
                    where userLogin == null || e.User.Login == userLogin
                    where noGroups || groupNames.Contains(e.Group.Name)
                    orderby e.Object.Name
                    select e).ToList();
         }
      }

      protected override void DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         using (var ctx = Db.CreateContext())
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
         using (var ctx = Db.CreateContext())
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
