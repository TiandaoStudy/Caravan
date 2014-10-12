using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class DataView : MasterPage, IPage
   {
      protected void Page_Init(object sender, EventArgs e)
      {
         Master.Page_Visible += Page_Visible_Proxy;
      }

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