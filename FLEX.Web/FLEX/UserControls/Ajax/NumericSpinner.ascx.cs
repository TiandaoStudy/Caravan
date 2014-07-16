using System;
using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class NumericSpinner : UserControl
   {
      protected NumericSpinner()
      {
         // Default values
         MaxValue = 100M;
         MinValue = 0M;
         Step = 0.5M;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
      }

      #region Public Properties

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
   }
}