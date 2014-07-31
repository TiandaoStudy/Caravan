using System.Data;

namespace FLEX.Common.Data
{
   public interface IQueryExecutor
   {
      string ConnectionString { get; }

      DataTable FillDataTableFromQuery(string query);
   }
}