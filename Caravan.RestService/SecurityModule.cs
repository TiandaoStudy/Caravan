using System;
using System.IO;
using System.Web;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Rest;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.RestService.Properties;
using Finsa.Caravan.XmlSchemas.MenuEntries;

namespace Finsa.Caravan.RestService
{
   public sealed class SecurityModule : CustomModule
   {
      public SecurityModule() : base("security")
      {
         // DA TOGLIERE!!!
         Get["/menu"] = p =>
         {
            var xml = File.ReadAllText(HttpContext.Current.Server.MapPath("~/bin/MenuBar.xml"));
            using (var stream = new StringReader(xml))
            {
               Menu.DeserializeFrom(stream);
            }
            return xml;
         };

         /*
          * Apps
          */
         
         Post["/{appName}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetApp);

         /*
          * Contexts
          */
         
         Post["/{appName}/contexts"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetContexts);

         /*
          * Entries
          */

         Post["/{appName}/entries/{contextName}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetEntries);

         Put["/{appName}/entries/{contextName}"] = p =>
         {
            var secEntry = StartSafeResponse<SecEntrySingle>(NotCached).Entry;
         };

         Delete["/{appName}/entries/{contextName}"] = p =>
         {
            var secEntry = StartSafeResponse<SecEntrySingle>(NotCached).Entry;
         };

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
      }

      private static dynamic GetApp(dynamic p, dynamic body)
      {
         return new SecAppSingle {App = Db.Security.App(p.appName)};
      }

      private static dynamic GetContexts(dynamic p, dynamic body)
      {
         var contexts = Db.Security.Contexts(p.appName);
         return new SecContextList {Contexts = contexts};
      }

      private static dynamic GetEntries(dynamic p, dynamic body)
      {
         var entries = Db.Security.Entries(p.appName, p.contextName);
         return new SecEntryList {Entries = entries};
      }

      private static dynamic AddEntry(dynamic p, SecEntrySingle body)
      {
         var secEntry = body.Entry;
         Db.Security.AddEntry(p.appName, secEntry.Context, secEntry.Object, secEntry.User.Login, secEntry.Group.Name);
         return Success;
      }

      private static dynamic RemoveEntry(dynamic p, SecEntrySingle body)
      {
         var secEntry = body.Entry;
         Db.Security.RemoveEntry(p.appName, p.contextName, secEntry.Object.Name, secEntry.User.Login, secEntry.Group.Name);
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
         Db.Security.AddUser(p.appName, body.User);
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
   }
}