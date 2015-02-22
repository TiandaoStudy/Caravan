using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Core
{
    public abstract class DbManagerBase : IDbManager
    {
        public abstract DataAccessKind Kind { get; }

        public abstract void ElaborateConnectionString(ref string connectionString);

        public DbConnection OpenConnection()
        {
            var connection = CreateConnection();
            connection.Open();
            return connection;
        }

        public abstract DbConnection CreateConnection();
    }
}