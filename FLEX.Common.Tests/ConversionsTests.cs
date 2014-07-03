using System;
using System.Data;
using System.Globalization;

namespace FLEX.Common.Web.Tests
{
    public sealed class ConversionsTests : TestBase
    {
        public void DataTable_ToJson_EmptyGrid()
        {
            
        }

        private DataTable GenerateRandomDataTable(int rowCount, int columnCount)
        {
            var rand = new Random();
            var dt = new DataTable();

            for (var i = 0; i < columnCount; ++i)
            {
                var colTag = rand.Next(100, 999).ToString(CultureInfo.InvariantCulture);
                dt.Columns.Add(colTag);
            }

            for (var i = 0; i < rowCount; ++i)
            {
                var tags = new object[columnCount];
                for (var j = 0; j < columnCount; ++j)
                {
                    tags[j] = rand.Next(100, 999).ToString(CultureInfo.InvariantCulture);
                }
                dt.Rows.Add(tags);
            }

            return dt;
        }
    }
}
