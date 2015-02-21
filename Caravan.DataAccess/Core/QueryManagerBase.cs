using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.Models.Security;
using PommaLabs.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
   internal abstract class QueryManagerBase : IQueryManager
   {
      public IEnumerable<SecGroup> Groups(string appName, string queryString)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return QueryGroups(appName, queryString ?? String.Empty);
      }

      protected abstract IEnumerable<SecGroup> QueryGroups(string appName, string queryString);
   }
}
