using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Sql
{
   public sealed class SqlSecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         using (var ctx = DataAccess.CreateContext())
         {
            return (from a in ctx.SecApps.Include("Users.Groups").Include("Groups.Users").Include("Contexts.Objects").Include(a => a.LogSettings)
                    where appName == null || a.Name == appName.ToLower()
                    orderby a.Name
                    select a).ToLogAndList();
         }
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName, string groupName)
      {
         using (var ctx = DataAccess.CreateContext())
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
         throw new System.NotImplementedException();
      }

      protected override void DoRemoveGroup(string appName, string groupName)
      {
         using (var ctx = DataAccess.CreateContext())
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
         using (var ctx = DataAccess.CreateContext())
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
         using (var ctx = DataAccess.CreateContext())
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
         using (var ctx = DataAccess.CreateContext())
         {
            return (from c in ctx.SecContexts.Include(c => c.Objects)
                    where appName == null || c.App.Name == appName.ToLower()
                    orderby c.Name, c.App.Name
                    select c).ToList();
         }
      }
   }
}
