using System;
using System.Collections.Generic;
using System.Web.UI;
using FLEX.Common;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using PommaLabs.GRAMPA.Diagnostics;

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
      protected const string ActiveOnClass = "btn btn-sm btn-on btn-primary switch-active";
      protected const string ActiveOffClass = "btn btn-sm btn-off btn-primary switch-active";
      protected const string InactiveOnClass = "btn btn-sm btn-on btn-default switch-inactive";
      protected const string InactiveOffClass = "btn btn-sm btn-off btn-default switch-inactive";

      protected override void Page_Load(object sender, EventArgs e)
      {
         base.Page_Load(sender, e);

         OnSwitchedChanged(Switched);
         var switchFunction = String.Format("switchOnOff_{0}(event);", ClientID);
         btnON.OnClientClick = btnOFF.OnClientClick = switchFunction;
      }

      #region Public Properties
      
      /// <summary>
      ///   TODO
      /// </summary>
      public static string OnValue
      {
         get { return "1"; }
      }
      
      /// <summary>
      ///   TODO
      /// </summary>
      public static string OffValue
      {
         get { return "0"; }
      }
      
      /// <summary>
      ///   TODO
      /// </summary>
      public string OnLabel
      {
         get { return btnON.Text; }
         set
         {
            Raise<ArgumentException>.IfIsEmpty(value);
            btnON.Text = value;
         }
      }
      
      /// <summary>
      ///   TODO
      /// </summary>
      public string OffLabel
      {
         get { return btnOFF.Text; }
         set
         {
            Raise<ArgumentException>.IfIsEmpty(value);
            btnOFF.Text = value;
         }
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

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(btnON);
         Master.ScriptManager.RegisterPostBackControl(btnOFF);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new PostBackTrigger
         {
            ControlID = btnON.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
         trigger = new PostBackTrigger
         {
            ControlID = btnOFF.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
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

      public dynamic DynamicSelectedValues
      {
         get { return Switched; }
      }

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
         btnON.CssClass = switched ? ActiveOnClass : InactiveOnClass;
         btnOFF.CssClass = switched ? InactiveOffClass : ActiveOffClass;
      }

      #endregion
   }
}