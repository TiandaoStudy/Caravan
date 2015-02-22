using Finsa.Caravan.DataAccess.Core;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Sql.MySql
{
    internal sealed class MySqlDbManager : DbManagerBase
    {
        private static readonly DbProviderFactory DbFactory = global::MySql.Data.MySqlClient.MySqlClientFactory.Instance;

        public override DataAccessKind Kind
        {
            get { return DataAccessKind.MySql; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            var connection = DbFactory.CreateConnection();
            connection.ConnectionString = Db.ConnectionString;
            return connection;
        }
    }
}