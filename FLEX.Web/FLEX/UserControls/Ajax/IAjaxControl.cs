using System.Web.UI;

namespace FLEX.Web
{
   public interface IAjaxControl
   {
      bool? DoPostBack { get; set; }

      /// <summary>
      ///   If false, control is disabled, that is, it will not react to user input.
      /// </summary>
      bool? Enabled { get; set; }

      UpdatePanel UpdatePanel { get; }

      void AttachToUpdatePanel(UpdatePanel updatePanel);
   }
}