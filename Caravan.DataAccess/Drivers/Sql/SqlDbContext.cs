﻿using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

            /************************************************
             * SqlSecUser
             ************************************************/

            // SqlSecUser(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecUser>()
                .HasRequired<SqlSecApp>(x => x.App)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);

            /************************************************
             * SqlSecGroup
             ************************************************/

            // SqlSecGroup(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecGroup>()
                .HasRequired<SqlSecApp>(x => x.App)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);

            // SqlSecGroup(N) <-> SqlSecUser(N)
            mb.Entity<SqlSecGroup>()
                .HasMany<SqlSecUser>(x => x.Users)
                .WithMany(x => x.Groups)
                .Map(x => x.MapLeftKey("CGRP_ID")
                           .MapRightKey("CUSR_ID")
                           .ToTable("CRVN_SEC_USER_GROUPS"));

            /************************************************
             * SqlSecRole
             ************************************************/

            // SqlSecRole(N) <-> SqlSecGroup(1)
            mb.Entity<SqlSecRole>()
                .HasRequired<SqlSecGroup>(x => x.Group)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.GroupId)
                .WillCascadeOnDelete(true);

            // SqlSecRole(N) <-> SqlSecUser(N)
            mb.Entity<SqlSecRole>()
                .HasMany<SqlSecUser>(x => x.Users)
                .WithMany(x => x.Roles)
                .Map(x => x.MapLeftKey("CROL_ID")
                           .MapRightKey("CUSR_ID")
                           .ToTable("CRVN_SEC_USER_ROLES"));

            /************************************************
             * SqlSecContext
             ************************************************/

            // SqlSecContext(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecContext>()
                .HasRequired<SqlSecApp>(x => x.App)
                .WithMany(x => x.Contexts)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);

            /************************************************
             * SqlSecObject
             ************************************************/

            // SqlSecObject(N) <-> SqlSecContext(1)
            mb.Entity<SqlSecObject>()
                .HasRequired<SqlSecContext>(x => x.Context)
                .WithMany(x => x.Objects)
                .HasForeignKey(x => x.ContextId)
                .WillCascadeOnDelete(true);

            /************************************************
             * SqlSecEntry
             ************************************************/

            // SqlSecEntry(N) <-> SqlSecUser(1)
            mb.Entity<SqlSecEntry>()
                .HasOptional<SqlSecUser>(x => x.User)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);

            // SqlSecEntry(N) <-> SqlSecGroup(1)
            mb.Entity<SqlSecEntry>()
                .HasOptional<SqlSecGroup>(x => x.Group)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.GroupId)
                .WillCascadeOnDelete(true);

            // SqlSecEntry(N) <-> SqlSecRole(1)
            mb.Entity<SqlSecEntry>()
                .HasOptional<SqlSecRole>(x => x.Role)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.RoleId)
                .WillCascadeOnDelete(true);

            // SqlSecEntry(N) <-> SqlSecObject(1)
            mb.Entity<SqlSecEntry>()
                .HasRequired<SqlSecObject>(x => x.Object)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.ObjectId)
                .WillCascadeOnDelete(true);

            /************************************************
             * SqlLogSettings
             ************************************************/

            // SqlLogSettings(N) <-> SqlSecApp(1)
            mb.Entity<SqlLogSetting>()
                .HasRequired<SqlSecApp>(x => x.App)
                .WithMany(x => x.LogSettings)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);

            /************************************************
             * SqlLogEntry
             ************************************************/

            // SqlLogEntry(N) <-> SqlSecApp(1)
            mb.Entity<SqlLogEntry>()
                .HasRequired<SqlSecApp>(x => x.App)
                .WithMany(x => x.LogEntries)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);

            // SqlLogEntry(N) <-> SqlLogSettings(1)
            mb.Entity<SqlLogEntry>()
                .HasRequired<SqlLogSetting>(x => x.LogSetting)
                .WithMany(x => x.LogEntries)
                .HasForeignKey(x => new { x.AppId, x.LogType })
                .WillCascadeOnDelete(true);
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
            Db.Logger.LogDebugAsync<IDbManager>("EF generated query", logEntry, "Logging and timing the query", new[]
            {
                KeyValuePair.Create("milliseconds", milliseconds.ToString(CultureInfo.InvariantCulture))
            });

            return list;
        }
    }
}