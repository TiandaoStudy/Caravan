using System.Data.Entity;
using Sample.DataModel.Entities;

namespace Sample.DataAccess.EntityFramework
{
   public class NorthwindDbContext : DbContext
   {
      #region Construction

      private NorthwindDbContext()
      {
         // Non va usato, chiamare Instance().
      }

      private NorthwindDbContext(string nameOrConnectionString)
         : base(nameOrConnectionString)
      {
         // Non va usato, chiamare Instance().
      }

      public static void Initialize()
      {
         // Database initialization, without Code First migrations.
         Database.SetInitializer<NorthwindDbContext>(null);
         using (var ctx = new NorthwindDbContext())
         {
            ctx.Database.Initialize(false);
         }
      }

      // Importante, qui gestire eventuale cache.
      public static NorthwindDbContext Instance()
      {
         return new NorthwindDbContext();
      }

      public static NorthwindDbContext Instance(string nameOrConnectionString)
      {
         return new NorthwindDbContext(nameOrConnectionString);
      }

      #endregion

      #region Tables

      public DbSet<Customer> Customers { get; set; }
      public DbSet<Employee> Employees { get; set; }
      public DbSet<Shipper> Shippers { get; set; }

      #endregion
   }
}