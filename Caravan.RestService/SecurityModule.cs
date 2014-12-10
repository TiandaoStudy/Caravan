using System;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Exceptions;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.RestService.Properties;
using Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class SecurityModule : CustomModule
   {
      public SecurityModule() : base("security")
      {
         /*
          * Apps
          */

         Post[""] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetApps);
         Post["/{appName}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetApp);

         /*
          * Contexts
          */

         Post["/{appName}/contexts"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetContexts);

         /*
          * Entries
          */

         Post["/{appName}/entries/{contextName}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetEntries);
         Post["/{appName}/entries"] = p => SafeResponse<SecEntrySingle>(p, NotCached, (Func<dynamic, SecEntrySingle, dynamic>) GetEntries);
         Post["/{appName}/entries/{contextName}/{objectName}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetEntries);
         Put["/{appName}/entries"] = p => SafeResponse<SecEntrySingle>(p, NotCached, (Func<dynamic, SecEntrySingle, dynamic>) AddEntry);
         Delete["/{appName}/entries/{contextName}/{objectName}"] = p => SafeResponse<SecEntrySingle>(p, NotCached, (Func<dynamic, SecEntrySingle, dynamic>) RemoveEntry);

         /*
          * Groups
          */

         Post["/{appName}/groups"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetGroups);
         Post["/{appName}/groups/{groupName}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetGroup);
         Put["/{appName}/groups"] = p => SafeResponse<SecGroupSingle>(p, NotCached, (Func<dynamic, SecGroupSingle, dynamic>) AddGroup);
         Patch["/{appName}/groups/{groupName}"] = p => SafeResponse<SecGroupSingle>(p, NotCached, (Func<dynamic, SecGroupSingle, dynamic>) UpdateGroup);
         Delete["/{appName}/groups/{groupName}"] = p => SafeResponse<dynamic>(p, NotCached, (Func<dynamic, dynamic, dynamic>) RemoveGroup);

         /*
          * Objects
          */

         Post["/{appName}/objects"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetObjects);

         /*
          * Users
          */

         Post["/{appName}/users"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetUsers);
         Post["/{appName}/users/{userLogin}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetUser);
         Put["/{appName}/users"] = p => SafeResponse<SecUserSingle>(p, NotCached, (Func<dynamic, SecUserSingle, dynamic>) AddUser);
         Patch["/{appName}/users/{userLogin}"] = p => SafeResponse<SecUserSingle>(p, NotCached, (Func<dynamic, SecUserSingle, dynamic>) UpdateUser);
         Delete["/{appName}/users/{userLogin}"] = p => SafeResponse<dynamic>(p, NotCached, (Func<dynamic, dynamic, dynamic>) RemoveUser);

         /* 
          * Users and Groups
          */

         Put["/{appName}/users/{userLogin}/{groupName}"] = p => SafeResponse<SecUserSingle>(p, NotCached, (Func<dynamic, SecUserSingle, dynamic>) AddUserToGroup);
         Delete["/{appName}/users/{userLogin}/{groupName}"] = p => SafeResponse<SecUserSingle>(p, NotCached, (Func<dynamic, SecUserSingle, dynamic>) RemoveUserFromGroup);
      }

      private static dynamic GetApp(dynamic p, dynamic body)
      {
         return new SecAppSingle {App = Db.Security.App(p.appName)};
      }

      private static dynamic GetApps(dynamic p, dynamic body)
      {
         var apps = Db.Security.Apps();
         return new SecAppList {Apps = apps};
      }

      private static dynamic GetContexts(dynamic p, dynamic body)
      {
         var contexts = Db.Security.Contexts(p.appName);
         return new SecContextList {Contexts = contexts};
      }

      private static dynamic GetEntries(dynamic p, dynamic body)
      {
         var entries = (p.objectName == null)
            ? Db.Security.Entries(p.appName, p.contextName)
            : Db.Security.EntriesForObject(p.appName, p.contextName, p.objectName);
         return new SecEntryList {Entries = entries};
      }

      private static dynamic AddEntry(dynamic p, SecEntrySingle body)
      {
         var secEntry = body.Entry;
         if (secEntry.User.Login != null)
         {
            Db.Security.AddEntry(p.appName, secEntry.Context, secEntry.Object, secEntry.User.Login, null);
         }
         else
         {
            Db.Security.AddEntry(p.appName, secEntry.Context, secEntry.Object, null, secEntry.Group.Name);
         }

         return Success;
      }

      private static dynamic RemoveEntry(dynamic p, SecEntrySingle body)
      {
         var secEntry = body.Entry;

         if (secEntry.User.Login != null)
         {
            Db.Security.RemoveEntry(p.appName, p.contextName, p.objectName, secEntry.User.Login, null);
         }
         else
         {
            Db.Security.RemoveEntry(p.appName, p.contextName, p.objectName, null, secEntry.Group.Name);
         }

         return Success;
      }

      private static dynamic GetGroups(dynamic p, dynamic body)
      {
         var groups = Db.Security.Groups(p.appName);
         return new SecGroupList {Groups = groups};
      }

      private static dynamic GetGroup(dynamic p, dynamic body)
      {
         var group = Db.Security.Group(p.appName, p.groupName);
         return new SecGroupSingle {Group = group};
      }

      private static dynamic AddGroup(dynamic p, SecGroupSingle body)
      {
         Db.Security.AddGroup(p.appName, body.Group);
         return Success;
      }

      private static dynamic UpdateGroup(dynamic p, SecGroupSingle body)
      {
         Db.Security.UpdateGroup(p.appName, p.groupName, body.Group);
         return Success;
      }

      private static dynamic RemoveGroup(dynamic p, dynamic body)
      {
         Db.Security.RemoveGroup(p.appName, p.groupName);
         return Success;
      }

      private static dynamic GetObjects(dynamic p, dynamic body)
      {
         var objects = Db.Security.Objects(p.appName);
         return new SecObjectList {Objects = objects};
      }

      private static dynamic GetUsers(dynamic p, dynamic body)
      {
         var users = Db.Security.Users(p.appName);
         return new SecUserList {Users = users};
      }

      private static dynamic GetUser(dynamic p, dynamic body)
      {
         var user = Db.Security.User(p.appName, p.userLogin);
         return new SecUserSingle {User = user};
      }

      private static dynamic AddUser(dynamic p, SecUserSingle body)
      {
         try
         {
            Db.Security.AddUser(p.appName, body.User);
         }
         catch (AppNotFoundException ex)
         {
            return ErrorResponse(HttpStatusCode.NotFound, ex.Message);
         }
         catch (UserExistingException ex)
         {
            return ErrorResponse(HttpStatusCode.Conflict, ex.Message);
         }
         return Success;
      }

      private static dynamic UpdateUser(dynamic p, SecUserSingle body)
      {
         Db.Security.UpdateUser(p.appName, p.userLogin, body.User);
         return Success;
      }

      private static dynamic RemoveUser(dynamic p, dynamic body)
      {
         Db.Security.RemoveUser(p.appName, p.userLogin);
         return Success;
      }

      private static dynamic AddUserToGroup(dynamic p, SecUserSingle body)
      {
         Db.Security.AddUserToGroup(p.appName, p.userLogin, p.groupName);
         return Success;
      }

      private static dynamic RemoveUserFromGroup(dynamic p, SecUserSingle body)
      {
         Db.Security.RemoveUserFromGroup(p.appName, p.userLogin, p.groupName);
         return Success;
      }

      private static Response ErrorResponse(HttpStatusCode statusCode, string errorMessage)
      {
         var response = (Response) errorMessage;
         response.StatusCode = statusCode;
         return response;
      }
   }
}