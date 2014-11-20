using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Dummy
{
   public sealed class DummySecurityManager : SecurityManagerBase<DummySecurityManager>
   {
      protected override IList<SecApp> GetApps(string appName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddApp(SecApp app)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecGroup> GetGroups(string appName, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddGroup(string appName, SecGroup newGroup)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveGroup(string appName, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecUser> GetUsers(string appName, string userLogin)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddUser(string appName, SecUser newUser)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveUser(string appName, string userLogin)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddUserToGroup(string appName, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecContext> GetContexts(string appName)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecObject> GetObjects(string appName, string contextName)
      {
         throw new System.NotImplementedException();
      }

      #region Entries

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectName, string userLogin)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      #endregion
   }
}
