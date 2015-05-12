using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.FakeSql
{
    internal sealed class FakeSqlDataSourceManager : AbstractDataSourceManager
    {
        private DbConnection _connection;

        public FakeSqlDataSourceManager()
        {
            ResetConnection();
        }

        public override DataSourceKind DataSourceKind
        {
            get { return DataSourceKind.FakeSql; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            // Nothing to do with the connection string.
        }

        public override DbConnection CreateConnection()
        {
            // Close connection, since someone might have left it open.
            _connection.Close();
            return _connection;
        }

        public void ResetConnection()
        {
            // Connection should be persisted, otherwise the DB may be lost.
            _connection = Effort.DbConnectionFactory.CreatePersistent("caravan");
        }
    }
}