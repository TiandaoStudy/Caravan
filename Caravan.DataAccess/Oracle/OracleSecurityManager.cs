using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Oracle
{
   public sealed class OracleSecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         using (var ctx = new OracleDbContext())
         {
            return (from a in ctx.SecApps.Include("Users.Groups").Include(a => a.Groups).Include(a => a.LogSettings)
                    where appName == null || a.Name == appName.ToLower()
                    orderby a.Name
                    select a).ToLogAndList();
         }
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName)
      {
         using (var ctx = new OracleDbContext())
         {
            return (from g in ctx.SecGroups.Include(u => u.Users)
                    where appName == null || g.App.Name == appName.ToLower()
                    orderby g.Name, g.App.Name
                    select g).ToList();
         }
      }

      protected override IEnumerable<SecUser> GetUsers(string appName)
      {
         using (var ctx = new OracleDbContext())
         {
            return (from u in ctx.SecUsers.Include(u => u.Groups)
                    where appName == null || u.App.Name == appName.ToLower()
                    orderby u.Login, u.App.Name
                    select u).ToList();
         }
      }
   }
}
