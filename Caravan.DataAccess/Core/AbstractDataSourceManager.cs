using System;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class AbstractDataSourceManager : ICaravanDataSourceManager
    {
        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionString = ElaborateConnectionString(CaravanDataAccessConfiguration.Instance.ConnectionString);
                }
                return _connectionString;
            }
            set
            {
                _connectionString = ElaborateConnectionString(value);
            }
        }

        public abstract CaravanDataSourceKind DataSourceKind { get; }

        public abstract string ElaborateConnectionString(string connectionString);

        public DbConnection OpenConnection()
        {
            var connection = CreateConnection();
            connection.Open();
            return connection;
        }

        public abstract DbConnection CreateConnection();
    }
}