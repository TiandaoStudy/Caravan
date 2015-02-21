using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Core;

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
