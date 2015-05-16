using System.Data.Common;
using System.Globalization;
using Finsa.Caravan.DataAccess.Core;
using Oracle.ManagedDataAccess.Client;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Oracle
{
    internal sealed class OracleDataSourceManager : AbstractDataSourceManager
    {
        public override DataSourceKind DataSourceKind
        {
            get { return DataSourceKind.Oracle; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
            var lowerConnString = connectionString.ToLower(CultureInfo.InvariantCulture);

            // Add a semicolor in order to avoid issues
            connectionString = connectionString.TrimEnd();
            if (!connectionString.EndsWith(";"))
            {
                connectionString += ';';
            }

            // Connection Pooling
            if (!lowerConnString.Contains("pooling"))
            {
                connectionString += "Pooling=true;";
            }

            // Statement Cache
            if (!lowerConnString.Contains("statement cache size"))
            {
                connectionString += string.Format("Statement Cache Size={0};", DataAccessConfiguration.Instance.OracleStatementCacheSize);
            }
        }

        public override DbConnection CreateConnection()
        {
            var connection = OracleClientFactory.Instance.CreateConnection();
            connection.ConnectionString = DataSource.ConnectionString;
            return connection;
        }
    }
}
