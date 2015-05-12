using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.MySql
{
    internal sealed class MySqlDataSourceManager : AbstractDataSourceManager
    {
        public override DataSourceKind DataSourceKind
        {
            get { return DataSourceKind.MySql; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            var connection = global::MySql.Data.MySqlClient.MySqlClientFactory.Instance.CreateConnection();
            connection.ConnectionString = Db.ConnectionString;
            return connection;
        }
    }
}