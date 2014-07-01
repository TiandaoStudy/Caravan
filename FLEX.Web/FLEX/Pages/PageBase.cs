using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.Core;
using FLEX.Web.MasterPages;
using FLEX.Web.UserControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PageBase : Page, IPageBase
   {
      protected override sealed PageStatePersister PageStatePersister
      {
         get { return new CacheViewStatePersister(this); }
      }

      #region IPageBase Members

      public UserControls.Ajax.ErrorHandler ErrorHandler { get; private set; }
      public bool HasPageVisibleHandlers { get; private set; }
      public HtmlForm MainForm { get; private set; }
      public MenuBar MenuBar { get; private set; }
      public ScriptManager ScriptManager { get; private set; }
      public event EventHandler Page_Visible;

      #endregion
   }
}