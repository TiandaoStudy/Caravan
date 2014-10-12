using System.Web.Mvc;
using Finsa.Caravan.WebApi.Results;
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