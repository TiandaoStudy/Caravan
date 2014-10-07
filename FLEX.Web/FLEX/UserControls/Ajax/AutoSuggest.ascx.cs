using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using FLEX.Common;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using FLEX.Web.WebForms;
using FLEX.WebForms;
using PommaLabs.GRAMPA.Diagnostics;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   /// <summary>
   /// 
   /// </summary>
   [ValidationProperty("Key")]
   public partial class AutoSuggest : AjaxControlBase, IAjaxControl, ISearchControl
   {
      private const string XmlLookupViewStateKey = "AutoSuggest.XmlLookup";
      private const string LookupByViewStateKey = "AutoSuggest.LookupBy";
      private const string QueryFilterViewStateKey = "AutoSuggest.QueryFilter";
      private const string MinLengthForHintViewStateKey = "AutoSuggest.MinLengthForHint";
      private const string MaxMenuHeightViewStateKey = "AutoSuggest.MaxMenuHeight";

      private const string MaxMenuHeightDefaultValue = "250";
      internal const int MinLengthForHintDefaultValue = 2;
      private const string PlaceHolderDefaultValue = "...";
      private const string QueryFilterDefaultValue = "''";

      #region Public Properties

      public string Key
      {
         get { return txtKey.Text; }
         set { txtKey.Text = value; }
      }

      public TextBox KeyField
      {
         get { return txtKey; }
      }

      public string Suggestion
      {
         get { return txtSuggestion.Text; }
         set { txtSuggestion.Text = value; }
      }

      public TextBox SuggestionField
      {
         get { return txtSuggestion; }
      }

      public string XmlLookup
      {
         get { return (string) ViewState[XmlLookupViewStateKey]; }
         set
         {
            Raise<ArgumentException>.IfIsEmpty(value, ErrorMessages.UserControls_AutoSuggest_NullOrEmptyXmlLookup);
            ViewState[XmlLookupViewStateKey] = value;
         }
      }

      public string LookupBy
      {
         get { return (string) ViewState[LookupByViewStateKey]; }
         set
         {
            Raise<ArgumentException>.IfIsEmpty(value, ErrorMessages.UserControls_AutoSuggest_NullOrEmptyLookupBy);
            ViewState[LookupByViewStateKey] = value;
         }
      }

      public string QueryFilter
      {
         get { return (string) ViewState[QueryFilterViewStateKey]; }
         set { ViewState[QueryFilterViewStateKey] = value ?? QueryFilterDefaultValue; }
      }

      /// <summary>
      ///   Minimum number of characters required for the auto suggest to trigger itself.
      /// </summary>
      public int? MinLengthForHint
      {
         get { return (int?) ViewState[MinLengthForHintViewStateKey]; }
         set { ViewState[MinLengthForHintViewStateKey] = value ?? MinLengthForHintDefaultValue; }
      }

      // Imposta un'altezza alla tendinda dei suggerimenti (def: 250px)
      public string MaxMenuHeight
      {
         get { return (string) ViewState[MaxMenuHeightViewStateKey]; }
         set { ViewState[MaxMenuHeightViewStateKey] = value ?? MaxMenuHeightDefaultValue; }
      }

      public string PlaceHolder
      {
         get { return txtSuggestion.Attributes["placeholder"]; }
         set { txtSuggestion.Attributes["placeholder"] = value ?? PlaceHolderDefaultValue; }
      }

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(txtKey);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new PostBackTrigger
         {
            ControlID = txtKey.UniqueID,
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = txtKey.UniqueID,
            EventName = "TextChanged"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl Members

      public dynamic DynamicSelectedValues
      {
         get { return HasValues ? txtKey.Text : null; }
      }

      public bool HasValues
      {
         get { return !String.IsNullOrEmpty(txtKey.Text); }
      }

      public IList<string> SelectedValues
      {
         get
         {
            if (!HasValues)
            {
               return Common.Configuration.EmptyStringList;
            }
            return new OneItemList<string>(txtKey.Text);
         }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         txtSuggestion.Text = String.Empty;
         txtKey.Text = String.Empty;
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<AutoSuggest>(searchControl);

         var otherAutoSuggest = (AutoSuggest) searchControl;
         txtKey.Text = otherAutoSuggest.txtKey.Text;
         txtSuggestion.Text = otherAutoSuggest.txtSuggestion.Text;
      }

      #endregion

      #region AjaxControlBase Members

      protected override void SetDefaultValues()
      {
         base.SetDefaultValues();
         MaxMenuHeight = MaxMenuHeight ?? MaxMenuHeightDefaultValue;
         MinLengthForHint = MinLengthForHint ?? MinLengthForHintDefaultValue;
         PlaceHolder = PlaceHolder ?? PlaceHolderDefaultValue;
         QueryFilter = QueryFilter ?? QueryFilterDefaultValue;
      }

      protected override void OnDoPostBackChanged(bool doPostBack)
      {
         base.OnDoPostBackChanged(doPostBack);
         txtKey.AutoPostBack = doPostBack;
      }

      protected override void OnEnabledChanged(bool enabled)
      {
         base.OnEnabledChanged(enabled);
         txtKey.Enabled = enabled;
         txtKey.ReadOnly = !enabled;
         txtSuggestion.Enabled = enabled;
         txtSuggestion.ReadOnly = !enabled;
      }

      #endregion

      #region Private Members

      protected void txtKey_OnTextChanged(object sender, EventArgs e)
      {
         Basics.TriggerEvent(ValueSelected, this, new SearchCriteriaSelectedArgs());
      }

      #endregion
   }
}