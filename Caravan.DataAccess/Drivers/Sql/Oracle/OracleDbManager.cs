using System.Data.Common;
using System.Globalization;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Properties;
using Oracle.ManagedDataAccess.Client;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Oracle
{
    internal sealed class OracleDbManager : DbManagerBase
    {
        public override DataAccessKind Kind
        {
            get { return DataAccessKind.Oracle; }
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
                connectionString += string.Format("Statement Cache Size={0};", Settings.Default.OracleStatementCacheSize);
            }
        }

        public override DbConnection CreateConnection()
        {
            var connection = OracleClientFactory.Instance.CreateConnection();
            connection.ConnectionString = Db.ConnectionString;
            return connection;
        }
    }
}