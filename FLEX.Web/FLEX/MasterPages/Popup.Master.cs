using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class Popup : MasterPage, IPopupBase
   {
      #region IPopupBase Members

      public ErrorHandler ErrorHandler
      {
         get { return Master.ErrorHandler; }
      }

      public HtmlForm MainForm
      {
         get { return Master.MainForm; }
      }

      #endregion

      public void RegisterAlert(Page child, string message)
      {
         // TODO
      }

      public void RegisterCloseScript(Page child)
      {
         txtDoClose.Text = "CLOSE";
         var closeCallback = String.Format("document.getElementById('{0}').value = 'CLOSE'; checkClose();", txtDoClose.ClientID);
         RegisterStartupScript(Page, closeCallback, "_ReturnDataAndExit_");
      }

      public void RegisterMessage(Page child, string message)
      {
         // TODO
      }

      private static void RegisterStartupScript(Page child, string script, string scriptName)
      {
         script = String.Format("<script type=\"text/javascript\">{0}</script>", script);
         child.ClientScript.RegisterStartupScript(child.GetType(), scriptName, script);
      }
   }
}