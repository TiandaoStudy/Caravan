using System.Web.Mvc;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.Mvc.ViewModels.Security;

namespace Finsa.Caravan.Mvc.Controllers
{
   public class SecurityController : Controller
   {
      public ActionResult SecGroupList()
      {
         var viewModel = new SecGroupList
         {
            Groups = Db.Security.Groups(Common.Configuration.Instance.ApplicationName)
         };
         return View(viewModel);
      }
   }
}