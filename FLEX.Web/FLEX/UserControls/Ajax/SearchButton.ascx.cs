using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Common;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class SearchButton : AjaxControlBase, IAjaxControl
   {
      protected override void Page_Load(object sender, EventArgs e)
      {
         base.Page_Load(sender, e);
         btnSearch.Visible = Visible;
      }

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public bool Visible { get; set; }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(btnSearch);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new PostBackTrigger
         {
            ControlID = btnSearch.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = btnSearch.UniqueID,
            EventName = "Click"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      public HtmlButton AspButton
      {
         get { return btnSearch; }
      }

      public event EventHandler Click;

      #region Private Members

      protected void btnSearch_Click(object sender, EventArgs args)
      {
         Basics.TriggerEvent(Click, this, args);
      }

      #endregion
   }
}