using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
  public interface IPopup : IHead
  {
     void RegisterAlert(Page child, string message);

     void RegisterCloseScript(Page child);
  }
}
