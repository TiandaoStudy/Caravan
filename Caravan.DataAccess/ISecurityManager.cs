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

      IEnumerable<SecGroup> Groups(string appName);

      SecGroup Group(string appName, string groupName);

      void AddOrUpdateGroup(string appName, SecGroup group);

      void RemoveGroup(string appName, string groupName);

      #endregion

      #region Users

      IEnumerable<SecUser> Users(string appName);

      SecUser User(string appName, string userLogin);

      #endregion

      #region Contexts

      IEnumerable<SecContext> Contexts();

      IEnumerable<SecContext> Contexts(string appName);

      #endregion
   }
}
