using System;
using System.Collections.Generic;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Dummy
{
   internal sealed class DummyQueryManager : IQueryManager
   {
      public IEnumerable<SecGroup> Groups(string queryString)
      {
         throw new NotImplementedException();
      }
   }
}
