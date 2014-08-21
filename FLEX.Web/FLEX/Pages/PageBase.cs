using System;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.MasterPages;
using FLEX.Web.UserControls;
using PommaLabs.KVLite.Web;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PageBase : HeadBase, IPage
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

      protected override void OnLoad(EventArgs e)
        {
 	        base.OnLoad(e);
            InitID();
        }

      protected override sealed PageStatePersister PageStatePersister
      {
         get { return new PersistentViewStatePersister(this); }
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

      #region IPage Members

      public UserControls.Ajax.ErrorHandler ErrorHandler
      {
         get { return MasterPage.ErrorHandler; }
      }

      public bool HasPageVisibleHandlers
      {
         get { return MasterPage.HasPageVisibleHandlers; }
      }

      public HtmlForm MainForm
      {
         get { return MasterPage.MainForm; }
      }

      public MenuBar MenuBar
      {
         get { return MasterPage.MenuBar; }
      }

      public PageFooter PageFooter
      {
         get { return MasterPage.PageFooter; }
      }

      public ScriptManager ScriptManager
      {
         get { return MasterPage.ScriptManager; }
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

      protected IPage MasterPage
      {
         get
         {
            Debug.Assert(Master is IPage);
            return Master as IPage;
         }
      }

      #endregion
   }
}