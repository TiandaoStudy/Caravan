using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Dummy
{
   internal sealed class DummyQueryManager : QueryManagerBase
   {
      protected override IEnumerable<SecGroup> QueryGroups(string appName, string queryString)
      {
         throw new NotImplementedException();
      }
   }
}
