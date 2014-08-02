using System;
using System.Collections.Generic;
using System.Web.UI;
using FLEX.Common;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using Thrower;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   TODO
   /// </summary>
   public partial class OnOffSwitch : AjaxControlBase, IAjaxControl, ISearchControl
   {
      protected override void Page_Load(object sender, EventArgs e)
      {
         base.Page_Load(sender, e);

         OnSwitchedChanged(Switched);
         var switchFunction = String.Format("switchOnOff_{0}(event);", ClientID);
         btnON.OnClientClick = btnOFF.OnClientClick = switchFunction;
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

      /// <summary>
      ///   TODO
      /// </summary>
      public bool Switched
      {
         get
         {
            if (String.IsNullOrWhiteSpace(txtSwitched.Text))
            {
               txtSwitched.Text = OnValue;
               return true;
            }
            return txtSwitched.Text == OnValue;
         }
         set
         {
            txtSwitched.Text = value ? OnValue : OffValue;
            OnSwitchedChanged(value);
         }
      }

      public string ActiveClass
      {
         get { return "btn btn-sm btn-on-off btn-primary switch-active"; }
      }

      public string InactiveClass
      {
         get { return "btn btn-sm btn-on-off btn-default switch-inactive"; }
      }

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
         get { return new OneItemList<string>(Switched ? OnValue : OffValue); }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         Switched = false;
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<OnOffSwitch>(searchControl);

         var otherSwitch = (OnOffSwitch) searchControl;
         otherSwitch.Switched = otherSwitch.Switched;
      }

      #endregion

      #region AjaxControlBase Members

      protected override void OnDoPostBackChanged(bool doPostBack)
      {
         base.OnDoPostBackChanged(doPostBack);
         txtSwitched.AutoPostBack = doPostBack;
      }

      protected override void OnEnabledChanged(bool enabled)
      {
         base.OnEnabledChanged(enabled);
         btnON.Enabled = btnOFF.Enabled = enabled;
      }

      #endregion

      #region Private Methods

      protected void txtSwitched_OnTextChanged(object sender, EventArgs e)
      {
         OnSwitchedChanged(Switched);
         Basics.TriggerEvent(ValueSelected, this, new SearchCriteriaSelectedArgs());
      }

      private void OnSwitchedChanged(bool switched)
      {
         btnON.CssClass = switched ? ActiveClass : InactiveClass;
         btnOFF.CssClass = switched ? InactiveClass : ActiveClass;
      }

      #endregion
   }
}