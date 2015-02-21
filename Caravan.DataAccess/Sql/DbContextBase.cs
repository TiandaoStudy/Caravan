﻿using System.Data.Entity;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Security;

namespace Finsa.Caravan.DataAccess.Sql
{
    internal abstract class DbContextBase : CaravanDbContext<DbContextBase>
    {
        internal DbContextBase()
            : base(Db.Manager.OpenConnection(), true)
        {
        }

        public DbSet<LogEntry> LogEntries { get; set; }

        public DbSet<LogSetting> LogSettings { get; set; }

        public DbSet<SecApp> SecApps { get; set; }

        public DbSet<SecContext> SecContexts { get; set; }

        public DbSet<SecEntry> SecEntries { get; set; }

        public DbSet<SecGroup> SecGroups { get; set; }

        public DbSet<SecObject> SecObjects { get; set; }

        public DbSet<SecUser> SecUsers { get; set; }
    }
}