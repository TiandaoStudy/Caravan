using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Sql.FakeSql
{
    internal sealed class FakeSqlDataSourceManager : AbstractDataSourceManager
    {
        private DbConnection _connection;

        public FakeSqlDataSourceManager()
        {
            ResetConnection();
        }

        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.FakeSql; }
        }

        public override string ElaborateConnectionString(string connectionString)
        {
            // Nothing to do with the connection string.
            return connectionString;
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