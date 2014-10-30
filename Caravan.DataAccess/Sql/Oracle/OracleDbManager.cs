using System.Data.Common;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

namespace Finsa.Caravan.DataAccess.Sql.Oracle
{
   public sealed class OracleDbManager : IDbManager
   {
      private static readonly OracleClientFactory DbFactory = new OracleClientFactory();

      public DataAccessKind Kind
      {
         get { return DataAccessKind.Oracle; }
      }

      public void ElaborateConnectionString(ref string connectionString)
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
            connectionString += string.Format("Statement Cache Size={0};", Configuration.Instance.OracleStatementCacheSize);
         }
      }

      public DbConnection OpenConnection()
      {
         var connection = DbFactory.CreateConnection();
         connection.ConnectionString = Configuration.Instance.ConnectionString;
         connection.Open();
         return connection;
      }
   }
}