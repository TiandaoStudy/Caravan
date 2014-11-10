using System.Collections.Generic;
using System.Linq;
using Sample.DataModel.Entities;

namespace Sample.DataAccess.EntityFramework
{
   public sealed class ShipperDao : DaoBase
   {
      public static IList<Shipper> RetrieveAll()
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            return ctx.Shippers.ToList();
         }
      }

      public static Shipper RetrieveById(int shipperId)
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            return ctx.Shippers.FirstOrDefault(s => s.ShipperID == shipperId);
         }
      }
   }
}