using System.IO;
using System.Web;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Rest;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.XmlSchemas.MenuEntries;
using Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class SecurityModule : CustomModule
   {
      public SecurityModule() : base("security")
      {
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

         Post["/"] = _ =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return RestResponse.Success(Db.Security.Apps());
         };
         
         Post["/{appName}"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return RestResponse.Success(Db.Security.App((string) p.appName));
         };

         /*
          * Contexts
          */
         
         Get["/{appName}/contexts"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return Db.Security.Contexts((string) p.appName);
         };

         /*
          * Entries
          */

         Get["/{appName}/entries"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return null;
         };

         /*
          * Groups
          */
         
         Get["/{appName}/groups"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return Db.Security.Groups((string) p.appName);
         };

         Get["/{appName}/groups/{groupName}"] = p => Response.AsJson(Db.Security.Group((string) p.appName, (string) p.groupName));
         Post["/{appName}/groups"] = p => null;
         Put["/{appName}/groups/{groupName}"] = p => null;
         Delete["/{appName}/groups/{groupName}"] = p => null;

         /*
          * Objects
          */

         Post["/{appName}/objects"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return Db.Security.Objects((string) p.appName);
         };

         /*
          * Users
          */

         Post["/{appName}/users"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            return Db.Security.Users((string) p.appName);
         };
      }
   }
}