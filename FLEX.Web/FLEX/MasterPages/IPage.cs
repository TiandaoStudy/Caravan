using System;
using FLEX.Web.UserControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   TODO
   /// </summary>
   public interface IPage : IHead
   {
      /// <summary>
      ///   TODO
      /// </summary>
      bool HasPageVisibleHandlers { get; }

      /// <summary>
      ///   TODO
      /// </summary>
      MenuBar MenuBar { get; }

      /// <summary>
      ///   TODO
      /// </summary>
      PageFooter PageFooter { get; }

      /// <summary>
      ///   TODO
      /// </summary>
      event EventHandler Page_Visible;
   }
}