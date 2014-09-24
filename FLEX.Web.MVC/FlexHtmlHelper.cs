using System.Web.Mvc;
using System.Web.Routing;

namespace FLEX.Web.MVC
{
   public sealed class FlexHtmlHelper : HtmlHelper
   {
      public FlexHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : base(viewContext, viewDataContainer)
      {
      }

      public FlexHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection) : base(viewContext, viewDataContainer, routeCollection)
      {
      }
   }
}
