using Dapper;
using Finsa.Caravan.DataAccess.Core;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Finsa.Caravan.DataAccess.Sql.SqlServer
{
    internal sealed class SqlServerDataSourceManager : AbstractDataSourceManager
    {
        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.SqlServer; }
        }

        public override DateTime DataSourceDateTime
        {
            get
            {
                using (var connection = OpenConnection())
                {
                    const string query = "TODO";
                    return connection.ExecuteScalar<DateTime>(query);
                }
            }
        }

        public override string ElaborateConnectionString(string connectionString)
        {
            // Nothing to do with the connection string.
            return connectionString;
        }

        public override DbConnection CreateConnection()
        {
            var connection = SqlClientFactory.Instance.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }
    }
}
