using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Thrower;

namespace FLEX.Common
{
   /// <summary>
   /// 
   /// </summary>
   public static class Conversions
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataTable"></param>
      /// <returns></returns>
      public static string ToJson(this DataTable dataTable)
      {
         Raise<ArgumentNullException>.IfIsNull(dataTable);

         var columns = new Pair<DataColumn, string>[dataTable.Columns.Count];
         var idx = 0;
         foreach (DataColumn col in dataTable.Columns)
         {
            columns[idx++] = Pair.Create(col, col.ColumnName.Trim());
         }

         var rows = new Dictionary<string, object>[dataTable.Rows.Count];
         idx = 0;
         foreach (DataRow dr in dataTable.Rows)
         {
            rows[idx++] = columns.ToDictionary(col => col.Second, col => dr[col.First]);
         }

         return JsonConvert.SerializeObject(rows);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="enumerable"></param>
      /// <returns></returns>
      public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable)
      {
         // Create DataTable Structure
         PropertyDescriptorCollection properties;
         var table = CreateTable<T>(out properties);
         // Get the list item and add into the list
         foreach (var item in enumerable)
         {
            var row = table.NewRow();
            foreach (PropertyDescriptor property in properties)
            {
               row[property.Name] = property.GetValue(item) ?? DBNull.Value;
            }
            table.Rows.Add(row);
         }
         return table;
      }

      #region Private Methods

      private static DataTable CreateTable<T>(out PropertyDescriptorCollection properties)
      {
         // T –> ClassName
         var entityType = typeof (T);
         // Set the datatable name as class name
         var table = new DataTable(entityType.Name);
         // Get the property list
         properties = TypeDescriptor.GetProperties(entityType);
         foreach (PropertyDescriptor property in properties)
         {
            // Add property as column
            var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            table.Columns.Add(property.Name, type);
         }
         return table;
      }

      #endregion
   }
}