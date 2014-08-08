using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public interface IPage : IHead
   {
      /// <summary>
      /// 
      /// </summary>
      bool HasPageVisibleHandlers { get; }

      /// <summary>
      /// 
      /// </summary>
      MenuBar MenuBar { get; }

      /// <summary>
      /// 
      /// </summary>
      PageFooter PageFooter { get; }

      /// <summary>
      /// 
      /// </summary>
      event EventHandler Page_Visible;
   }
}