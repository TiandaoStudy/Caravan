using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Sql.FakeSql
{
    internal sealed class FakeSqlDbManager : DbManagerBase
    {
        private readonly DbConnection _connection = Effort.DbConnectionFactory.CreatePersistent("caravan");

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
            _connection.Close();
            return _connection;
        }
    }
}