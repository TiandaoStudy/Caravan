using System.Net;
using System.Web.Mvc;
using Finsa.Caravan.WebApi.Results;
using Sample.DataAccess.EntityFramework;

namespace RestService.Controllers
{
   public class EmployeesController : Controller
   {
      public JsonNetResult Index()
      {
         return JsonNetResult.For(EmployeeDao.RetrieveAll());
      }

      public JsonNetResult GetById(int employeeId)
      {
         return JsonNetResult.For(EmployeeDao.RetrieveById(employeeId));
      }

      [HttpPost]
      public JsonNetResult Query(string q)
      {
         return JsonNetResult.For(EmployeeDao.Query(q));
      }

      public ActionResult Delete(int employeeId)
      {
         EmployeeDao.Delete(employeeId);
         return new HttpStatusCodeResult(HttpStatusCode.OK);
      }
   }
}