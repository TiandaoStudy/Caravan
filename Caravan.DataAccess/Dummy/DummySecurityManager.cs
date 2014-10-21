using System.Collections.Generic;
using Finsa.Caravan.Collections;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Dummy
{
   public sealed class DummySecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         return ReadOnlyList.Empty<SecApp>();
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName)
      {
         return ReadOnlyList.Empty<SecGroup>();
      }

      protected override IEnumerable<SecUser> GetUsers(string appName)
      {
         return ReadOnlyList.Empty<SecUser>();
      }

      protected override IEnumerable<SecContext> GetContexts(string appName)
      {
         return ReadOnlyList.Empty<SecContext>();
      }
   }
}
