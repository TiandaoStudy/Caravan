using System.Web.UI;

namespace FLEX.Web.UserControls.Ajax
{
   public abstract class AjaxControlBase : UserControl
   {
      protected AjaxControlBase()
      {
         // Default values
         DoPostBack = false;
         Enabled = true;
      }

      public virtual bool DoPostBack { get; set; }

      public virtual bool Enabled { get; set; }
   }
}
