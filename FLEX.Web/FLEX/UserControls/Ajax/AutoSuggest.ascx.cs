using System;
using System.Collections.Generic;
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
   /// <summary>
   /// 
   /// </summary>
   [ValidationProperty("Key")]
   public partial class AutoSuggest : AjaxControlBase, IAjaxControl, ISearchControl
   {
      /// <summary>
      /// 
      /// </summary>
      protected AutoSuggest()
      {
         // Default values
         QueryFilter = "''";
         MaxMenuHeight = "250";
         MinLengthForHint = 2;
      }

      protected override void Page_Load(object sender, EventArgs e)
      {
         base.Page_Load(sender, e);

         Raise<ArgumentException>.IfIsEmpty(XmlLookup);
         Raise<ArgumentException>.IfIsEmpty(LookupBy);

         txtKey.TextChanged += txtKey_TextChanged;
         txtKey.Enabled = Enabled;
         txtKey.ReadOnly = !Enabled;

         txtSuggestion.Enabled = Enabled;
         txtSuggestion.ReadOnly = !Enabled;
         txtSuggestion.Attributes.Add("placeholder", PlaceHolder);
      }

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

      public string XmlLookup { get; set; }

      public string LookupBy { get; set; }

      public string QueryFilter { get; set; }

      /// <summary>
      ///   Minimum number of characters required for the auto suggest to trigger itself.
      /// </summary>
      public int MinLengthForHint { get; set; }

      // Imposta un'altezza alla tendinda dei suggerimenti (def: 250px)
      public string MaxMenuHeight { get; set; }

      public string PlaceHolder { get; set; }

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
            ControlID = txtKey.UniqueID,
            EventName = "TextChanged"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl Members

      public bool HasValues
      {
         get { return !String.IsNullOrEmpty(txtKey.Text); }
      }

      public IList<string> SelectedValues // Valore attualmente selezionato
      {
         get
         {
            if (!HasValues)
            {
               return CommonSettings.EmptyStringList;
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

      #region Private Members

      private void txtKey_TextChanged(object sender, EventArgs e)
      {
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }

      #endregion
   }
}