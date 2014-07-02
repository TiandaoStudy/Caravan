using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Thrower;

namespace FLEX.Common.Web
{
    public static class Conversions
    {
        public static string ToJson(this DataTable dt)
        {
            Raise<ArgumentNullException>.IfIsNull(dt);

            var columns = new Pair<DataColumn, string>[dt.Columns.Count];
            var idx = 0;
            foreach (DataColumn col in dt.Columns)
            {
                columns[idx++] = Pair.Create(col, col.ColumnName.Trim());
            }

            var rows = new Dictionary<string, object>[dt.Rows.Count];
            idx = 0;
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (var col in columns)
                {
                    row.Add(col.Second, dr[col.First]);
                }
                rows[idx++] = row;
            }

            return JsonConvert.SerializeObject(rows);
        }
    }
}