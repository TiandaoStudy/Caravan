using System.Collections.Generic;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess
{
   public interface IQueryManager
   {
      IEnumerable<SecGroup> Groups(string appName, string queryString);
   }
}
