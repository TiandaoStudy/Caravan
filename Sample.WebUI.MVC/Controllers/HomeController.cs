using System.Web.Mvc;
using FLEX.Web.MVC.Controllers;

namespace Sample.WebUI.MVC.Controllers
{
   public class HomeController : FlexController
   {
      public ActionResult Index()
      {
         return View();
      }

      public ActionResult Search()
      {
         return View();
      }
   }
}