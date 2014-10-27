using System;
using System.Collections.Generic;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Dummy
{
   internal sealed class DummyQueryManager : IQueryManager
   {
      public IEnumerable<SecGroup> Groups(string appName, string queryString)
      {
         throw new NotImplementedException();
      }
   }
}
