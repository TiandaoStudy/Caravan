using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class HeadBase : Page, IHead
   {
      #region Private Members

      private IHead MasterHead
      {
         get
         {
            Debug.Assert(Master is IHead);
            return Master as IHead;
         }
      }

      #endregion

      public UserControls.Ajax.ErrorHandler ErrorHandler { get; private set; }
      public HtmlForm MainForm { get; private set; }
      public ScriptManager ScriptManager { get; private set; }
   }
}
