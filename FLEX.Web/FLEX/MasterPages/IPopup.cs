using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   TODO
   /// </summary>
   public interface IPopup : IHead
   {
      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="child"></param>
      /// <param name="message"></param>
      void RegisterAlert(Page child, string message);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="child"></param>
      void RegisterCloseScript(Page child);
   }
}