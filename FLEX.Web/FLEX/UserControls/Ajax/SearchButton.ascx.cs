using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class SearchButton : AjaxControlBase, IAjaxControl
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         btnSearch.Visible = Visible;
      }

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public bool Visible { get; set; }

      public void AttachToUpdatePanel(UpdatePanel updatePanel)
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

      public event EventHandler Clicked;

      #region Private Members

      protected void btnSearch_Click(object sender, EventArgs args)
      {
         Clicked(this, args);
      }

      #endregion
   }
}