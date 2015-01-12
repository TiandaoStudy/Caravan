using System.Diagnostics;
using System.Web.UI;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PopupBase : HeadBase, IPopup
   {
      #region IPopup Members

      public void RegisterAlert(Page child, string message)
      {
         MasterPopup.RegisterAlert(child, message);
      }

      public void RegisterCloseScript(Page child)
      {
         MasterPopup.RegisterCloseScript(child);
      }

      #endregion

      #region Protected Members

      protected IPopup MasterPopup
      {
         get
         {
            Debug.Assert(Master is IPopup);
            return Master as IPopup;
         }
      }

      #endregion
   }
}
