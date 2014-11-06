using System;
using System.Collections.Generic;
using Finsa.Caravan.DataModel.Exceptions;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess
{
   public interface ISecurityManager
   {
      #region Apps

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      IEnumerable<SecApp> Apps();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      SecApp App(string appName);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="app"></param>
      /// <exception cref="ArgumentNullException">
      ///   <paramref name="app"/> is null.
      /// </exception>
      SecApp AddApp(SecApp app);

      #endregion

      #region Groups

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      IEnumerable<SecGroup> Groups(string appName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="groupName"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      SecGroup Group(string appName, string groupName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="newGroup"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      void AddGroup(string appName, SecGroup newGroup);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="groupName"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      void RemoveGroup(string appName, string groupName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="groupName"></param>
      /// <param name="newGroup"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      void UpdateGroup(string appName, string groupName, SecGroup newGroup);

      #endregion

      #region Users

      IEnumerable<SecUser> Users(string appName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="userLogin"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      /// <exception cref="UserNotFoundException">
      ///   An user with given login does not exist.
      /// </exception>
      SecUser User(string appName, string userLogin);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="newUser"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      /// <exception cref="UserExistingException">
      ///   An user with given login already exists.
      /// </exception>
      void AddUser(string appName, SecUser newUser);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="userLogin"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      void RemoveUser(string appName, string userLogin);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="userLogin"></param>
      /// <param name="newUser"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      void UpdateUser(string appName, string userLogin, SecUser newUser);

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

      IList<SecEntry> Entries(string appName, string contextName, string userLogin, string[] groupNames);

      IList<SecEntry> Entries(string appName, string contextName, string objectName);

      IList<SecEntry> Entries(string appName, string contextName, string objectName, string userLogin, string[] groupNames);

      void AddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

      void RemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName);

      #endregion
   }
}
