using System.Collections.Generic;
using System.Linq;
using Sample.DataModel.Entities;

namespace Sample.DataAccess.EntityFramework
{
   public sealed class CustomersDao : DaoBase
   {
      public static IList<Customer> RetrieveAll()
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            return ctx.Customers.ToList();
         }
      }

      public static Customer RetrieveById(string customerId)
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            return ctx.Customers.FirstOrDefault(c => c.CustomerID == customerId);
         }
      }

      public static void Delete(string customerId)
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            var customer = ctx.Customers.FirstOrDefault(c => c.CustomerID == customerId);
            if (customer != null)
            {
               ctx.Customers.Remove(customer);
               ctx.SaveChanges();
            }
         }
      }
   }
}