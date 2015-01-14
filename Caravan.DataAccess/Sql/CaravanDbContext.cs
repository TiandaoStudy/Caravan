using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Finsa.Caravan.DataAccess.Properties;
using PommaLabs.Extensions;

namespace Finsa.Caravan.DataAccess.Sql
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
      #region Construction

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

      #endregion

      public DbTransaction BeginTransaction()
      {
         return Database.Connection.BeginTransaction();
      }

      public void SaveConcurrentChanges(Action<TCtx, DbUpdateConcurrencyException> onFailure)
      {
         for (var i = 0; i < Settings.Default.DefaultSaveChangesRetryCount; ++i)
         {
            try
            {
               SaveChanges();
               break;
            }
            catch (DbUpdateConcurrencyException ex)
            {
               // If this is the last run, then rethrow the exception.
               if (i + 1 == Settings.Default.DefaultSaveChangesRetryCount)
               {
                  throw;
               }

               // Call the handler user has passed as argument, if any.
               onFailure.SafeInvoke(this as TCtx, ex);

               // SaveChanges will be called on the next run.
            }
         }
      }

      public void UndoChanges()
      {
         foreach (var entry in ChangeTracker.Entries())
         {
            switch (entry.State)
            {
               case EntityState.Modified:
                  entry.State = EntityState.Unchanged;
                  break;
               case EntityState.Added:
                  entry.State = EntityState.Detached;
                  break;
               case EntityState.Deleted:
                  entry.Reload();
                  break;
            }
         }
      }

      #region Private Methods

      private void Init()
      {
         Configuration.LazyLoadingEnabled = false;
      }

      #endregion
   }
}