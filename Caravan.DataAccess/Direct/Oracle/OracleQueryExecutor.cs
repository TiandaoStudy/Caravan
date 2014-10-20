using System.Data.Common;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

namespace Finsa.Caravan.DataAccess.Oracle
{
   public sealed class OracleQueryExecutor : IQueryExecutor
   {
      private static readonly OracleClientFactory DbFactory = new OracleClientFactory();

      public DatabaseKind Kind
      {
         get { return DatabaseKind.Oracle; }
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