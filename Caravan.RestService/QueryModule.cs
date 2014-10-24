using Finsa.Caravan.RestService.Core;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class QueryModule : CustomModule
   {
      public QueryModule() : base("query")
      {
         Post["/groups"] = _ =>
         {
            var query = Request.Form.query as string;
            Context.EnableOutputCache(Configuration.LongCacheTimeoutInSeconds, query);
            return null;
         };
      }
   }
}