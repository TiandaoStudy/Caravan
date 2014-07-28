using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using FLEX.Common;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using Thrower;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class DatePicker : AjaxControlBase, IAjaxControl, ISearchControl
   {
      private string _startDate;
      private string _endDate;

      protected DatePicker()
      {
         // Default values
         DateFormat = "dd/mm/yyyy";
         _startDate = "01/01/2000";
         _endDate = "31/12/2999";
         StartView = StartViewMode.Month;
         MinView = MinViewMode.Days;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         Raise<ArgumentException>.IfIsEmpty(StartDate);
         Raise<ArgumentException>.IfIsEmpty(EndDate);
      }

      #region Public Properties

      public string DateFormat { get; set; }

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
         set
         {
            txtDate.Text = value.ToString("d", WebSettings.CurrentUserCulture);
         }
      }

      public string StartDate
      {
         get { return _startDate; }
         set
         {
            Raise<ArgumentException>.IfNot(DateIsValid(value));
            _startDate = value;
         }
      }

      public string EndDate
      {
         get { return _endDate; }
         set
         {
            Raise<ArgumentException>.IfNot(DateIsValid(value));
            _endDate = value;
         }
      }

      public StartViewMode StartView { get; set; }

      public MinViewMode MinView { get; set; }

      public enum StartViewMode : byte
      {
         Month = 0,
         Year = 1,
         Decade = 2
      }

      public enum MinViewMode : byte
      {
         Days = 0,
         Months = 1,
         Years = 2
      }

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return datepickerPanel; }
      }

      public void AttachToUpdatePanel(UpdatePanel updatePanel)
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
   }
}