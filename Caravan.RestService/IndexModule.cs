using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.XmlSchemas.MenuEntries;
using LinqToQuerystring.Nancy;
using Nancy;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class IndexModule : CustomModule
   {
       private static readonly IList<CPair<int, string>> TestData = new [] {
           CPair.Create(1, "AAA"),
           CPair.Create(2, "BBB")
       }; 

      public IndexModule()
      {
         Get["/"] = _ =>
         {
             Context.EnableOutputCache(30);
             return View["index"];
         };
         Get["/query"] = _ => FormatterExtensions.AsJson(Response, TestData.AsQueryable().LinqToQuerystring((IDictionary<string, object>) Context.Request.Query));
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
      }
   }
}