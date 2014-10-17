﻿using System.Data.Entity;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Core
{
   internal abstract class DbContextBase : CaravanDbContext<DbContextBase>
   {
      protected DbContextBase() : base(QueryExecutor.Instance.OpenConnection(), true)
      {
      }

      public DbSet<LogEntry> LogEntry { get; set; }
      public DbSet<LogSettings> LogSettings { get; set; }
      public DbSet<SecApp> SecApps { get; set; }
      public DbSet<SecGroup> SecGroups { get; set; }
      public DbSet<SecUser> SecUsers { get; set; }
   }
}
