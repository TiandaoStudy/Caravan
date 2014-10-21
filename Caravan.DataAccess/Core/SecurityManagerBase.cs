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

      public IEnumerable<SecGroup> Groups()
      {
         return GetGroups(null);
      }

      public IEnumerable<SecGroup> Groups(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetGroups(appName);
      }

      #endregion

      #region Users

      public IEnumerable<SecUser> Users()
      {
         return GetUsers(null);
      }

      public IEnumerable<SecUser> Users(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetUsers(appName);
      }

      #endregion

      #region Users

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

      protected abstract IEnumerable<SecGroup> GetGroups(string appName);

      protected abstract IEnumerable<SecUser> GetUsers(string appName);

      protected abstract IEnumerable<SecContext> GetContexts(string appName);

      #endregion
   }
}
