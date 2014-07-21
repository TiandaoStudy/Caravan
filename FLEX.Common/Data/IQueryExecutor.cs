using System.Data;

namespace FLEX.Common.Data
{
    public interface IQueryExecutor
    {
        DataTable FillDataTableFromQuery(string query);
    }
}
