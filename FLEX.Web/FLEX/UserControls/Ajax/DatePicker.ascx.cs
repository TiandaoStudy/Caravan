using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
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
   /// 
   /// </summary>
   [ValidationProperty("SelectedDate")]
   public partial class DatePicker : AjaxControlBase, IAjaxControl, ISearchControl
   {
      private const string DateFormatViewStateKey = "DatePicker.DateFormat";
      private const string EndDateViewStateKey = "DatePicker.EndDate";
      private const string MinViewViewStateKey = "DatePicker.MinView";
      private const string StartDateViewStateKey = "DatePicker.StartDate";
      private const string StartViewViewStateKey = "DatePicker.StartView";

      private const string DateFormatDefaultValue = "dd/mm/yyyy";
      private const string EndDateDefaultValue = "31/12/2999";
      private const MinViewMode MinViewDefaultValue = MinViewMode.Days;
      private const string StartDateDefaultValue = "01/01/2000";
      private const StartViewMode StartViewDefaultValue = StartViewMode.Month;

      #region Public Properties

      public string DateFormat
      {
         get { return (string) ViewState[DateFormatViewStateKey]; }
         set { ViewState[DateFormatViewStateKey] = value ?? DateFormatDefaultValue; }
      }

      public TextBox DateField
      {
         get { return txtDate; }
      }

      public DateTime SelectedDate
      {
         get
         {
            Raise<InvalidOperationException>.IfNot(HasValues);
            Raise<InvalidOperationException>.IfNot(DateIsValid(txtDate.Text));
            return DateTime.Parse(txtDate.Text, WebSettings.CurrentUserCulture, DateTimeStyles.AllowWhiteSpaces);
         }
         set { txtDate.Text = value.ToString("d", WebSettings.CurrentUserCulture); }
      }

      public string StartDate
      {
         get { return (string) ViewState[StartDateViewStateKey]; }
         set
         {
            Raise<ArgumentException>.IfNot(DateIsValid(value));
            ViewState[StartDateViewStateKey] = value ?? StartDateDefaultValue;
         }
      }

      public string EndDate
      {
         get { return (string) ViewState[EndDateViewStateKey]; }
         set
         {
            Raise<ArgumentException>.IfNot(DateIsValid(value));
            ViewState[EndDateViewStateKey] = value ?? EndDateDefaultValue;
         }
      }

      public StartViewMode? StartView
      {
         get { return (StartViewMode?) ViewState[StartViewViewStateKey]; }
         set { ViewState[StartViewViewStateKey] = value ?? StartViewDefaultValue; }
      }

      public MinViewMode? MinView
      {
         get { return (MinViewMode?) ViewState[MinViewViewStateKey]; }
         set { ViewState[MinViewViewStateKey] = value ?? MinViewDefaultValue; }
      }

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return datepickerPanel; }
      }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(txtDate);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new PostBackTrigger
         {
            ControlID = txtDate.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = txtDate.UniqueID,
            EventName = "TextChanged"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl Members

      public dynamic DynamicSelectedValues
      {
         get
         {
            DateTime result;
            if (HasValues && DateTime.TryParse(txtDate.Text, WebSettings.CurrentUserCulture, DateTimeStyles.AllowWhiteSpaces, out result))
            {
               return result;
            }
            return null;
         }
      }

      public bool HasValues
      {
         get { return !String.IsNullOrEmpty(txtDate.Text); }
      }

      public IList<string> SelectedValues // Valore attualmente selezionato
      {
         get
         {
            if (!HasValues)
            {
               return CommonSettings.EmptyStringList;
            }
            return new OneItemList<string>(txtDate.Text);
         }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         txtDate.Text = String.Empty;
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<DatePicker>(searchControl);

         var otherDatePicker = (DatePicker) searchControl;
         txtDate.Text = otherDatePicker.txtDate.Text;
      }

      #endregion

      #region AjaxControlBase Members

      protected override void SetDefaultValues()
      {
         base.SetDefaultValues();
         DateFormat = DateFormat ?? DateFormatDefaultValue;
         EndDate = EndDate ?? EndDateDefaultValue;
         MinView = MinView ?? MinViewDefaultValue;
         StartDate = StartDate ?? StartDateDefaultValue;
         StartView = StartView ?? StartViewDefaultValue;
      }

      protected override void OnDoPostBackChanged(bool doPostBack)
      {
         base.OnDoPostBackChanged(doPostBack);
         txtDate.AutoPostBack = doPostBack;
      }

      protected override void OnEnabledChanged(bool enabled)
      {
         base.OnEnabledChanged(enabled);
         txtDate.Enabled = enabled;
         txtDate.ReadOnly = !enabled;
      }

      #endregion

      #region Private Members

      protected void txtDate_TextChanged(object sender, EventArgs e)
      {
         Basics.TriggerEvent(ValueSelected, this, new SearchCriteriaSelectedArgs());
      }

      private static bool DateIsValid(string dateStr)
      {
         DateTime dt;
         return DateTime.TryParse(dateStr, WebSettings.CurrentUserCulture, DateTimeStyles.AllowWhiteSpaces, out dt);
      }

      #endregion

      /// <summary>
      /// 
      /// </summary>
      public enum StartViewMode : byte
      {
         Month = 0,
         Year = 1,
         Decade = 2
      }

      /// <summary>
      /// 
      /// </summary>
      public enum MinViewMode : byte
      {
         Days = 0,
         Months = 1,
         Years = 2
      }
   }
}