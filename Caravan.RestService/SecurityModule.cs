using System.IO;
using System.Web;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Rest;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.RestService.Core;
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
         
         Post["/{appName}"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return RestResponse.Success(new SecAppSingle {App = Db.Security.App(p.appName)});
         };

         /*
          * Contexts
          */
         
         Post["/{appName}/contexts"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var contexts = Db.Security.Contexts(p.appName);
            return RestResponse.Success(new SecContextList {Contexts = contexts});
         };

         /*
          * Entries
          */

         Post["/{appName}/entries/{contextName}"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var entries = Db.Security.Entries(p.appName, p.contextName);
            return RestResponse.Success(new SecEntryList {Entries = entries});
         };

         Put["/{appName}/entries/{contextName}"] = p =>
         {
            var secEntry = StartSafeResponse<SecEntrySingle>(NotCached).Entry;
            Db.Security.AddEntry(p.appName, secEntry.Context, secEntry.Object, secEntry.User.Login, secEntry.Group.Name);
            return RestResponse.Success("...");
         };

         Delete["/{appName}/entries/{contextName}"] = p =>
         {
            var secEntry = StartSafeResponse<SecEntrySingle>(NotCached).Entry;
            Db.Security.RemoveEntry(p.appName, p.contextName, secEntry.Object.Name, secEntry.User.Login, secEntry.Group.Name);
            return RestResponse.Success("...");
         };

         /*
          * Groups
          */

         Post["/{appName}/groups"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var groups = Db.Security.Groups(p.appName);
            return RestResponse.Success(new SecGroupList {Groups = groups});
         };

         Post["/{appName}/groups/{groupName}"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var group = Db.Security.Group(p.appName, p.groupName);
            return RestResponse.Success(new SecGroupSingle {Group = group});
         };

         Put["/{appName}/groups"] = p => null;

         Patch["/{appName}/groups/{groupName}"] = p => null;

         Delete["/{appName}/groups/{groupName}"] = p => null;

         /*
          * Objects
          */

         Post["/{appName}/objects"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var objects = Db.Security.Objects(p.appName);
            return RestResponse.Success(new SecObjectList {Objects = objects});
         };

         /*
          * Users
          */

         Post["/{appName}/users"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var users = Db.Security.Users(p.appName);
            return RestResponse.Success(new SecUserList {Users = users});
         };

         Post["/{appName}/users/{userLogin}"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var user = Db.Security.User(p.appName, p.userLogin);
            return RestResponse.Success(new SecUserSingle {User = user});
         };

         Put["/{appName}/users"] = p => null;

         Patch["/{appName}/users/{userLogin}"] = p => null;

         Delete["/{appName}/users/{userLogin}"] = p => null;
      }
   }
}