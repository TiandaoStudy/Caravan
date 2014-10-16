using System.Collections.Generic;
using System.Data;
using Dapper;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Oracle
{
   public sealed class OracleSecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApplications(string applicationName)
      {
         var query = @"
            select capp_name Name, capp_description Description
              from {0}caravan_sec_app
             where (:appName is null or capp_name = lower(:appName))
         ";
         query = string.Format(query, Configuration.Instance.OracleRunner);

         var parameters = new DynamicParameters();
         parameters.Add("appName", applicationName, DbType.AnsiString);

         using (var ctx = QueryExecutor.Instance.OpenConnection())
         {
            return ctx.Query<SecApp>(query, parameters);
         }
      }
   }
}
