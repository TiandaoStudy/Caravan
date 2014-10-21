using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Remote
{
   public sealed class RemoteSecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<SecUser> GetUsers(string appName)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<SecContext> GetContexts(string appName)
      {
         throw new NotImplementedException();
      }
   }
}
