using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   TODO
   /// </summary>
   public abstract class HeadBase : Page, IHead
   {
      #region IHead Members

      public UserControls.Ajax.ErrorHandler ErrorHandler
      {
         get { return MasterHead.ErrorHandler; }
      }

      public HtmlForm MainForm
      {
         get { return MasterHead.MainForm; }
      }

      public ScriptManager ScriptManager
      {
         get { return MasterHead.ScriptManager; }
      }

      #endregion

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
   }
}
