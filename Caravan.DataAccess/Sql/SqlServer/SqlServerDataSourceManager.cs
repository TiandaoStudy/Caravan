using System.Data.Common;
using System.Data.SqlClient;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Sql.SqlServer
{
    internal sealed class SqlServerDataSourceManager : AbstractDataSourceManager
    {
        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.SqlServer; }
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