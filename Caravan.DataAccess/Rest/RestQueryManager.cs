using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Rest
{
   internal sealed class RestQueryManager : QueryManagerBase
   {
      protected override IEnumerable<SecGroup> QueryGroups(string appName, string queryString)
      {
         throw new NotImplementedException();
      }
   }
}
