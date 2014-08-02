using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.Core;
using FLEX.Web.MasterPages;
using FLEX.Web.UserControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PageBase : Page, IPageBase
   {
      #region Constants

      private const string FromIdRequestKey = "from_id";
      private const string OldIdRequestKey = "old_id";

      private const string IdViewStateKey = "__PAGE_ID__";
      private const int MinIdValue = 1000000;
      private const int MaxIdValue = 9999999;

      private static readonly Random Random = new Random();

      #endregion

      #region Fields

      private readonly string _id;

      #endregion

      /// <summary>
      /// 
      /// </summary>
      protected PageBase()
      {
         Load += Page_Load;
      }

      private void Page_Load(object sender, EventArgs e)
      {
         InitID();

         // If user is not authenticated, then we redirect her to the session expired page.
         if (Configuration.Instance.CheckSecurity && !HttpContext.Current.User.Identity.IsAuthenticated)
         {
            Response.Redirect(Configuration.Instance.SessionExpiredPageUrl, endResponse: true);
         }

         // Browser cache management
         Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
         Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
         Response.AppendHeader("Expires", "0"); // Proxies.
      }

      protected override sealed PageStatePersister PageStatePersister
      {
         get { return new CacheViewStatePersister(this); }
      }

      #region Public Properties

      /// <summary>
      /// 
      /// </summary>
      public string FromID
      {
         get { return Request[FromIdRequestKey]; }
      }

      /// <summary>
      /// 
      /// </summary>
      public string FlexID
      {
         get { return (string) ViewState[IdViewStateKey]; }
      }

      /// <summary>
      /// 
      /// </summary>
      public string OldID
      {
         get { return Request[OldIdRequestKey]; }
      }

      #endregion

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

      #region Private Methods

      private void InitID()
      {        
         var storedId = ViewState[IdViewStateKey];
         if (storedId != null)
         {
            // ID is already stored, so we can return.
            return;
         }

         // Locally cached for performance reasons.
         var oldId = OldID;
         if (oldId != null)
         {
            ViewState[IdViewStateKey] = oldId;
            return;
         }

         // We generate a new ID, we store it in the view state and then we return.
         var newId = Random.Next(MinIdValue, MaxIdValue).ToString(CultureInfo.InvariantCulture);
         ViewState[IdViewStateKey] = newId;
      }

      #endregion
   }
}