using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FLEX.Web.UserControls;
using FLEX.Web.UserControls.Ajax;
using ImageButton = FLEX.Web.UserControls.Ajax.ImageButton;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class VerticalSearch : MasterPage, IPage
   {
      protected void Page_Init(object sender, EventArgs e)
      {
         Master.Page_Visible += Page_Visible_Proxy;
      }

      #region Public Properties

      public ImageButton ClearButton
      {
         get { return btnClear; }
      }

      public SearchButton SearchButton
      {
         get { return btnSearch; }
      }

      public Panel SearchCriteriaPanel
      {
         get { return divSearchCriteria; }
      }

      public Panel DataGridPanel
      {
         get { return divDataGrid; }
      }

      #endregion

      #region IPage Members

      public ErrorHandler ErrorHandler
      {
         get { return Master.ErrorHandler; }
      }

      public bool HasPageVisibleHandlers
      {
         get { return Page_Visible != null; }
      }

      public HtmlForm MainForm
      {
         get { return Master.MainForm; }
      }

      public MenuBar MenuBar
      {
         get { return Master.MenuBar; }
      }

      public PageFooter PageFooter
      {
         get { return Master.PageFooter; }
      }

      public ScriptManager ScriptManager
      {
         get { return Master.ScriptManager; }
      }

      public event EventHandler Page_Visible;

      #endregion

      #region Private Members

      private void Page_Visible_Proxy(object sender, EventArgs e)
      {
         Page_Visible(this, e);
      }

      #endregion
   }
}