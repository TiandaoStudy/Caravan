// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class PopupExitButton : ControlBase
   {
      #region Public Properties

      public string Text
      {
         get { return lblText.Text; }
         set { lblText.Text = value; }
      }

      #endregion
   }
}