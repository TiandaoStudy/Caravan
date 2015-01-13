using System;
using System.Data;
using System.Web.UI.WebControls;
using FLEX.WebForms.UserControls.Templates;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.WebForms.UserControls
// ReSharper restore CheckNamespace
{
   public sealed class DataGrid : GridView
   {
      // Blanks are needed to quickly build the sort expression
      private const string Ascending = " ASC";
      private const string Descending = " DESC";
      
      private const string DefaultSortExpressionViewStateKey = "DataGrid.DefaultSortExpression";
      private const string DefaultSortDirectionViewStateKey = "DataGrid.DefaultSortDirection";

      private const string DataSrcViewStateTag = "__DATAGRID_DATASOURCE__";
      private const string SortDirViewStateTag = "__DATAGRID_SORTDIRECTION__";
      private const string SortExpViewStateTag = "__DATAGRID_SORTEXPRESSION__";

      public DataGrid()
      {
         // Default settings - Paging
         AllowPaging = true;
         PagerTemplate = new ControlTemplate<DataGridPager>(OnPagerInit);
         PageIndexChanging += OnPageIndexChanging;
         PageSize = 10;

         // Default settings - Sorting
         AllowSorting = true;
         DefaultSortDirection = SortDirection.Ascending;
         Sorting += OnSorting;

         // Default settings - Styling
         BorderWidth = CellPadding = CellSpacing = 0;
         CssClass = "datagrid table table-striped table-hover";
         GridLines = GridLines.Both;

         // Default settings - Miscellanea
         AutoGenerateColumns = false;
         EmptyDataTemplate = new ControlTemplate<DataGridEmpty>();
      }

      #region Public Properties

      public string DefaultSortExpression
      {
         get { return (string) ViewState[DefaultSortExpressionViewStateKey]; }
         set { ViewState[DefaultSortExpressionViewStateKey] = value; }
      }

      public SortDirection DefaultSortDirection
      {
         get { return (SortDirection) ViewState[DefaultSortDirectionViewStateKey]; }
         set { ViewState[DefaultSortDirectionViewStateKey] = value; }
      }

      public event EventHandler DataSourceUpdating;

      public new SortDirection SortDirection
      {
         get
         {
            if (ViewState[SortDirViewStateTag] == null)
            {
               ViewState[SortDirViewStateTag] = DefaultSortDirection;
            }
            return (SortDirection) ViewState[SortDirViewStateTag];
         }
         set { ViewState[SortDirViewStateTag] = value; }
      }

      public new string SortExpression
      {
         get
         {
            if (ViewState[SortExpViewStateTag] == null)
            {
               ViewState[SortExpViewStateTag] = DefaultSortExpression;
            }
            return (string) ViewState[SortExpViewStateTag];
         }
         set { ViewState[SortExpViewStateTag] = value; }
      }

      #endregion

      #region GridView Overrides

      protected override void OnPagePreLoad(object sender, EventArgs e)
      {
         base.OnPagePreLoad(sender, e);
         DataSource = ViewState[DataSrcViewStateTag];
      }

      private void OnPageIndexChanging(object s, GridViewPageEventArgs e)
      {
         PageIndex = e.NewPageIndex;
         DataSource = SortCachedDataSource();
         DataBind();
      }

      private static void OnPagerInit(DataGridPager pager)
      {
         pager.ShowFirstAndLast = true;
         pager.ShowNextAndPrevious = true;
         pager.PageLinksToShow = 5;
         pager.NextText = "›";
         pager.PreviousText = "‹";
         pager.FirstText = "«";
         pager.LastText = "»";
      }

      protected override void OnPreRender(EventArgs e)
      {
         base.OnPreRender(e);
         MakeAccessible();
      }

      protected override void OnRowDataBound(GridViewRowEventArgs e)
      {
         base.OnRowDataBound(e);
         MakeSortable(e);
      }

      private void OnSorting(object s, GridViewSortEventArgs e)
      {
         var oldSortExp = SortExpression;
         var newSortExp = SortExpression = e.SortExpression;
         
         // If sort expression is the same, then we apply the complementary sorting.
         if (oldSortExp == newSortExp)
         {
            SortDirection = (SortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
         }
         // Otherwise, the expression is new, therefore we apply the default sort direction.
         else
         {
            SortDirection = DefaultSortDirection;
         }        
         
         var sortedData = SortCachedDataSource();
         DoUpdateDataSource(sortedData);
      }

      #endregion

      #region Public Methods

      public void UpdateDataSource()
      {
         var ev = DataSourceUpdating;
         if (ev != null)
         {
            ev(this, EventArgs.Empty);
         }
         DoUpdateDataSource(DataSource);
      }

      #endregion

      #region Private Methods

      private void DoUpdateDataSource(object dataSource)
      {
         // Reset pager
         PageIndex = 0;
         // Cache and sort data source
         ViewState[DataSrcViewStateTag] = dataSource;
         DataSource = SortCachedDataSource();
         // Redraw grid
         DataBind();
      }

      private void MakeAccessible()
      {
         UseAccessibleHeader = true;
         if (Rows.Count <= 0) return;
         HeaderRow.TableSection = TableRowSection.TableHeader;
         if (ShowFooter)
         {
            FooterRow.TableSection = TableRowSection.TableFooter;
         }
      }

      private void MakeSortable(GridViewRowEventArgs args)
      {
         if (args.Row.RowType != DataControlRowType.Header || String.IsNullOrEmpty(SortExpression))
         {
            return;
         }

         var cellIdx = -1;
         foreach (DataControlField field in Columns)
         {
            if (field.SortExpression == SortExpression)
            {
               cellIdx = Columns.IndexOf(field);
            }
            else if (!String.IsNullOrEmpty(field.SortExpression))
            {
               var tmpIdx = Columns.IndexOf(field);
               const string notSorted = "datagrid-not-sorted";
               args.Row.Cells[tmpIdx].CssClass += notSorted;
            }
         }

         if (cellIdx > -1)
         {
            var css = (SortDirection == SortDirection.Ascending) ? "datagrid-sort-asc" : "datagrid-sort-desc";
            args.Row.Cells[cellIdx].CssClass += css;
         }
      }

      private object SortCachedDataSource()
      {
         var dataSrc = ViewState[DataSrcViewStateTag];
         if (dataSrc == null)
         {
            return null;
         }

         var sortDirStr = (SortDirection == SortDirection.Ascending) ? Ascending : Descending;
         var sortExp = SortExpression + sortDirStr; // Blank is included in direction

         var dataView = dataSrc as DataView;
         if (dataView != null)
         {
            dataView.Sort = sortExp;
            return dataView.Table;
         }

         var dataTable = dataSrc as DataTable;
         if (dataTable != null)
         {
            dataTable.DefaultView.Sort = sortExp;
            return dataTable;
         }

         // Data source type not handled
         return null;
      }

      #endregion
   }
}