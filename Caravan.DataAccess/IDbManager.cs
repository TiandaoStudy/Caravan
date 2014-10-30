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
   }

   public enum DataAccessKind
   {
      Dummy,
      Oracle,
      Postgres,
      Rest,
      SqlServer,
      SqlServerCe
   }
}