using System.IO;
using System.Web;
using FLEX.RestService.Core;
using Nancy;

namespace FLEX.RestService
{
   public sealed class SecurityModule : NancyModule
   {
      public SecurityModule() : base("security")
      {
         Get["menu"] = p =>
         {
            var xml = File.ReadAllText(HttpContext.Current.Server.MapPath("~/bin/MenuBar.xml"));
            using (var stream = new StringReader(xml))
            {
               Menu.DeserializeFrom(stream);
            }
            return xml;
         };
      }
   }
}