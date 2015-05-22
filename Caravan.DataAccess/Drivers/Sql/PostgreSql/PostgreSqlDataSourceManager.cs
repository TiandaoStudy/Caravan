using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;
using Npgsql;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.PostgreSql
{
    internal sealed class PostgreSqlDataSourceManager : AbstractDataSourceManager
    {
        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.PostgreSql; }
        }

        public override string ElaborateConnectionString(string connectionString)
        {
            // Nothing to do with the connection string.
            return connectionString;
        }

        public override DbConnection CreateConnection()
        {
            var connection = NpgsqlFactory.Instance.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }
    }
}