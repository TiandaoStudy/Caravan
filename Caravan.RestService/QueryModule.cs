using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.RestService.Core;

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