using System.Collections.Generic;
using Finsa.Caravan.DataModel;
using LinqToQuerystring;

namespace Finsa.Caravan.DataAccess.Sql
{
   internal sealed class SqlQueryManager : IQueryManager
   {
      public IEnumerable<SecGroup> Groups(string queryString)
      {
         using (var ctx = Db.CreateContext())
         {
            return ctx.SecGroups.Include("Users.Groups").LinqToQuerystring(queryString);
         }
      }
   }
}
