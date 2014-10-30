﻿using System.Collections.Generic;
using Finsa.Caravan.DataModel.Security;

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

      void AddGroup(string appName, SecGroup group);

      void RemoveGroup(string appName, string groupName);

      void UpdateGroup(string appName, string groupName, SecGroup newGroup);

      #endregion

      #region Users

      IEnumerable<SecUser> Users(string appName);

      SecUser User(string appName, string userLogin);

      #endregion

      #region Contexts

      IEnumerable<SecContext> Contexts();

      IEnumerable<SecContext> Contexts(string appName);

      #endregion

      #region Objects

      IEnumerable<SecObject> Objects();

      IEnumerable<SecObject> Objects(string appName);

      IEnumerable<SecObject> Objects(string appName, string contextName);

      #endregion

      #region Entries

      IList<SecEntry> Entries(string appName, string contextName);

      IList<SecEntry> Entries(string appName, string contextName, string objectType);

      IList<SecEntry> Entries(string appName, string contextName, string objectType, string userLogin, string[] groupNames);

      void AddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

      #endregion
   }
}
