using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PopupBase : Page, IPopupBase
   {
      #region IPopupBase Members

      public UserControls.Ajax.ErrorHandler ErrorHandler
      {
         get { return MasterPopup.ErrorHandler; }
      }

      public HtmlForm MainForm
      {
         get { return MasterPopup.MainForm; }
      }
      
      public void RegisterCloseScript(Page child)
      {
         MasterPopup.RegisterCloseScript(this);
      }

      #endregion

      #region Private Members

      private IPopupBase MasterPopup
      {
         get
         {
            Debug.Assert(Master is IPopupBase);
            return Master as IPopupBase;
         }
      }

      #endregion
   }
}
