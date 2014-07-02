using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public interface IPageBase
   {
      /// <summary>
      /// 
      /// </summary>
      ErrorHandler ErrorHandler { get; }

      /// <summary>
      /// 
      /// </summary>
      bool HasPageVisibleHandlers { get; }

      /// <summary>
      /// 
      /// </summary>
      HtmlForm MainForm { get; } 

      /// <summary>
      /// 
      /// </summary>
      MenuBar MenuBar { get; }

      ScriptManager ScriptManager { get; }

      /// <summary>
      /// 
      /// </summary>
      event EventHandler Page_Visible;
   }
}