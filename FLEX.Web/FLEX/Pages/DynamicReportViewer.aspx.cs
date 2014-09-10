using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using FLEX.Common.Data;
using FLEX.Web.UserControls.Ajax;
using Newtonsoft.Json;
using PommaLabs.GRAMPA.XML;
using DataTable = System.Data.DataTable;
using StringPair = PommaLabs.GRAMPA.Pair<string, string>;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class DynamicReportViewer : PageBase
   {
      #region Class Fields

      private static readonly Dictionary<string, Func<dynamic, DataControlField>> ColumnBuilders = new Dictionary<string, Func<dynamic, DataControlField>>
      {
         {"Bound", BuildDataGrid_BoundColumn}
      };

      private static readonly Dictionary<string, Func<Page, dynamic, Control>> ControlBuilders = new Dictionary<string, Func<Page, dynamic, Control>>
      {
         {"AutoSuggest", BuildSearchCriteria_AutoSuggest},
         {"CheckBoxList", BuildSearchCriteria_CheckBoxList}
      }; 

      #endregion

      #region Instance Fields

      #endregion

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
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.PageEvent);
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

         BuildSearchCriteria(repSearchCriteria, reportXml.Parameters);
         BuildDataGrid(fdtgReport, reportXml.Columns);
      }

      #region Search Criteria building from XML

      private static void BuildSearchCriteria(Repeater searchCriteria, dynamic paramsSpec)
      {
         var paramsList = new List<dynamic>();
         foreach (var paramSpec in paramsSpec.Parameter)
         {
            paramsList.Add(paramSpec);
         }
         searchCriteria.DataSource = paramsList;
         searchCriteria.DataBind();
      }

      protected void repSearchCriteria_OnItemDataBound(object sender, RepeaterItemEventArgs e)
      {
         if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
         {
            return;
         }

         dynamic paramSpec = e.Item.DataItem;

         var label = e.Item.FindControl("lblSearchCriterium") as Label;
         label.Text = paramSpec.Label;

         var placeHolder = e.Item.FindControl("plhSearchCriterium") as PlaceHolder;
         placeHolder.Controls.Add(ControlBuilders[paramSpec.ControlType](this, paramSpec));
      }

      private static Control BuildSearchCriteria_AutoSuggest(Page page, dynamic paramSpec)
      {
         var autoSuggest = page.LoadControl("~/FLEX/UserControls/Ajax/AutoSuggest.ascx") as AutoSuggest;
         Debug.Assert(autoSuggest != null);
         // AutoSuggest properties
         autoSuggest.XmlLookup = paramSpec.XmlLookup;
         autoSuggest.LookupBy = paramSpec.LookupBy;
         // IAjaxControl properties
         autoSuggest.Enabled = paramSpec.Enabled;
         autoSuggest.Visible = paramSpec.Visible;
         return autoSuggest;
      }

      private static Control BuildSearchCriteria_CheckBoxList(Page page, dynamic paramSpec)
      {
         var checkBoxList = page.LoadControl("~/FLEX/UserControls/Ajax/CollapsibleCheckBoxList.ascx") as CollapsibleCheckBoxList;
         Debug.Assert(checkBoxList != null);
         // CheckBoxList properties
         switch ((string) paramSpec.DataSourceType)
         {
            case "JSON":
               var list = JsonConvert.DeserializeObject<IList<StringPair>>(paramSpec.DataSource);
               checkBoxList.SetDataSource(list);
               break;
            case "SQL":
               var dataTable = QueryExecutor.Instance.FillDataTableFromQuery(paramSpec.DataSource);
               checkBoxList.SetDataSource(dataTable, paramSpec.ValueColumn, paramSpec.LabelColumn);
               break;
         }
         // IAjaxControl properties
         checkBoxList.Enabled = paramSpec.Enabled;
         checkBoxList.Visible = paramSpec.Visible;
         return checkBoxList;
      }

      #endregion

      #region Grid Building from XML

      private static void BuildDataGrid(UserControls.DataGrid dataGrid, dynamic columnsSpec)
      {
         dataGrid.DefaultSortExpression = columnsSpec.DefaultSortExpression;
         dataGrid.DefaultSortDirection = ParseSortDirection(columnsSpec.DefaultSortDirection);

         foreach (var columnSpec in columnsSpec.Column)
         {
            dataGrid.Columns.Add(ColumnBuilders[columnSpec.Type](columnSpec));
         }
      }

      private static DataControlField BuildDataGrid_BoundColumn(dynamic columnSpec)
      {
         return new BoundField
         {
            DataField = columnSpec.DataField,
            HeaderText = columnSpec.HeaderText ?? String.Empty,
            SortExpression = columnSpec.SortExpression ?? String.Empty,
            Visible = (columnSpec.Visible != null) ? Boolean.Parse(columnSpec.Visible.ToLower()) : true
         };
      }

      private static SortDirection ParseSortDirection(string sortDirection)
      {
         return (sortDirection != null) ? (SortDirection) Enum.Parse(typeof(SortDirection), sortDirection) : SortDirection.Ascending;
      }

      #endregion
   }
}