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
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         using (var ctx = Db.CreateContext())
         {
            return (from a in ctx.SecApps.Include("Users.Groups").Include("Groups.Users").Include("Contexts.Objects").Include(a => a.LogSettings)
                    where appName == null || a.Name == appName.ToLower()
                    orderby a.Name
                    select a).ToLogAndList();
         }
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName, string groupName)
      {
         using (var ctx = Db.CreateContext())
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
         using (var ctx = Db.CreateContext())
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
         using (var ctx = Db.CreateContext())
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
               secContext.Id = ctx.SecContexts.Where(o => o.AppId == appId).Max(o => (int?) o.Id) ?? 0;
               ctx.SecContexts.Add(dbContext);
               dbContext = secContext;
            }
            var dbObject = ctx.SecObjects.FirstOrDefault(o => o.App.Name == appName && o.ContextId == dbContext.Id && o.Name == secObject.Name);
            if (dbObject == null)
            {
               secObject.Id = ctx.SecObjects.Where(o => o.App.Name == appName && o.ContextId == dbContext.Id).Max(o => (int?) o.Id) ?? 0;
               secObject.AppId = appId;
               secObject.ContextId = dbContext.Id;
               ctx.SecObjects.Add(secObject);
               dbObject = secObject;
            }
            long? userId = null;
            if (!String.IsNullOrWhiteSpace(userLogin))
            {
               userId = ctx.SecUsers.Where(u => u.App.Name == appName && u.Login == userLogin).Select(u => u.Id).FirstOrDefault();
            }
            long? groupId = null;
            if (!String.IsNullOrWhiteSpace(groupName))
            {
               groupId = ctx.SecGroups.Where(g => g.App.Name == appName && g.Name == groupName).Select(g => g.Id).FirstOrDefault();
            }
            var secEntry = new SecEntry
            {
               AppId = dbObject.AppId,
               UserId = userId,
               GroupId = groupId,
               ContextId = dbObject.ContextId,
               ObjectId = dbObject.Id
            };
            ctx.SecEntries.Add(secEntry);
            ctx.SaveChanges();
            trx.Commit();
         }
      }

      #endregion
   }
}
