using System;
using System.IO;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using FLEX.Common.XML;
using DataTable = System.Data.DataTable;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class DynamicReportViewer : PageBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            // Page must be built from XML only once.
            if (!IsPostBack)
            {
               BuildPage();
            }

            fdtgReport.UpdateDataSource();
         }
         catch (Exception)
         {
            throw;
         }
      }

      #region Grid Events

      protected void fdtgReport_OnDataSourceUpdating(object sender, EventArgs e)
      {
         var dt = new DataTable();
         dt.Columns.Add("First");
         dt.Columns.Add("Second");
         dt.Rows.Add(1, 4);
         dt.Rows.Add(2, 5);
         fdtgReport.DataSource = dt;
      }

      #endregion

      private void BuildPage()
      {
         var reportXmlPath = Server.MapPath(Path.Combine(Configuration.Instance.DynamicReportsFolder, "SampleReport.xml"));
         dynamic reportXml = DynamicXml.Load(reportXmlPath);

         BuildDataGrid(fdtgReport, reportXml.Columns);
      }

      #region Grid Building from XML

      private static void BuildDataGrid(UserControls.DataGrid dataGrid, dynamic columnsSpec)
      {
         dataGrid.DefaultSortExpression = columnsSpec.DefaultSortExpression;
         dataGrid.DefaultSortDirection = ParseSortDirection(columnsSpec.DefaultSortDirection);

         foreach (var columnSpec in columnsSpec.Column)
         {
            switch ((string) columnSpec.Type)
            {
               case "Bound":
                  BuildDataGrid_BoundColumn(dataGrid, columnSpec);
                  break;
            }
         }
      }

      private static void BuildDataGrid_BoundColumn(UserControls.DataGrid dataGrid, dynamic columnSpec)
      {
         var boundField = new BoundField
         {
            DataField = columnSpec.DataField,
            HeaderText = columnSpec.HeaderText ?? String.Empty
         };
         dataGrid.Columns.Add(boundField);
      }

      private static SortDirection ParseSortDirection(string sortDirection)
      {
         return (sortDirection != null) ? (SortDirection) Enum.Parse(typeof(SortDirection), sortDirection) : SortDirection.Ascending;
      }

      #endregion
   }
}