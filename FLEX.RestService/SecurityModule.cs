using System.IO;
using System.Web;
using Nancy;

namespace FLEX.RestService
{
   public sealed class SecurityModule : NancyModule
   {
      public SecurityModule() : base("security")
      {
         Get["menu"] = p => Response.AsXml(File.ReadAllText(HttpContext.Current.Server.MapPath("~/bin/MenuBar.xml")));
      }
   }
}