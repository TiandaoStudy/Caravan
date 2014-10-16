using System.Collections.Generic;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess
{
   public interface ISecurityManager
   {
      #region Apps

      IEnumerable<SecApp> Apps();

      SecApp App(string appName);

      #endregion

      #region Groups

      IEnumerable<SecGroup> Groups();

      IEnumerable<SecGroup> Groups(string appName);

      #endregion
   }
}
