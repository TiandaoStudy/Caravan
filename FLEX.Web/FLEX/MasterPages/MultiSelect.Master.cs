using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class MultiSelect : MasterPage, IPopupBase
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

      #region IPopupBase Members

      public ErrorHandler ErrorHandler
      {
         get { return Master.ErrorHandler; }
      }

      public HtmlForm MainForm
      {
         get { return Master.MainForm; }
      }

      public void RegisterCloseScript(Page child)
      {
         Master.RegisterCloseScript(child);
      }

      #endregion
   }
}