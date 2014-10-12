using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan;
using LinqToQuerystring.Nancy;
using Nancy;

namespace FLEX.RestService
{
   public sealed class IndexModule : NancyModule
   {
       private static readonly IList<GPair<int, string>> TestData = new GPair<int, string>[] {
           GPair.Create(1, "AAA"),
           GPair.Create(2, "BBB")
       }; 

      public IndexModule()
      {
         Get["/"] = parameters => View["index"];
          Get["/query"] = _ => FormatterExtensions.AsJson(Response, TestData.AsQueryable().LinqToQuerystring((IDictionary<string, object>) Context.Request.Query));
      }
   }
}