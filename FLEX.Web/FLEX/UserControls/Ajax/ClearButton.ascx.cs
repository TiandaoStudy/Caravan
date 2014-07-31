using System;
using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class ClearButton : AjaxControlBase, IAjaxControl
   {
      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void AttachToUpdatePanel(UpdatePanel updatePanel) // Regestra il trigger all'updatepanel e aggiunge un eventhandler per l'evente
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = btnClear.UniqueID,
            EventName = "Click"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      public event EventHandler Clicked;

      #region Private Members

      protected void btnClear_Click(object sender, EventArgs args)
      {
         Clicked(this, args);
      }

      #endregion
   }
}