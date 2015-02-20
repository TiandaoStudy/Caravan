using Finsa.Caravan.DataAccess.Core;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Sql.MySql
{
    public sealed class MySqlDbManager : DbManagerBase
    {
        private static readonly DbProviderFactory DbFactory = MySql.MySqlDbManager.DbFactory;

        public override DataAccessKind Kind
        {
            get { return DataAccessKind.MySql; }
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