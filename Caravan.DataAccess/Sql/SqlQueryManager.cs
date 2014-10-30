using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;
using LinqToQuerystring;

namespace Finsa.Caravan.DataAccess.Sql
{
   internal sealed class SqlQueryManager : QueryManagerBase
   {
      protected override IEnumerable<SecGroup> QueryGroups(string appName, string queryString)
      {
         using (var ctx = Db.CreateContext())
         {
            return ctx.SecGroups
               .Include("Users.Groups")
               .Where(g => g.App.Name == appName.ToLower())
               .LinqToQuerystring(queryString)
               .ToList();
         }
      }
   }
}
