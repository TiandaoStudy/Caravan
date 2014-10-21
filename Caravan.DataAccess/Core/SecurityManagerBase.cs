using System;
using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
   public abstract class SecurityManagerBase : ISecurityManager
   {
      #region Apps

      public IEnumerable<SecApp> Apps()
      {
         return GetApps(null);
      }

      public SecApp App(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetApps(appName).FirstOrDefault();
      }

      #endregion

      #region Groups

      public IEnumerable<SecGroup> Groups(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetGroups(appName, null);
      }
      
      public SecGroup Group(string appName, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         return GetGroups(appName, groupName).FirstOrDefault();
      }

      public void AddOrUpdateGroup(string appName, SecGroup group)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsNotNull(group);
         DoAddOrUpdateGroup(appName, group);
      }

      public void RemoveGroup(string appName, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         DoRemoveGroup(appName, groupName);
      }

      #endregion

      #region Users

      public IEnumerable<SecUser> Users(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetUsers(appName, null);
      }

      public SecUser User(string appName, string userLogin)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         return GetUsers(appName, userLogin).FirstOrDefault();
      }

      #endregion

      #region Contexts

      public IEnumerable<SecContext> Contexts()
      {
         return GetContexts(null);
      }

      public IEnumerable<SecContext> Contexts(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetContexts(appName);
      }

      #endregion

      #region Abstract Methods

      protected abstract IEnumerable<SecApp> GetApps(string appName);

      protected abstract IEnumerable<SecGroup> GetGroups(string appName, string groupName);

      protected abstract void DoAddOrUpdateGroup(string appName, SecGroup group);

      protected abstract void DoRemoveGroup(string appName, string groupName);

      protected abstract IEnumerable<SecUser> GetUsers(string appName, string userLogin);

      protected abstract IEnumerable<SecContext> GetContexts(string appName);

      #endregion
   }
}
