using System.Web.Mvc;
using FLEX.Web.MVC.Controllers;

namespace Sample.WebUI.MVC.Controllers
{
   public class OverviewController : FlexController
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