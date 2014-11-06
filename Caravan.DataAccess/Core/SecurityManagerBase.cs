using System;
using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.DataModel.Exceptions;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
   public abstract class SecurityManagerBase
   {
      protected static readonly string[] EmptyStringArray = new string[0];
   }

   public abstract class SecurityManagerBase<TSec> : SecurityManagerBase, ISecurityManager where TSec : SecurityManagerBase<TSec>
   {
      #region Apps

      public IEnumerable<SecApp> Apps()
      {
         return GetApps(String.Empty);
      }

      public SecApp App(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetApps(appName.ToLowerOrEmpty()).FirstOrDefault();
      }

      public SecApp AddApp(SecApp app)
      {
         Raise<ArgumentNullException>.IfIsNull(app);
         Raise<ArgumentException>.IfIsEmpty(app.Name);
         return DoAddApp(app);
      }

      #endregion

      #region Groups

      public IEnumerable<SecGroup> Groups(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetGroups(appName.ToLowerOrEmpty(), String.Empty);
      }
      
      public SecGroup Group(string appName, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         return GetGroups(appName.ToLowerOrEmpty(), groupName.ToLowerOrEmpty()).FirstOrDefault();
      }

      public void AddGroup(string appName, SecGroup newGroup)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsNull(newGroup);
         DoAddGroup(appName, newGroup);
      }

      public void RemoveGroup(string appName, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         DoRemoveGroup(appName, groupName);
      }

      public void UpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         Raise<ArgumentException>.IfIsNull(newGroup);
         DoUpdateGroup(appName, groupName, newGroup);
      }

      #endregion

      #region Users

      public IEnumerable<SecUser> Users(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetUsers(appName.ToLower(), null);
      }

      public SecUser User(string appName, string userLogin)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         var user = GetUsers(appName.ToLower(), userLogin.ToLower()).FirstOrDefault();
         if (user == null)
         {
            throw new UserNotFoundException();
         }
         return user;
      }

      public void AddUser(string appName, SecUser newUser)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsNull(newUser);
         Raise<ArgumentException>.IfIsEmpty(newUser.Login);
         if (!DoAddUser(appName, newUser))
         {
            throw new UserExistingException();
         }
      }

      public void RemoveUser(string appName, string userLogin)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         if (!DoRemoveUser(appName, userLogin))
         {
            throw new UserNotFoundException();
         }
      }

      public void UpdateUser(string appName, string userLogin, SecUser newUser)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsNull(newUser);
         Raise<ArgumentException>.IfIsEmpty(newUser.Login);
         DoUpdateUser(appName, userLogin, newUser);
      }

      #endregion

      #region Contexts

      public IEnumerable<SecContext> Contexts()
      {
         return GetContexts(String.Empty);
      }

      public IEnumerable<SecContext> Contexts(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetContexts(appName.ToLowerOrEmpty());
      }

      #endregion

      #region Objects

      public IEnumerable<SecObject> Objects()
      {
         return GetObjects(String.Empty, String.Empty);
      }

      public IEnumerable<SecObject> Objects(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetObjects(appName.ToLowerOrEmpty(), String.Empty);
      }

      public IEnumerable<SecObject> Objects(string appName, string contextName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         return GetObjects(appName.ToLowerOrEmpty(), contextName.ToLowerOrEmpty());
      }

      #endregion

      #region Entries

      public IList<SecEntry> Entries(string appName, string contextName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         return GetEntries(appName.ToLowerOrEmpty(), contextName.ToLowerOrEmpty(), String.Empty, String.Empty, EmptyStringArray);
      }

      public IList<SecEntry> Entries(string appName, string contextName, string userLogin, string[] groupNames)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsEmpty(groupNames as ICollection<string>);
         return GetEntries(appName.ToLowerOrEmpty(), contextName.ToLowerOrEmpty(), String.Empty, userLogin.ToLowerOrEmpty(), groupNames.Select(g => g.ToLower()).ToArray());
      }

      public IList<SecEntry> Entries(string appName, string contextName, string objectName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(objectName);
         return GetEntries(appName.ToLowerOrEmpty(), contextName.ToLowerOrEmpty(), objectName.ToLowerOrEmpty(), String.Empty, EmptyStringArray);
      }

      public IList<SecEntry> Entries(string appName, string contextName, string objectName, string userLogin, string[] groupNames)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(objectName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsEmpty(groupNames as ICollection<string>);
         return GetEntries(appName.ToLowerOrEmpty(), contextName.ToLowerOrEmpty(), objectName.ToLowerOrEmpty(), userLogin.ToLowerOrEmpty(), groupNames.Select(g => g.ToLower()).ToArray());
      }

      public void AddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsNull(secContext);
         Raise<ArgumentException>.IfIsEmpty(secContext.Name);
         Raise<ArgumentException>.IfIsNull(secObject);
         Raise<ArgumentException>.IfIsEmpty(secObject.Name);
         Raise<ArgumentException>.If(String.IsNullOrWhiteSpace(userLogin) && String.IsNullOrWhiteSpace(groupName));

         const string logShort = "Security entry for object '{0}' in context '{1}' has been added for '{2}'";
         const string logCtx = "Adding a new security entry";

         try
         {
            secContext.Name = secContext.Name.ToLower();
            secObject.Name = secObject.Name.ToLower();
            if (userLogin != null)
            {
               userLogin = userLogin.ToLower();
            }
            if (groupName != null)
            {
               groupName = groupName.ToLower();
            }
            DoAddEntry(appName.ToLower(), secContext, secObject, userLogin, groupName);
            Db.Logger.LogWarnAsync<TSec>(String.Format(logShort, secObject.Name, secContext.Name, userLogin ?? groupName), context: logCtx, applicationName: appName);
         }
         catch (Exception ex)
         {
            Db.Logger.LogWarnAsync<TSec>(ex, logCtx);
            throw;
         }
      }

      public void RemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(objectName);
         Raise<ArgumentException>.If(String.IsNullOrWhiteSpace(userLogin) && String.IsNullOrWhiteSpace(groupName));

         const string logShort = "Security entry for object '{0}' in context '{1}' has been removed for '{2}'";
         const string logCtx = "Removing a security entry";

         try
         {
            if (userLogin != null)
            {
               userLogin = userLogin.ToLower();
            }
            if (groupName != null)
            {
               groupName = groupName.ToLower();
            }
            DoRemoveEntry(appName.ToLower(), contextName.ToLower(), objectName.ToLower(), userLogin, groupName);
            Db.Logger.LogWarnAsync<TSec>(String.Format(logShort, objectName.ToLower(), contextName.ToLower(), userLogin ?? groupName), context: logCtx, applicationName: appName);
         }
         catch (Exception ex)
         {
            Db.Logger.LogWarnAsync<TSec>(ex, logCtx);
            throw;
         }
      }

      #endregion

      #region Abstract Methods

      protected abstract IEnumerable<SecApp> GetApps(string appName);

      protected abstract SecApp DoAddApp(SecApp app);

      protected abstract IEnumerable<SecGroup> GetGroups(string appName, string groupName);

      protected abstract void DoAddGroup(string appName, SecGroup newGroup);

      protected abstract void DoRemoveGroup(string appName, string groupName);

      protected abstract void DoUpdateGroup(string appName, string groupName, SecGroup newGroup);

      protected abstract IEnumerable<SecUser> GetUsers(string appName, string userLogin);

      protected abstract bool DoAddUser(string appName, SecUser newUser);

      protected abstract bool DoRemoveUser(string appName, string userLogin);

      protected abstract void DoUpdateUser(string appName, string userLogin, SecUser newUser);

      protected abstract IEnumerable<SecContext> GetContexts(string appName);

      protected abstract IEnumerable<SecObject> GetObjects(string appName, string contextName);

      protected abstract IList<SecEntry> GetEntries(string appName, string contextName, string objectType, string userLogin, string[] groupNames);

      protected abstract void DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

      protected abstract void DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName);

      #endregion
   }
}
