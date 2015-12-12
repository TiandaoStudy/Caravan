using Finsa.Caravan.DataAccess.Core;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Sql.Effort
{
    internal sealed class EffortDataSourceManager : AbstractDataSourceManager
    {
        private DbConnection _connection;

        public EffortDataSourceManager()
        {
            ResetConnection();
        }

        public override CaravanDataSourceKind DataSourceKind { get; } = CaravanDataSourceKind.Effort;

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
            _connection = global::Effort.DbConnectionFactory.CreatePersistent("caravan");
        }
    }
}