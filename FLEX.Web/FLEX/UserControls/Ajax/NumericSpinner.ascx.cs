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
      /// <summary>
      /// 
      /// </summary>
      protected NumericSpinner()
      {
         // Default values
         DecimalCount = 1;
         MaxValue = 100M;
         MinValue = 0M;
         Step = 0.5M;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected void Page_Load(object sender, EventArgs e)
      {
         btnApply.Click += btnApply_Click;
      }

      #region Public Properties

      /// <summary>
      /// 
      /// </summary>
      public int DecimalCount { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public decimal MaxValue { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public decimal MinValue { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public decimal Step { get; set; }

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

      #region Private Members

      private void btnApply_Click(object sender, EventArgs e)
      {
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }

      #endregion
   }
}