using System.Web.UI;

namespace FLEX.Web.UserControls.Ajax
{
   public abstract class AjaxControlBase : UserControl
   {
      protected AjaxControlBase()
      {
         // Default values
         DoPostBack = true;
         Enabled = true;
      }

      public bool DoPostBack { get; set; }

      public bool Enabled { get; set; }
   }
}
