using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using FLEX.Common;
using FLEX.Common.Data;
using FLEX.Common.Web;
using FLEX.Web.UserControls.Ajax;
using Newtonsoft.Json;
using PommaLabs.GRAMPA;
using PommaLabs.GRAMPA.Extensions;
using PommaLabs.GRAMPA.XML;
using Thrower;
using Pair = PommaLabs.GRAMPA.Pair;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class DynamicReportViewer : PageBaseListAndSearch
   {
      #region Class Fields

      private const string ReportNameRequestKey = "reportName";

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

      private readonly List<Pair<ISearchControl, string>> _searchControls = new List<Pair<ISearchControl, string>>();
      private Pair<string, CommandType> _queryInfo; 

      #endregion

      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            // Report name is mandatory.
            Raise<InvalidOperationException>.IfIsEmpty(Request[ReportNameRequestKey], WebErrorMessages.Pages_DynamicReportViewer_MissingReportName);

            // Configuration for the VerticalSearch search button.
            Master.SearchButton.Visible = true;
            Master.SearchButton.Click += SearchButton_Click;

            // Page is built starting from the XML specification.
            BuildPage(Request[ReportNameRequestKey]);

            // There should not be an initial update of the data grid,
            // since we are using a search button.
            // NO --> fdtgReport.UpdateDataSource(); <-- NO
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.PageEvent);
         }
      }

      #region Grid Events

      protected void fdtgReport_OnDataSourceUpdating(object sender, EventArgs e)
      {
         // Parameters construction
         var parameters = new DynamicParameters();
         foreach (var searchControl in _searchControls)
         {
            parameters.Add(searchControl.Second, searchControl.First.DynamicSelectedValues);
         }

         switch (_queryInfo.Second)
         {
            case CommandType.Text:
               var dataTable = QueryExecutor.Instance.FillDataTableFromQuery(_queryInfo.First, parameters);
               fdtgReport.DataSource = dataTable;
               break;
            case CommandType.StoredProcedure:
               break;
         }
      }

      #endregion

      private void BuildPage(string reportName)
      {
         var reportXmlPath = Server.MapPath(Path.Combine(Configuration.Instance.DynamicReportsFolder, reportName + Constants.XmlExtension));
         dynamic reportXml = DynamicXml.Load(reportXmlPath);

         _queryInfo = RetrieveQueryInfo(reportXml.Query);

         BuildSearchCriteria(repSearchCriteria, reportXml.Parameters);
         
         if (!IsPostBack)
         {
            // We need to build data grid only once, at page load.
            BuildDataGrid(fdtgReport, reportXml.Columns);
         }
      }

      protected override void FillSearchCriteria()
      {
      }

      protected override void RegisterSearchCriteria(SearchCriteria criteria)
      {
         foreach (var control in _searchControls)
         {
            criteria.RegisterControl(control.First, control.Second);
         }
         criteria.CriteriaChanged += SearchCriteria_CriteriaChanged;
      }

      private void SearchCriteria_CriteriaChanged(SearchCriteria criteria, SearchCriteriaChangedArgs args)
      {
         try
         {
            fdtgReport.UpdateDataSource();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      private void SearchButton_Click(object sender, EventArgs args)
      {
         try
         {
            SearchCriteria_CriteriaChanged(SearchCriteria, new SearchCriteriaChangedArgs());
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      #region Query Handling

      private static Pair<string, CommandType> RetrieveQueryInfo(dynamic querySpec)
      {
         var commandType = StringExtensions.ToEnumOrDefault(querySpec.Type, CommandType.Text);
         return Pair.Create(querySpec.Code, commandType);
      }

      #endregion

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
         var control = ControlBuilders[paramSpec.ControlType](this, paramSpec);
         placeHolder.Controls.Add(control);
         _searchControls.Add(Pair.Create(control as ISearchControl, paramSpec.UniqueName));
      }

      private static Control BuildSearchCriteria_AutoSuggest(Page page, dynamic paramSpec)
      {
         var autoSuggest = page.LoadControl("~/FLEX/UserControls/Ajax/AutoSuggest.ascx") as AutoSuggest;
         Debug.Assert(autoSuggest != null);
         // AutoSuggest properties
         autoSuggest.XmlLookup = paramSpec.XmlLookup;
         autoSuggest.LookupBy = paramSpec.LookupBy;
         autoSuggest.MinLengthForHint = StringExtensions.ToInt32OrDefault(paramSpec.MinLengthForHint, AutoSuggest.MinLengthForHintDefaultValue);
         // IAjaxControl properties
         autoSuggest.Enabled = StringExtensions.ToBooleanOrDefault(paramSpec.Enabled, AjaxControlBase.EnabledDefaultValue);
         autoSuggest.Visible = StringExtensions.ToBooleanOrDefault(paramSpec.Visible, true);
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
               var list = JsonConvert.DeserializeObject<IList<Pair<string, string>>>(paramSpec.DataSource);
               checkBoxList.SetDataSource(list);
               break;
            case "SQL":
               var dataTable = QueryExecutor.Instance.FillDataTableFromQuery(paramSpec.DataSource);
               checkBoxList.SetDataSource(dataTable, paramSpec.ValueColumn, paramSpec.LabelColumn);
               break;
         }
         // IAjaxControl properties
         checkBoxList.Enabled = StringExtensions.ToBooleanOrDefault(paramSpec.Enabled, AjaxControlBase.EnabledDefaultValue);
         checkBoxList.Visible = StringExtensions.ToBooleanOrDefault(paramSpec.Visible, true);
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