using System.Data;
using System.Data.Common;
using Dapper;
using Finsa.Caravan.Extensions;

namespace Finsa.Caravan.DataAccess.Core
{
    public abstract class QueryExecutorBase : IQueryExecutor
    {
       public abstract DatabaseVendor Vendor { get; }

       public DataTable FillDataTableFromQuery(string query)
        {
            using (var connection = OpenConnection())
            {
                return connection.Query(query).ToDataTable();
            }
        }

        public DataTable FillDataTableFromQuery(string query, object parameters)
        {
            using (var connection = OpenConnection())
            {
                return connection.Query(query, parameters).ToDataTable();
            }
        }

        public abstract void ElaborateConnectionString(ref string connectionString);

        public abstract DbConnection OpenConnection();
    }
}