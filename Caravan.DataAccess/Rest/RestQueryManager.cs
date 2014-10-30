using System;
using System.Collections.Generic;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Rest
{
   internal sealed class RestQueryManager : IQueryManager
   {
      public IEnumerable<SecGroup> Groups(string appName, string queryString)
      {
         throw new NotImplementedException();
      }
   }
}
