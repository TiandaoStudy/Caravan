using System;
using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.Common.DataModel;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
   public abstract class SecurityManagerBase : ISecurityManager
   {
      public IEnumerable<SecApp> Applications()
      {
         return GetApplications(null);
      }

      public SecApp Application(string applicationName)
      {
         Raise<ArgumentException>.IfIsEmpty(applicationName);
         return GetApplications(applicationName).FirstOrDefault();
      }

      #region Abstract Methods

      protected abstract IEnumerable<SecApp> GetApplications(string applicationName);

      #endregion
   }
}
