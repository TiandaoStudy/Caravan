using System.Web.Mvc;
using Finsa.Caravan.Mvc.Controllers;

namespace Sample.WebUI.Mvc.Controllers
{
   public class OverviewController : CaravanController
   {
      public ActionResult Index()
      {
         return View();
      }

      public ActionResult Style()
      {
         return View();
      }
   }
}