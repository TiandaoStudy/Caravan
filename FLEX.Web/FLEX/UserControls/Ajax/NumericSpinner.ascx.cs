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
   /// 
   /// </summary>
   public partial class NumericSpinner : AjaxControlBase, IAjaxControl, ISearchControl
   {
      private const string DecimalCountViewStateKey = "NumericSpinner.DecimalCount";
      private const string MaxValueViewStateKey = "NumericSpinner.MaxValue";
      private const string MinValueViewStateKey = "NumericSpinner.MinValue";
      private const string StepViewStateKey = "NumericSpinner.Step";

      private const int DecimalCountDefaultValue = 1;
      private const decimal MaxValueDefaultValue = 100M;
      private const decimal MinValueDefaultValue = 0M;
      private const decimal StepDefaultValue = 0.5M;

      #region Public Properties

      public int? DecimalCount
      {
         get { return (int?) ViewState[DecimalCountViewStateKey]; }
         set { ViewState[DecimalCountViewStateKey] = value ?? DecimalCountDefaultValue; }
      }

      public decimal? MaxValue
      {
         get { return (decimal?) ViewState[MaxValueViewStateKey]; }
         set { ViewState[MaxValueViewStateKey] = value ?? MaxValueDefaultValue; }
      }

      public decimal? MinValue
      {
         get { return (decimal?) ViewState[MinValueViewStateKey]; }
         set { ViewState[MinValueViewStateKey] = value ?? MinValueDefaultValue; }
      }

      public decimal? Step
      {
         get { return (decimal?) ViewState[StepViewStateKey]; }
         set { ViewState[StepViewStateKey] = value ?? StepDefaultValue; }
      }

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(btnApply);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new PostBackTrigger
         {
            ControlID = btnApply.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = btnApply.UniqueID,
            EventName = "Click"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl Members

      public bool HasValues
      {
         get { return !String.IsNullOrEmpty(txtNumber.Text); }
      }

      public IList<string> SelectedValues // Valore attualmente selezionato
      {
         get
         {
            if (!HasValues)
            {
               return CommonSettings.EmptyStringList;
            }
            return new OneItemList<string>(txtNumber.Text);
         }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         txtNumber.Text = String.Empty;
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<NumericSpinner>(searchControl);

         var otherNumericSpinner = (NumericSpinner) searchControl;
         txtNumber.Text = otherNumericSpinner.txtNumber.Text;
      }

      #endregion

      #region AjaxControlBase Members

      protected override void SetDefaultValues()
      {
         base.SetDefaultValues();
         DecimalCount = DecimalCount ?? DecimalCountDefaultValue;
         MaxValue = MaxValue ?? MaxValueDefaultValue;
         MinValue = MinValue ?? MinValueDefaultValue;
         Step = Step ?? StepDefaultValue;
      }

      protected override void OnEnabledChanged(bool enabled)
      {
         base.OnEnabledChanged(enabled);
         txtNumber.Enabled = btnApply.Enabled = enabled;
         txtNumber.ReadOnly = !enabled;         
      }

      #endregion

      #region Private Members

      protected void btnApply_Click(object sender, EventArgs e)
      {
         Basics.TriggerEvent(ValueSelected, this, new SearchCriteriaSelectedArgs());
      }

      #endregion
   }
}