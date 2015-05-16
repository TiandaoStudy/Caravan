using System.Data.Common;
using System.Data.SqlClient;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.SqlServer
{
    internal sealed class SqlServerDataSourceManager : AbstractDataSourceManager
    {
        public override DataSourceKind DataSourceKind
        {
            get { return DataSourceKind.SqlServer; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            var connection = SqlClientFactory.Instance.CreateConnection();
            connection.ConnectionString = DataSource.ConnectionString;
            return connection;
        }
    }
}