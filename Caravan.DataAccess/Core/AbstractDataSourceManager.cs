using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class AbstractDataSourceManager : IDataSourceManager
    {
        public abstract DataSourceKind DataSourceKind { get; }

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