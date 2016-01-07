using System.Data.Common;
using System.Data.SqlServerCe;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Sql.SqlServerCe
{
    internal sealed class SqlServerCeDataSourceManager : AbstractDataSourceManager
    {
        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.SqlServerCe; }
        }

        public override string ElaborateConnectionString(string connectionString)
        {
            // Nothing to do with the connection string.
            return connectionString;
        }

        public override DbConnection CreateConnection()
        {
            var connection = SqlCeProviderFactory.Instance.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }
    }
}