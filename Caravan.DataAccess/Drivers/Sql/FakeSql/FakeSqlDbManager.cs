using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Sql.FakeSql
{
    internal sealed class FakeSqlDbManager : DbManagerBase
    {
        private DbConnection _connection;

        public FakeSqlDbManager()
        {
            ResetConnection();
        }

        public override DataAccessKind Kind
        {
            get { return DataAccessKind.FakeSql; }
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