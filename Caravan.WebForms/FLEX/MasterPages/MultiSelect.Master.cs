using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class MultiSelect : MasterPage, IPopup
   {
      #region Public Properties

      public HtmlGenericControl LeftPanelTitle
      {
         get { return leftPanelTitle; }
      }

      public HtmlGenericControl RightPanelTitle
      {
         get { return rightPanelTitle; }
      }

      public LinkButton MoveRight
      {
         get { return btnMoveRight; }
      }

      public LinkButton MoveAllRight
      {
         get { return btnMoveAllRight; }
      }

      public LinkButton MoveLeft
      {
         get { return btnMoveLeft; }
      }

      public LinkButton MoveAllLeft
      {
         get { return btnMoveAllLeft; }
      }

      #endregion

      #region IPopup Members

      public ErrorHandler ErrorHandler
      {
         get { return Master.ErrorHandler; }
      }

      public HtmlForm MainForm
      {
         get { return Master.MainForm; }
      }

      public ScriptManager ScriptManager
      {
         get { return Master.ScriptManager; }
      }

      public void RegisterAlert(Page child, string message)
      {
         Master.RegisterAlert(child, message);
      }

      public void RegisterCloseScript(Page child)
      {
         Master.RegisterCloseScript(child);
      }

      #endregion
   }
}