using System;
using System.Collections.Generic;
using System.Web.UI;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using Thrower;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class OnOffSwitch : AjaxControlBase, IAjaxControl, ISearchControl
   {
      protected OnOffSwitch()
      {
         // Default values
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            SetButtonsStyle();
         }
      }

      #region Public Properties

      public static string OnValue
      {
         get { return "1"; }
      }

      public static string OffValue
      {
         get { return "0"; }
      }

      public bool Switched
      {
         get { return chkSwitch.Checked; }
         set { chkSwitch.Checked = value; }
      }

      #endregion

      #region IAjaxControl Members

      public override bool DoPostBack
      {
         get { return base.DoPostBack; }
         set { base.DoPostBack = btnON.UseSubmitBehavior = btnOFF.UseSubmitBehavior = value; }
      }

      public override bool Enabled
      {
         get { return base.Enabled; }
         set { base.Enabled = btnON.Enabled = btnOFF.Enabled = value; }
      }

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void AttachToUpdatePanel(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = btnON.UniqueID,
            EventName = "Click"
         };
         updatePanel.Triggers.Add(trigger);
         trigger = new AsyncPostBackTrigger
         {
            ControlID = btnOFF.UniqueID,
            EventName = "Click"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl Members

      public bool HasValues
      {
         get { return true; }
      }

      public IList<string> SelectedValues // Valore attualmente selezionato
      {
         get { return new OneItemList<string>(chkSwitch.Checked ? OnValue : OffValue); }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         Switched = false;
         SetButtonsStyle();
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<OnOffSwitch>(searchControl);

         var otherSwitch = (OnOffSwitch) searchControl;
         chkSwitch.Checked = otherSwitch.chkSwitch.Checked;
      }

      #endregion

      protected void btnON_Click(object sender, EventArgs e)
      {
         Switched = true;
         SetButtonsStyle();
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }

      protected void btnOFF_Click(object sender, EventArgs e)
      {
         Switched = false;
         SetButtonsStyle();
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }

      private void SetButtonsStyle()
      {
         var inactiveCss = "btn btn-sm btn-on-off btn-default";
         var activeCss = "btn btn-sm btn-on-off btn-primary";

         btnON.CssClass = chkSwitch.Checked ? activeCss : inactiveCss;
         btnOFF.CssClass = chkSwitch.Checked ? inactiveCss : activeCss;
      }
   }
}