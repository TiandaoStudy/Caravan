using System;
using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class ImageButton : AjaxControlBase, IAjaxControl
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         btn.CssClass = String.IsNullOrEmpty(ButtonClass) ? String.Empty : ButtonClass;
         btnIcon.Attributes["class"] = String.IsNullOrEmpty(IconClass) ? String.Empty : IconClass;
         btnText.InnerText = String.IsNullOrEmpty(ButtonText) ? String.Empty : " " + ButtonText;
      }

      #region Public Properties

      public string ButtonClass { get; set; }

      public string ButtonText { get; set; }

      public string IconClass { get; set; }

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void AttachToUpdatePanel(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = btn.UniqueID,
            EventName = "Click"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      public event EventHandler Click;

      #region Private Members

      protected void btn_Click(object sender, EventArgs args)
      {
         Click(this, args);
      }

      #endregion
   }
}