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

      public UserControls.Ajax.ErrorHandler ErrorHandler
      {
         get { return (Master as IPageBase).ErrorHandler; }
      }

      public bool HasPageVisibleHandlers
      {
         get { return (Master as IPageBase).HasPageVisibleHandlers; }
      }

      public HtmlForm MainForm
      {
         get { return (Master as IPageBase).MainForm; }
      }

      public MenuBar MenuBar
      {
         get { return (Master as IPageBase).MenuBar; }
      }

      public PageFooter PageFooter
      {
         get { return (Master as IPageBase).PageFooter; }
      }

      public ScriptManager ScriptManager
      {
         get { return (Master as IPageBase).ScriptManager; }
      }

      public event EventHandler Page_Visible;

      #endregion
   }
}