using System;
using System.Collections.Generic;
using System.Linq;
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
         return GetApps(null);
      }

      public SecApp App(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetApps(appName).FirstOrDefault();
      }

      #endregion

      #region Groups

      public IEnumerable<SecGroup> Groups(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetGroups(appName, null);
      }
      
      public SecGroup Group(string appName, string groupName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(groupName);
         return GetGroups(appName, groupName).FirstOrDefault();
      }

      public void AddGroup(string appName, SecGroup group)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsNull(group);
         DoAddGroup(appName, group);
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
         return GetUsers(appName, null);
      }

      public SecUser User(string appName, string userLogin)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         return GetUsers(appName, userLogin).FirstOrDefault();
      }

      #endregion

      #region Contexts

      public IEnumerable<SecContext> Contexts()
      {
         return GetContexts(null);
      }

      public IEnumerable<SecContext> Contexts(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetContexts(appName);
      }

      #endregion

      #region Objects

      public IEnumerable<SecObject> Objects()
      {
         return GetObjects(null, null);
      }

      public IEnumerable<SecObject> Objects(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetObjects(appName, null);
      }

      public IEnumerable<SecObject> Objects(string appName, string contextName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         return GetObjects(appName, contextName);
      }

      #endregion

      #region Entries

      public IList<SecEntry> Entries(string appName, string contextName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         return GetEntries(appName.ToLower(), contextName.ToLower(), null, null, EmptyStringArray);
      }

      public IList<SecEntry> Entries(string appName, string contextName, string objectType)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(objectType);
         return GetEntries(appName.ToLower(), contextName.ToLower(), objectType.ToLower(), null, EmptyStringArray);
      }

      public IList<SecEntry> Entries(string appName, string contextName, string objectType, string userLogin, string[] groupNames)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfIsEmpty(contextName);
         Raise<ArgumentException>.IfIsEmpty(objectType);
         Raise<ArgumentException>.IfIsEmpty(userLogin);
         Raise<ArgumentException>.IfIsEmpty(groupNames as ICollection<string>);
         return GetEntries(appName.ToLower(), contextName.ToLower(), objectType.ToLower(), userLogin.ToLower(), groupNames.Select(g => g.ToLower()).ToArray());
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

      protected abstract IEnumerable<SecGroup> GetGroups(string appName, string groupName);

      protected abstract void DoAddGroup(string appName, SecGroup group);

      protected abstract void DoRemoveGroup(string appName, string groupName);

      protected abstract void DoUpdateGroup(string appName, string groupName, SecGroup newGroup);

      protected abstract IEnumerable<SecUser> GetUsers(string appName, string userLogin);

      protected abstract IEnumerable<SecContext> GetContexts(string appName);

      protected abstract IEnumerable<SecObject> GetObjects(string appName, string contextName);

      protected abstract IList<SecEntry> GetEntries(string appName, string contextName, string objectType, string userLogin, string[] groupNames);

      protected abstract void DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName);

      protected abstract void DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName);

      #endregion
   }
}
