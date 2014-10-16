using System.IO;
using System.Web;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.RestService.Core;
using FLEX.Common.XmlSchemas.MenuEntries;
using Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class SecurityModule : CaravanModule
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

         Get["/applications"] = _ => Response.AsJson(SecurityManager.Instance.Applications());
         Get["/applications/{applicationName}"] = p => Response.AsJson(SecurityManager.Instance.Application((string) p.applicationName));
      }
   }
}