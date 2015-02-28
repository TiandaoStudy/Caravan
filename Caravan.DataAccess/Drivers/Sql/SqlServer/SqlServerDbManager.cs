using System.Data.SqlClient;
using Finsa.Caravan.DataAccess.Core;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.SqlServer
{
    internal sealed class SqlServerDbManager : DbManagerBase
    {
        public override DataAccessKind Kind
        {
            get { return DataAccessKind.SqlServer; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            var connection = SqlClientFactory.Instance.CreateConnection();
            connection.ConnectionString = Db.ConnectionString;
            return connection;
        }
    }
}