using System;
using System.Collections.Generic;
using System.Data.Entity;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Core
{
   internal abstract class DbContextBase : CaravanDbContext<DbContextBase>
   {
      internal DbContextBase() : base(Db.Manager.CreateConnection(), true)
      {
      }

      public DbSet<LogEntry> LogEntries { get; set; }

      public DbSet<LogSettings> LogSettings { get; set; }

      public DbSet<SecApp> SecApps { get; set; }

      public DbSet<SecContext> SecContexts { get; set; }

      public DbSet<SecEntry> SecEntries { get; set; }

      public DbSet<SecGroup> SecGroups { get; set; }

      public DbSet<SecObject> SecObjects { get; set; }

      public DbSet<SecUser> SecUsers { get; set; }
   }

   internal static class DbContextExtensions
   {
      public static void RemoveRange<T>(this DbSet<T> dbSet, IEnumerable<T> items) where T : class
      {
         foreach (var item in items)
         {
            dbSet.Remove(item);
         }
      }

      internal static string ToLowerOrEmpty(this string str)
      {
         return str == null ? String.Empty : str.ToLower();
      }
   }
}
