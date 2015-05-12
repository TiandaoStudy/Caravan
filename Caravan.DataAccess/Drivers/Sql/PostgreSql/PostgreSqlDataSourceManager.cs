using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;
using Npgsql;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.PostgreSql
{
    internal sealed class PostgreSqlDataSourceManager : AbstractDataSourceManager
    {
        public override DataSourceKind DataSourceKind
        {
            get { return DataSourceKind.PostgreSql; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            var connection = NpgsqlFactory.Instance.CreateConnection();
            connection.ConnectionString = Db.ConnectionString;
            return connection;
        }
    }
}