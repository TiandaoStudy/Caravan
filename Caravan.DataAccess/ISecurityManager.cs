using System.Collections.Generic;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess
{
   public interface ISecurityManager
   {
      #region Applications

      IEnumerable<SecApp> Applications();

      SecApp Application(string applicationName);

      #endregion

      #region Groups

      IEnumerable<SecGroup> Groups();

      IEnumerable<SecGroup> Groups(string applicationName);

      #endregion
   }
}
