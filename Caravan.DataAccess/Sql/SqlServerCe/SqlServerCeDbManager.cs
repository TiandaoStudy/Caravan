using System.Data.Common;
using System.Data.SqlServerCe;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Sql.SqlServerCe
{
    internal sealed class SqlServerCeDbManager : DbManagerBase
    {
        public override DataAccessKind Kind
        {
            get { return DataAccessKind.SqlServerCe; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            var connection = SqlCeProviderFactory.Instance.CreateConnection();
            connection.ConnectionString = Db.ConnectionString;
            return connection;
        }
    }
}