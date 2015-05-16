using System.Data.Common;
using System.Data.SqlServerCe;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.SqlServerCe
{
    internal sealed class SqlServerCeDataSourceManager : AbstractDataSourceManager
    {
        public override DataSourceKind DataSourceKind
        {
            get { return DataSourceKind.SqlServerCe; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            var connection = SqlCeProviderFactory.Instance.CreateConnection();
            connection.ConnectionString = DataSource.ConnectionString;
            return connection;
        }
    }
}