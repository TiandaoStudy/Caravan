using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FLEX.Common.Data;

namespace FLEX.Extensions.TestDataAccess
{
   public sealed class QueryExecutor : IQueryExecutor
   {
      private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["QueryProvider"].ConnectionString;

      public int ExecuteStoredProcedure(string storedProcedure)
      {
         using (var conn = new SqlConnection(ConnectionString))
         {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = storedProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteNonQuery();
         }
      }

      public int ExecuteQuery(string query)
      {
         using (var conn = new SqlConnection(ConnectionString))
         {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteNonQuery();
         }
      }

      public DataTable FillDataTableFromQuery(string query)
      {
         using (var conn = new SqlConnection(ConnectionString))
         {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            using (var adapter = new SqlDataAdapter(cmd))
            {
               var table = new DataTable();
               adapter.Fill(table);
               return table;
            }
         }
      }
   }
}