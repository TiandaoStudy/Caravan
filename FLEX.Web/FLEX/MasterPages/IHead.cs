using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public interface IHead
   {
      /// <summary>
      /// 
      /// </summary>
      ErrorHandler ErrorHandler { get; }

      /// <summary>
      /// 
      /// </summary>
      HtmlForm MainForm { get; }

      /// <summary>
      /// 
      /// </summary>
      ScriptManager ScriptManager { get; }
   }
}
