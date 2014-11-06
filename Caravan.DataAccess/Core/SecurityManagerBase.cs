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

      public IList<SecApp> Apps()
      {
         return GetApps(null);
      }

      public SecApp App(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         var app = GetApps(appName.ToLower()).FirstOrDefault();
         if (app == null)
         {
            throw new AppNotFoundException();
         }
         return app;
      }

      public void AddApp(SecApp app)
      {
         Raise<ArgumentNullException>.IfIsNull(app);
         Raise<ArgumentException>.IfIsEmpty(app.Name);
         if (!DoAddApp(app))
         {
            throw new AppExistingException();
         }
      }

      #endregion

      #region Groups

      public IList<SecGroup> Groups(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetGroups(appName.ToLower(), null);
      }
      
      public SecGroup Group(string appName, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         var group = GetGroups(appName.ToLower(), groupName.ToLower()).FirstOrDefault();
         if (group == null)
         {
            throw new GroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);
         }
         return group;
      }

      public void AddGroup(string appName, SecGroup newGroup)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentNullException>.IfIsNull(newGroup);
         Raise<ArgumentException>.IfIsEmpty(newGroup.Name);
         if (!DoAddGroup(appName, newGroup))
         {
            throw new GroupExistingException();
         }
      }

      public void RemoveGroup(string appName, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         if (!DoRemoveGroup(appName, groupName))
         {
            throw new GroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);   
         }
      }

      public void UpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         Raise<ArgumentNullException>.IfIsNull(newGroup);
         Raise<ArgumentException>.IfIsEmpty(newGroup.Name);
         if (!DoUpdateGroup(appName, groupName, newGroup))
         {
            throw new GroupNotFoundException(ErrorMessages.Core_SecurityManagerBase_GroupNotFound);   
         }
      }

      #endregion

      #region Users

      public IList<SecUser> Users(string appName)
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
         Raise<ArgumentNullException>.IfIsNull(newUser);
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
         Raise<ArgumentNullException>.IfIsNull(newUser);
         Raise<ArgumentException>.IfIsEmpty(newUser.Login);
         if (!DoUpdateUser(appName, userLogin, newUser))
         {
            throw new UserNotFoundException();
         }
      }

      public void AddUserToGroup(string appName, string userLogin, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         try
         {
            if (!DoAddUserToGroup(appName, userLogin, groupName))
            {
               throw new UserExistingException();
            }
         }
         catch (Exception ex)
         {
            Db.Logger.LogErrorAsync<SecurityManagerBase>(ex, "Adding an user to a group");
            throw;
         }
      }

      public void RemoveUserFromGroup(string appName, string userLogin, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         try
         {
            if (!DoRemoveUserFromGroup(appName, userLogin, groupName))
            {
               throw new UserNotFoundException();
            }
         }
         catch (Exception ex)
         {
            Db.Logger.LogErrorAsync<SecurityManagerBase>(ex, "Removing an user from a group");
            throw;
         }
      }

      #endregion

      #region Contexts

      public IList<SecContext> Contexts(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetContexts(appName.ToLower());
      }

      #endregion

      #region Objects

      public IList<SecObject> Objects(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetObjects(appName.ToLower(), null);
      }

      public IList<SecObject> Objects(string appName, string contextName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         return GetObjects(appName.ToLower(), contextName.ToLower());
      }

      #endregion

      #region Entries

      public IList<SecEntry> Entries(string appName, string contextName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         return GetEntries(appName.ToLower(), contextName.ToLower(), null, null, EmptyStringArray);
      }

      public IList<SecEntry> Entries(string appName, string contextName, string userLogin, string[] groupNames)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsEmpty(groupNames as ICollection<string>);
         return GetEntries(appName.ToLower(), contextName.ToLower(), null, userLogin.ToLower(), groupNames.Select(g => g.ToLower()).ToArray());
      }

      public IList<SecEntry> Entries(string appName, string contextName, string objectName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(objectName);
         return GetEntries(appName.ToLower(), contextName.ToLower(), objectName.ToLower(), null, EmptyStringArray);
      }

      public IList<SecEntry> Entries(string appName, string contextName, string objectName, string userLogin, string[] groupNames)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(objectName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsEmpty(groupNames as ICollection<string>);
         return GetEntries(appName.ToLower(), contextName.ToLower(), objectName.ToLower(), userLogin.ToLower(), groupNames.Select(g => g.ToLower()).ToArray());
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

      protected abstract IList<SecApp> GetApps(string appName);

      protected abstract bool DoAddApp(SecApp app);

      protected abstract IList<SecGroup> GetGroups(string appName, string groupName);

      protected abstract bool DoAddGroup(string appName, SecGroup newGroup);

      protected abstract bool DoRemoveGroup(string appName, string groupName);

      protected abstract bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup);

      protected abstract IList<SecUser> GetUsers(string appName, string userLogin);

      protected abstract bool DoAddUser(string appName, SecUser newUser);

      protected abstract bool DoRemoveUser(string appName, string userLogin);

      protected abstract bool DoUpdateUser(string appName, string userLogin, SecUser newUser);

      protected abstract bool DoAddUserToGroup(string appName, string userLogin, string groupName);

      protected abstract bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName);

      protected abstract IList<SecContext> GetContexts(string appName);

      protected abstract IList<SecObject> GetObjects(string appName, string contextName);

      protected abstract IList<SecEntry> GetEntries(string appName, string contextName, string objectType, string userLogin, string[] groupNames);

      protected abstract bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

      protected abstract bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName);

      #endregion
   }
}
