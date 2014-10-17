using System.Data.Common;
using System.Data.Entity;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess
{
   public abstract class CaravanDbContext<TCtx> : DbContext where TCtx : CaravanDbContext<TCtx>
   {
      protected CaravanDbContext()
      {
         Init();
      }

      protected CaravanDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
      {
         Init();
      }

      protected CaravanDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
      {
         Init();
      }

      private static void Init()
      {
         Database.SetInitializer<TCtx>(null);
      }
   }
}
