using System;
using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class HiddenTrigger : UserControl
   {
      protected void Page_Init(object sender, EventArgs e)
      {
         txtHidden.TextChanged += txtHidden_TextChanged;
      }

      #region Public Members

      public Control Trigger
      {
         get { return txtHidden; }
      }

      public event EventHandler Triggered;

      #endregion

      #region Private Members

      private void txtHidden_TextChanged(object sender, EventArgs e)
      {
         if (Triggered != null)
         {
            Triggered(this, new EventArgs());
         }
      }

      #endregion
   }
}