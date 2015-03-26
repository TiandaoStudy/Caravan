﻿using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;
using Finsa.Caravan.DataAccess.Properties;

namespace Finsa.Caravan.DataAccess.Drivers.Sql
{
    internal sealed class SqlDbContext : DbContext
    {
        #region Constants

        internal const short TinyLength = 8;     // 2^3
        internal const short SmallLength = 32;   // 2^5
        internal const short MediumLength = 256; // 2^8
        internal const short LargeLength = 1024; // 2^10

        #endregion Constants

        static SqlDbContext()
        {
            switch (Settings.Default.SqlInitializer)
            {
                case "CreateDatabaseIfNotExists":
                    Database.SetInitializer(new CreateDatabaseIfNotExists<SqlDbContext>());
                    break;
                case "DropCreateDatabaseAlways":
                    Database.SetInitializer(new DropCreateDatabaseAlways<SqlDbContext>());
                    break;
                case "DropCreateDatabaseIfModelChanges":
                    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlDbContext>());
                    break;
                default:
                    Database.SetInitializer<SqlDbContext>(null);
                    break;
            }
        }

        public SqlDbContext()
            : base(GetConnection(), true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static SqlDbContext CreateReadContext()
        {
            var ctx = CreateWriteContext();
            ctx.Configuration.ProxyCreationEnabled = false;
            return ctx;
        }

        public static SqlDbContext CreateWriteContext()
        {
            var ctx = new SqlDbContext();
            ctx.Database.Initialize(false);
            return ctx;
        }

        public static TransactionScope BeginTrasaction()
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions {IsolationLevel = IsolationLevel.Snapshot});
        }

        private static DbConnection GetConnection()
        {
            if (Db.Manager.Kind == DataAccessKind.FakeSql)
            {
                // Needed, otherwise Unit Tests fail.
                return Db.Manager.OpenConnection();
            }
            return Db.Manager.CreateConnection();
        }

        #region DB Sets

        public DbSet<SqlLogEntry> LogEntries { get; set; }

        public DbSet<SqlLogSetting> LogSettings { get; set; }

        public DbSet<SqlSecApp> SecApps { get; set; }

        public DbSet<SqlSecContext> SecContexts { get; set; }

        public DbSet<SqlSecEntry> SecEntries { get; set; }

        public DbSet<SqlSecGroup> SecGroups { get; set; }

        public DbSet<SqlSecObject> SecObjects { get; set; }

        public DbSet<SqlSecUser> SecUsers { get; set; }

        #endregion DB Sets

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Conventions.Remove(new PluralizingTableNameConvention());
            mb.HasDefaultSchema(Settings.Default.SqlSchema);

            base.OnModelCreating(mb);

            mb.Configurations.Add(new SqlSecAppTypeConfiguration());
            mb.Configurations.Add(new SqlSecContextTypeConfiguration());
            mb.Configurations.Add(new SqlSecEntryTypeConfiguration());
            mb.Configurations.Add(new SqlSecGroupTypeConfiguration());
            mb.Configurations.Add(new SqlSecObjectTypeConfiguration());
            mb.Configurations.Add(new SqlSecRoleTypeConfiguration());
            mb.Configurations.Add(new SqlSecUserTypeConfiguration());
            mb.Configurations.Add(new SqlLogSettingTypeConfiguration());
            mb.Configurations.Add(new SqlLogEntryTypeConfiguration());
        }
    }

    public static class QueryableExtensions
    {
        public static List<T> ToLogAndList<T>(this IQueryable<T> queryable)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var list = queryable.ToList();
            stopwatch.Stop();

            // Logging query and execution time.
            var logEntry = queryable.ToString();
            var milliseconds = stopwatch.ElapsedMilliseconds;
            Db.Logger.LogTraceAsync<IDbManager>("EF generated query", logEntry, "Logging and timing the query", new[]
            {
                KeyValuePair.Create("milliseconds", milliseconds.ToString(CultureInfo.InvariantCulture))
            });

            return list;
        }
    }
}