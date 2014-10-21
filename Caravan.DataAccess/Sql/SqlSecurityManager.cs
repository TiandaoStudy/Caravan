using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Sql
{
   public sealed class SqlSecurityManager : SecurityManagerBase
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

      protected override IEnumerable<SecGroup> GetGroups(string appName)
      {
         using (var ctx = Db.CreateContext())
         {
            return (from g in ctx.SecGroups.Include("Users.Groups")
                    where appName == null || g.App.Name == appName.ToLower()
                    orderby g.Name, g.App.Name
                    select g).ToList();
         }
      }

      protected override IEnumerable<SecUser> GetUsers(string appName)
      {
         using (var ctx = Db.CreateContext())
         {
            return (from u in ctx.SecUsers.Include("Groups.Users")
                    where appName == null || u.App.Name == appName.ToLower()
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
   }
}
