using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.RestService.Core;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class QueryModule : CustomModule
   {
      public QueryModule() : base("query")
      {
         Post["/{appName}/groups"] = p =>
         {
            var appName = (string) p.appName;
            var queryString = (string) Request.Form.query;

            // Context.EnableOutputCache(Configuration.LongCacheTimeoutInSeconds, new {appName, queryString});
            
            return new SecGroupList
            {
               Groups = Db.Query.Groups(appName, queryString)
            };
         };
      }
   }
}