using Finsa.Caravan.DataAccess.Core;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Sql.SqlServer
{
    public sealed class SqlServerDbManager : DbManagerBase
    {
        private static readonly DbProviderFactory DbFactory = System.Data.SqlClient.SqlClientFactory.Instance;

        public override DataAccessKind Kind
        {
            get { return DataAccessKind.SqlServer; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            connectionString = connectionString;
        }

        public override DbConnection CreateConnection()
        {
            var connection = DbFactory.CreateConnection();
            connection.ConnectionString = Db.ConnectionString;
            return connection;
        }
    }
}