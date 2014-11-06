using System.Data.Common;
using System.Data.Entity;

namespace Finsa.Caravan.DataAccess
{
   public static class CaravanDbContext
   {
      public static void Init<TCtx>() where TCtx : DbContext
      {
         Database.SetInitializer<TCtx>(null);
      }
   }

   public abstract class CaravanDbContext<TCtx> : DbContext where TCtx : CaravanDbContext<TCtx>
   {
      protected CaravanDbContext()
      {
      }

      protected CaravanDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
      {
      }

      protected CaravanDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
      {
      }

      public DbTransaction BeginTransaction()
      {
         return Database.Connection.BeginTransaction();
      }
   }
}
