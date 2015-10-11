using System;
using System.Web.UI;
using Finsa.CodeServices.Common;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class ImageButton : AjaxControlBase, IAjaxControl
   {
      #region Public Properties

      public string ButtonClass
      {
         get { return btn.CssClass; }
         set { btn.CssClass = string.IsNullOrWhiteSpace(value) ? string.Empty : value; }
      }

      public string ButtonText
      {
         get { return btnText.InnerText; } 
         set { btnText.InnerText = string.IsNullOrWhiteSpace(value) ? string.Empty : " " + value;}
      }

      public bool CausesValidation
      {
         get { return btn.CausesValidation; }
         set { btn.CausesValidation = value; }
      }

      public string IconClass
      {
         get { return btnIcon.Attributes["class"]; }
         set { btnIcon.Attributes["class"] = string.IsNullOrWhiteSpace(value) ? string.Empty : value; }
      }

      public string OnClientClick
      {
         get { return btn.OnClientClick; }
         set { btn.OnClientClick = string.IsNullOrWhiteSpace(value) ? string.Empty : value; }
      }

      public string ValidationGroup
      {
         get { return btn.ValidationGroup; }
         set { btn.ValidationGroup = value; }
      }

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(btn);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new PostBackTrigger
         {
            ControlID = btn.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = btn.UniqueID,
            EventName = "Click"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region AjaxControlBase Members

      protected override void OnEnabledChanged(bool enabled)
      {
         base.OnEnabledChanged(enabled);
         btn.Enabled = enabled;
      }

      #endregion

      public event EventHandler Click;

      #region Private Members

      protected void btn_Click(object sender, EventArgs args)
      {
         Click.SafeInvoke(this, args);
      }

      #endregion
   }
}