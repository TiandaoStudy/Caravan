using System.Data;

namespace FLEX.Common.Data
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
      /// <returns></returns>
      IDbConnection OpenConnection();
   }
}