using System.Linq;
using System.Net;
using System.Web.Mvc;
using FLEX.WebAPI;
using LinqToQuerystring;
using Sample.DataAccess.EntityFramework;

namespace RestService.Controllers
{
   public class CustomersController : Controller
   {
      public JsonNetResult Index()
      {
         return JsonNetResult.For(CustomersDao.RetrieveAll());
      }

      public JsonNetResult GetById(string customerId)
      {
         return JsonNetResult.For(CustomersDao.RetrieveById(customerId));
      }

      public JsonNetResult Query(string q)
      {
         return JsonNetResult.For(CustomersDao.RetrieveAll().AsQueryable().LinqToQuerystring(q));
      }

      public ActionResult Delete(string customerId)
      {
         CustomersDao.Delete(customerId);
         return new HttpStatusCodeResult(HttpStatusCode.OK);
      }
   }
}