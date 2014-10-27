using System.IO;
using System.Web;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.XmlSchemas.MenuEntries;
using Nancy;
using PommaLabs.KVLite.Nancy;

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

         Get["/"] = _ =>
         {
            Context.EnableOutputCache(Configuration.LongCacheTimeoutInSeconds);
            return Db.Security.Apps();
         };
         
         Get["/{appName}"] = p =>
         {
            Context.EnableOutputCache(Configuration.LongCacheTimeoutInSeconds);
            return Db.Security.App((string) p.appName);
         };
         
         Get["/{appName}/groups"] = p => Response.AsJson(Db.Security.Groups((string) p.appName));
         Get["/{appName}/groups/{groupName}"] = p => Response.AsJson(Db.Security.Group((string) p.appName, (string) p.groupName));
         Post["/{appName}/groups"] = p => null;
         Put["/{appName}/groups/{groupName}"] = p => null;
         Delete["/{appName}/groups/{groupName}"] = p => null;

         Get["/{appName}/objects"] = p =>
         {
            Context.EnableOutputCache(Configuration.LongCacheTimeoutInSeconds);
            return null;
         };

         Get["/{appName}/users"] = p => Response.AsJson(Db.Security.Users((string) p.appName));
         
         Get["/{appName}/contexts"] = p => Response.AsJson(Db.Security.Contexts((string) p.appName));
      }
   }
}