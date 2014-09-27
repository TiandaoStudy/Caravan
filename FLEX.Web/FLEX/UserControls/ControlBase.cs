using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public abstract class ControlBase : UserControl
   {
      public IHead Master
      {
         get { return Page.Master as IHead; }
      }

      public string JQueryID()
      {
         return JQueryID(this);
      }

      public string JQueryID(Control control)
      {
         return String.Format("\"#{0}\"", control.ClientID);
      }

      protected static string EncodeJsNumber(decimal d)
      {
         return Convert.ToString(d, CultureInfo.InvariantCulture);
      }

      protected static string EncodeJsNumber(decimal? d)
      {
         return d.HasValue ? EncodeJsNumber(d.Value) : String.Empty;
      }

      protected static string EncodeJsNumber(int i)
      {
         return Convert.ToString(i, CultureInfo.InvariantCulture);
      }

      protected static string EncodeJsNumber(int? i)
      {
         return i.HasValue ? EncodeJsNumber(i.Value) : String.Empty;
      }
   }
}