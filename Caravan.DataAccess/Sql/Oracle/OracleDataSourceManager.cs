using System.Data.Common;
using System.Globalization;
using Finsa.Caravan.DataAccess.Core;
using Oracle.ManagedDataAccess.Client;

namespace Finsa.Caravan.DataAccess.Sql.Oracle
{
    internal sealed class OracleDataSourceManager : AbstractDataSourceManager
    {
        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.Oracle; }
        }

        public override string ElaborateConnectionString(string connectionString)
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
                connectionString += string.Format("Statement Cache Size={0};", CaravanDataAccessConfiguration.Instance.OracleStatementCacheSize);
            }

            return connectionString;
        }

        public override DbConnection CreateConnection()
        {
            var connection = OracleClientFactory.Instance.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }
    }
}
