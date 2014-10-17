using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.RestService.Core;
using LinqToQuerystring.Nancy;
using Nancy;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class IndexModule : CustomModule
   {
       private static readonly IList<GPair<int, string>> TestData = new [] {
           GPair.Create(1, "AAA"),
           GPair.Create(2, "BBB")
       }; 

      public IndexModule()
      {
         Get["/"] = _ =>
         {
             Context.EnableOutputCache(30);
             return View["index"];
         };
         Get["/query"] = _ => FormatterExtensions.AsJson(Response, TestData.AsQueryable().LinqToQuerystring((IDictionary<string, object>) Context.Request.Query));
      }
   }
}