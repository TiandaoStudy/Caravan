using System.Collections.Generic;
using System.Linq;
using LinqToQuerystring;
using Sample.DataModel.Entities;

namespace Sample.DataAccess.EntityFramework
{
   public sealed class EmployeeDao : DaoBase
   {
      public static IList<Employee> RetrieveAll()
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            return ctx.Employees.ToList();
         }
      }

      public static Employee RetrieveById(int employeeId)
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            return ctx.Employees.FirstOrDefault(e => e.EmployeeID == employeeId);
         }
      }

      public static IList<Employee> Query(string queryString)
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            return ctx.Employees.LinqToQuerystring(queryString).ToList();
         }
      }

      public static void Delete(int employeeId)
      {
         using (var ctx = NorthwindDbContext.Instance(ConnectionString))
         {
            var employee = ctx.Employees.FirstOrDefault(e => e.EmployeeID == employeeId);
            if (employee != null)
            {
               ctx.Employees.Remove(employee);
               ctx.SaveChanges();
            }
         }
      }
   }
}