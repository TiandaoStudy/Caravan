using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   TODO
   /// </summary>
   public interface IHead
   {
      /// <summary>
      ///   TODO
      /// </summary>
      ErrorHandler ErrorHandler { get; }

      /// <summary>
      ///   TODO
      /// </summary>
      HtmlForm MainForm { get; }

      /// <summary>
      ///   TODO
      /// </summary>
      ScriptManager ScriptManager { get; }
   }
}
