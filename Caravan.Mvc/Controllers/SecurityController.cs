using System.Web.Mvc;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.Mvc.ViewModels.Security;

namespace Finsa.Caravan.Mvc.Controllers
{
   public sealed class SecurityController : Controller
   {
      public static string CaravanQueryService
      {
         get { return DataAccess.Configuration.Instance.CaravanRestServiceUrl + "query/" + Common.Configuration.Instance.ApplicationName + "/"; }
      }

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