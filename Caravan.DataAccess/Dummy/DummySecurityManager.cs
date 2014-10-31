using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Dummy
{
   public sealed class DummySecurityManager : SecurityManagerBase<DummySecurityManager>
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

      #region Entries

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectType, string userLogin, string[] groupNames)
      {
         throw new System.NotImplementedException();
      }

      protected override void DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override void DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      #endregion
   }
}
