using System;
using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
   public abstract class SecurityManagerBase : ISecurityManager
   {
      public IEnumerable<SecApp> Apps()
      {
         return GetApplications(null);
      }

      public SecApp App(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetApplications(appName).FirstOrDefault();
      }

      public IEnumerable<SecGroup> Groups()
      {
         return GetGroups(null);
      }

      public IEnumerable<SecGroup> Groups(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetGroups(appName);
      }

      #region Abstract Methods

      protected abstract IEnumerable<SecApp> GetApplications(string appName);

      protected abstract IEnumerable<SecGroup> GetGroups(string appName);

      #endregion
   }
}
