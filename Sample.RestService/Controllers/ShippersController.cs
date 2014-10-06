using System.Web.Mvc;
using FLEX.Web.WebAPI.Results;
using Sample.DataAccess.EntityFramework;

namespace RestService.Controllers
{
   public class ShippersController : Controller
   {
      public JsonNetResult Index()
      {
         return JsonNetResult.For(ShipperDao.RetrieveAll());
      }

      public JsonNetResult GetById(int shipperId)
      {
         return JsonNetResult.For(ShipperDao.RetrieveById(shipperId));
      }
   }
}