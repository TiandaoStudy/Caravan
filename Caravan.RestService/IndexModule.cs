using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.Common.XmlSchemas.MenuEntries;
using LinqToQuerystring.Nancy;
using Nancy;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class IndexModule : CustomModule
   {
       private static readonly IList<Tuple<int, string>> TestData = new [] {
           Tuple.Create(1, "AAA"),
           Tuple.Create(2, "BBB")
       }; 

      public IndexModule()
      {
         Get["/"] = _ =>
         {
             Context.EnableOutputCache(30);
             return View["index"];
         };
         Get["/api"] = _ =>
         {
            Context.EnableOutputCache(30);
            return View["api"];
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