using System.Data.Common;

namespace Finsa.Caravan.DataAccess
{
   /// <summary>
   /// 
   /// </summary>
   public interface IQueryExecutor
   {
      DatabaseKind Kind { get; }

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

   public enum DatabaseKind
   {
      Dummy,
      Oracle,
      Postgres,
      Remote,
      SqlServer,
      SqlServerCe
   }
}