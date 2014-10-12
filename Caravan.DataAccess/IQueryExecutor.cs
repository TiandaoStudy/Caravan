using System.Data;

namespace FLEX.DataAccess
{
   /// <summary>
   /// 
   /// </summary>
   public interface IQueryExecutor
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="query"></param>
      /// <returns></returns>
      DataTable FillDataTableFromQuery(string query);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="query"></param>
      /// <param name="parameters"></param>
      /// <returns></returns>
      DataTable FillDataTableFromQuery(string query, dynamic parameters);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="connectionString"></param>
      void ElaborateConnectionString(ref string connectionString);

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      IDbConnection OpenConnection();
   }
}