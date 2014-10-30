﻿using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Dummy
{
   public sealed class DummySecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApps(string appName)
      {
         throw new System.NotImplementedException();
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override void DoAddGroup(string appName, SecGroup @group)
      {
         throw new System.NotImplementedException();
      }

      protected override void DoRemoveGroup(string appName, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override void DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         throw new System.NotImplementedException();
      }

      protected override IEnumerable<SecUser> GetUsers(string appName, string userLogin)
      {
         throw new System.NotImplementedException();
      }

      protected override IEnumerable<SecContext> GetContexts(string appName)
      {
         throw new System.NotImplementedException();
      }

      protected override IEnumerable<SecObject> GetObjects(string appName, string contextName)
      {
         throw new System.NotImplementedException();
      }

      protected override IEnumerable<SecObject> GetEntries(string appName, string userLogin, string[] groupNames, string contextName, string objectType)
      {
         throw new System.NotImplementedException();
      }
   }
}
