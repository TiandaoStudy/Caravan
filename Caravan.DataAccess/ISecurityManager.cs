using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.DataModel.Exceptions;
using Finsa.Caravan.Common.DataModel.Security;

namespace Finsa.Caravan.DataAccess
{
   public interface ISecurityManager
   {
      #region Apps

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      IList<SecApp> Apps();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <returns></returns>
      /// <exception cref="AppNotFoundException">
      ///   There is no app with given <paramref name="appName"/>.
      /// </exception>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      SecApp App(string appName);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="app"></param>
      /// <exception cref="AppExistingException">
      ///   There is already an app with given <paramref name="appName"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      ///   <paramref name="app"/> is null.
      /// </exception>
      /// <exception cref="ArgumentException">
      ///   <paramref name="app.Name"/> is null or empty.
      /// </exception>
      void AddApp(SecApp app);

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
      IList<SecGroup> Groups(string appName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="groupName"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> or <paramref name="groupName"/> are null or empty.
      /// </exception>
      SecGroup Group(string appName, string groupName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="newGroup"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> or <paramref name="newGroup.Name"/> are null or empty.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      ///   <paramref name="newGroup"/> is null.
      /// </exception>
      void AddGroup(string appName, SecGroup newGroup);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="groupName"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> or <paramref name="groupName"/> are null or empty.
      /// </exception>
      void RemoveGroup(string appName, string groupName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="groupName"></param>
      /// <param name="newGroup"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/>, <paramref name="groupName"/> or <paramref name="newGroup.Name"/> are null or empty.
      /// </exception>
      void UpdateGroup(string appName, string groupName, SecGroup newGroup);

      #endregion

      #region Users

      IList<SecUser> Users(string appName);

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

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="userLogin"></param>
      /// <param name="groupName"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/>, <paramref name="userLogin"/> or <paramref name="groupName"/> are null or empty.
      /// </exception>
      void AddUserToGroup(string appName, string userLogin, string groupName);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="userLogin"></param>
      /// <param name="groupName"></param>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/>, <paramref name="userLogin"/> or <paramref name="groupName"/> are null or empty.
      /// </exception>
      void RemoveUserFromGroup(string appName, string userLogin, string groupName);

      #endregion

      #region Contexts

      IList<SecContext> Contexts(string appName);

      #endregion

      #region Objects
      
      IList<SecObject> Objects(string appName);

      IList<SecObject> Objects(string appName, string contextName);

      #endregion

      #region Entries

      IList<SecEntry> Entries(string appName);

      IList<SecEntry> Entries(string appName, string contextName);

      IList<SecEntry> Entries(string appName, string contextName, string userLogin);

      IList<SecEntry> EntriesForObject(string appName, string contextName, string objectName);

      IList<SecEntry> EntriesForObject(string appName, string contextName, string objectName, string userLogin);

      void AddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

      void RemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName);

      #endregion
   }
}
