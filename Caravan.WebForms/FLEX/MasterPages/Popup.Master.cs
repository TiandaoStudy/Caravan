using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class Popup : MasterPage, IPopup
   {
      protected override void OnLoad(EventArgs e)
      {
         base.OnLoad(e);
         Master.RedirectIfNotAuthenticated();
      }

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

      #endregion

      public void RegisterAlert(Page child, string message)
      {
         var script = String.Format("bootbox.alert('PROVA');", message);
         ScriptManager.RegisterStartupScript(child, child.GetType(), "_Alert_", script, true);
      }

      public void RegisterCloseScript(Page child)
      {
         txtDoClose.Text = "CLOSE";
         var closeCallback = String.Format("document.getElementById('{0}').value = 'CLOSE'; checkClose();", txtDoClose.ClientID);
         RegisterStartupScript(Page, closeCallback, "_ReturnDataAndExit_");
      }

      private static void RegisterStartupScript(Page child, string script, string scriptName)
      {
         script = String.Format("<script type=\"text/javascript\">{0}</script>", script);
         child.ClientScript.RegisterStartupScript(child.GetType(), scriptName, script);
      }
   }
}