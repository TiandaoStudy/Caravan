using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Oracle
{
   public sealed class OracleSecurityManager : SecurityManagerBase
   {
      protected override IEnumerable<SecApp> GetApplications(string appName)
      {
         var query = QueryExecutor.OracleQuery(@"
            select capp_id Id, capp_name Name, capp_description Description
              from {0}caravan_sec_app
             where (:appName is null or capp_name = lower(:appName))
             order by capp_name
         ");

         var parameters = new DynamicParameters();
         parameters.Add("appName", appName, DbType.AnsiString);

         using (var ctx = QueryExecutor.Instance.OpenConnection())
         {
            return ctx.Query<SecApp>(query, parameters);
         }
      }

      protected override IEnumerable<SecGroup> GetGroups(string appName)
      {
         var query = QueryExecutor.OracleQuery(@"
            select cgrp_id Id, cgrp_name Name, cgrp_description Description, cgrp_admin IsAdmin,
                   sa.capp_id, sa.capp_id Id, sa.capp_name Name, sa.capp_description Description
              from {0}caravan_sec_group sg
              join {0}caravan_sec_app sa on (sg.capp_id = sa.capp_id)
             where (:appName is null or capp_name = lower(:appName))
             order by capp_name, cgrp_name
         ");

         var parameters = new DynamicParameters();
         parameters.Add("appName", appName, DbType.AnsiString);

         using (var ctx = QueryExecutor.Instance.OpenConnection())
         {
            var groups = ctx.Query<SecGroup, SecApp, SecGroup>(query, (g, a) => {
               g.App = a;
               return g;
            }, parameters, splitOn: "capp_id").ToList();

            foreach (var group in groups)
            {
               query = QueryExecutor.OracleQuery(@"
                  select cgrp_id Id, cgrp_name Name, cgrp_description Description, cgrp_admin IsAdmin,
                         sa.capp_id, sa.capp_id Id, sa.capp_name Name, sa.capp_description Description
                    from {0}caravan_sec_user su
                   where (:appName is null or capp_name = lower(:appName))
                   order by capp_name, cgrp_name
               ");
            }

            return groups;
         }
      }
   }
}
