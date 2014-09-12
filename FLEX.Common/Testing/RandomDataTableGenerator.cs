using System;
using System.Data;
using System.Globalization;
using System.Linq;
using PommaLabs.GRAMPA.Diagnostics;

namespace FLEX.Common.Testing
{
    public sealed class RandomDataTableGenerator
    {
        private readonly Random _random = new Random();
        private readonly string[] _columnNames;

        public RandomDataTableGenerator(params string[] columnNames)
        {
            Raise<ArgumentNullException>.IfIsNull(columnNames);
            Raise<ArgumentException>.If(columnNames.Any(String.IsNullOrEmpty));

            _columnNames = columnNames.Clone() as string[];
        }

        public DataTable GenerateDataTable(int rowCount)
        {
            Raise<ArgumentOutOfRangeException>.If(rowCount < 0);

            var dt = new DataTable("RANDOMLY_GENERATED_DATA_TABLE");
            foreach (var columnName in _columnNames)
            {
                dt.Columns.Add(columnName);
            }
            for (var i = 0; i < rowCount; ++i)
            {
                var row = new object[_columnNames.Length];
                for (var j = 0; j < row.Length; ++j)
                {
                    row[j] = _random.Next(100, 999).ToString(CultureInfo.InvariantCulture);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
