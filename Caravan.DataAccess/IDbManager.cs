using System.Data.Common;

namespace Finsa.Caravan.DataAccess
{
   /// <summary>
   /// 
   /// </summary>
   public interface IDbManager
   {
      DataAccessKind Kind { get; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="connectionString"></param>
      void ElaborateConnectionString(ref string connectionString);

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      DbConnection OpenConnection();

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      DbConnection CreateConnection();
   }

   public enum DataAccessKind : byte
   {
      Oracle = 0,
      PostgreSql = 1,
      Rest = 2,
      SqlServer = 3,
      SqlServerCe = 4,
      MongoDb = 5
   }
}