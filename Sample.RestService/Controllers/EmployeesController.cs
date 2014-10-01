using System.Net;
using System.Web.Mvc;
using FLEX.WebAPI;
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

      public ActionResult Delete(int employeeId)
      {
         EmployeeDao.Delete(employeeId);
         return new HttpStatusCodeResult(HttpStatusCode.OK);
      }
   }
}