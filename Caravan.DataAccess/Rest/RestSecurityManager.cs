using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Rest
{
   public sealed class RestSecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName, string groupName)
      {
         throw new NotImplementedException();
      }

      protected override void DoAddGroup(string appName, SecGroup @group)
      {
         throw new NotImplementedException();
      }

      protected override void DoRemoveGroup(string appName, string groupName)
      {
         throw new NotImplementedException();
      }

      protected override void DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<SecUser> GetUsers(string appName, string userLogin)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<SecContext> GetContexts(string appName)
      {
         throw new NotImplementedException();
      }
   }
}
