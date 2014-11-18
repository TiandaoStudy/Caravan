using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.WebForms.UserControls.Templates
// ReSharper restore CheckNamespace
{
   [ToolboxData(@"<{0}:DataGridPager runat=""server"" PersistentDataSource=true></{0}:DataGridPager>")]
   internal sealed class DataGridPager : UserControl
   {
      #region Properties

      /// <summary>
      ///     Gets or sets the number of results.
      /// </summary>
      /// <value> The number of results. </value>
      public int NumberOfResults { get; set; }

      /// <summary>
      ///     Gets or sets the results to show per page.
      /// </summary>
      /// <value> The results to show per page. </value>
      public int ResultsToShowPerPage { get; set; }

      /// <summary>
      ///     Gets or sets the page links to show for pagination links.
      /// </summary>
      /// <value> The page links to show. </value>
      public int PageLinksToShow { get; set; }

      /// <summary>
      ///     Gets or sets a value indicating whether [show first and last].
      /// </summary>
      /// <value>
      ///     <c>true</c> if [show first and last]; otherwise, <c>false</c> .
      /// </value>
      public bool ShowFirstAndLast { get; set; }

      /// <summary>
      ///     Gets or sets a value indicating whether [show next and previous].
      /// </summary>
      /// <value>
      ///     <c>true</c> if [show next and previous]; otherwise, <c>false</c> .
      /// </value>
      public bool ShowNextAndPrevious { get; set; }

      /// <summary>
      ///     Gets or sets the next text.
      /// </summary>
      /// <value> The next text. </value>
      public string NextText { get; set; }

      /// <summary>
      ///     Gets or sets the previous text.
      /// </summary>
      /// <value> The previous text. </value>
      public string PreviousText { get; set; }

      /// <summary>
      ///     Gets or sets the first text.
      /// </summary>
      /// <value> The first text. </value>
      public string FirstText { get; set; }

      /// <summary>
      ///     Gets or sets the last text.
      /// </summary>
      /// <value> The last text. </value>
      public string LastText { get; set; }

      public DataGrid TheGrid { get; private set; }

      #endregion

      /// <summary>
      ///     Handles the Load event of the Page control.
      /// </summary>
      /// <param name="args">
      ///     The <see cref="System.EventArgs" /> instance containing the event data.
      /// </param>
      protected override void OnLoad(EventArgs args)
      {
         base.OnLoad(args);

         ResultsToShowPerPage = 5;

         var control = Parent;
         while (control != null)
         {
            var view = control as DataGrid;
            if (view != null)
            {
               TheGrid = view;
               break;
            }
            control = control.Parent;
         }
      }

      /// <summary>
      ///     Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
      /// </summary>
      protected override void CreateChildControls()
      {
         CreateGridInfo();
         CreatePagination();
      }

      private void CreatePagination()
      {
         Controls.Add(new Literal {Text = "<div class=\"text-right datagrid-pager\"><ul class=\"pagination\">"});

         var min = Math.Min(Math.Max(0, TheGrid.PageIndex - (PageLinksToShow/2)),
            Math.Max(0, TheGrid.PageCount - PageLinksToShow + 1));
         var max = Math.Min(TheGrid.PageCount, min + PageLinksToShow);

         if (ShowFirstAndLast)
         {
            AddLink(FirstText ?? "First", "firstPage", TheGrid.PageIndex > 0, "First");
         }

         if (ShowNextAndPrevious)
         {
            AddLink(PreviousText ?? "Previous", "previousPage", TheGrid.PageIndex > 0, "Prev");
         }

         for (var i = min; i < max; i++)
         {
            AddLink((i + 1).ToString(CultureInfo.InvariantCulture), "selected", TheGrid.PageIndex != i, (i + 1).ToString(CultureInfo.InvariantCulture), TheGrid.PageIndex == i);
         }

         if (ShowNextAndPrevious)
         {
            AddLink(NextText ?? "Next", "nextPage", TheGrid.PageIndex < TheGrid.PageCount - 1, "Next");
         }
         if (ShowFirstAndLast)
         {
            AddLink(LastText ?? "Last", "lastPage", TheGrid.PageIndex < TheGrid.PageCount - 1, "Last");
         }      

         Controls.Add(new Literal {Text = "</ul></div>"});
      }

      private void CreateGridInfo()
      {
         const string infoMsgFmt = "Showing {0} to {1} of {2} entries";

         Controls.Add(new Literal {Text = "<div class=\"text-left datagrid-info\"><em>"});

         if (TheGrid != null)
         {
            var count = 0;
            
            var dataView = TheGrid.DataSource as DataView;
            if (dataView != null)
            {
               count = dataView.Count;
            }
            else
            {
               var dataTable = TheGrid.DataSource as DataTable;
               if (dataTable != null)
               {
                  count = dataTable.Rows.Count;
               }
            }

            // The index, starting from 1, of the FIRST row of the page.
            var minIdx = TheGrid.PageIndex*TheGrid.PageSize + 1;
            // The index, starting from 1, of the LAST row of the page.
            var maxIdx = Math.Min(count, minIdx + TheGrid.PageSize - 1);

            var infoMsg = String.Format(infoMsgFmt, minIdx, maxIdx, count);
            Controls.Add(new Literal {Text = infoMsg});
         }

         Controls.Add(new Literal {Text = "</em></div>"});
      }

      /// <summary>
      /// Adds the link for the page (and next/last etc) or a label if its a deactivated link
      /// </summary>
      /// <param name="text">The text.</param>
      /// <param name="cssClass">The CSS class.</param>
      /// <param name="addAsLink">if set to <c>true</c> [add as link].</param>
      /// <param name="commandArgument">The command argument.</param>
      private void AddLink(String text, String cssClass, bool addAsLink, string commandArgument, bool current = false)
      {
         if (addAsLink)
         {
            Controls.Add(new Literal {Text = "<li class='text-center'>"});
            var button = new LinkButton
            {
               ID = "Page" + text,
               CommandName = "Page",
               CommandArgument = commandArgument,
               Text = text
            };

            Controls.Add(button);
         }
         else if (current)
         {
            Controls.Add(new Literal {Text = "<li class='text-center active'>"});
            Controls.Add(new Label {Text = text, CssClass = cssClass});
         }
         else
         {
            Controls.Add(new Literal {Text = "<li>"});
            Controls.Add(new Label {Text = text, CssClass = cssClass});
         }
         Controls.Add(new Literal {Text = "</li>"});
      }
   }
}