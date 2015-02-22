﻿using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Properties;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Finsa.Caravan.DataAccess.Sql.Models.Logging;
using Finsa.Caravan.DataAccess.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Sql
{
    internal sealed class SqlDbContext : DbContextBase
    {
        static SqlDbContext()
        {
            CaravanDbContext.Init<SqlDbContext>();
        }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            base.OnModelCreating(mb);

            /************************************************
             * SqlSecApp
             ************************************************/

            mb.Entity<SqlSecApp>().ToTable("CARAVAN_SEC_APP", Settings.Default.SqlSchema);
            mb.Entity<SqlSecApp>().HasKey(x => x.Id);

            mb.Entity<SqlSecApp>().Property(x => x.Id).HasColumnName("CAPP_ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mb.Entity<SqlSecApp>().Property(x => x.Name).HasColumnName("CAPP_NAME");
            mb.Entity<SqlSecApp>().Property(x => x.Description).HasColumnName("CAPP_DESCRIPTION");

            /************************************************
             * SqlSecUser
             ************************************************/

            mb.Entity<SqlSecUser>().ToTable("CARAVAN_SEC_USER", Settings.Default.SqlSchema);
            mb.Entity<SqlSecUser>().HasKey(x => new { x.Id, x.AppId });

            mb.Entity<SqlSecUser>().Property(x => x.Id).HasColumnName("CUSR_ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mb.Entity<SqlSecUser>().Property(x => x.AppId).HasColumnName("capp_id");
            mb.Entity<SqlSecUser>().Property(x => x.Active).HasColumnName("CUSR_ACTIVE");
            mb.Entity<SqlSecUser>().Property(x => x.Login).HasColumnName("CUSR_LOGIN");
            mb.Entity<SqlSecUser>().Property(x => x.HashedPassword).HasColumnName("CUSR_HASHED_PWD");
            mb.Entity<SqlSecUser>().Property(x => x.FirstName).HasColumnName("CUSR_FIRST_NAME");
            mb.Entity<SqlSecUser>().Property(x => x.LastName).HasColumnName("CUSR_LAST_NAME");
            mb.Entity<SqlSecUser>().Property(x => x.Email).HasColumnName("CUSR_EMAIL");

            // SqlSecUser(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecUser>()
               .HasRequired<SqlSecApp>(x => x.App)
               .WithMany(x => x.Users)
               .HasForeignKey(x => x.AppId);

            /************************************************
             * SqlSecGroup
             ************************************************/

            mb.Entity<SqlSecGroup>().ToTable("CARAVAN_SEC_GROUP", Settings.Default.SqlSchema);
            mb.Entity<SqlSecGroup>().HasKey(x => new { x.Id, x.AppId });

            mb.Entity<SqlSecGroup>().Property(x => x.Id).HasColumnName("cgrp_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mb.Entity<SqlSecGroup>().Property(x => x.AppId).HasColumnName("capp_id");
            mb.Entity<SqlSecGroup>().Property(x => x.Name).HasColumnName("CGRP_NAME");
            mb.Entity<SqlSecGroup>().Property(x => x.Description).HasColumnName("CGRP_DESCRIPTION");
            mb.Entity<SqlSecGroup>().Property(x => x.IsAdmin).HasColumnName("CGRP_ADMIN");
            mb.Entity<SqlSecGroup>().Property(x => x.Notes).HasColumnName("CGRP_NOTES");

            // SqlSecGroup(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecGroup>()
               .HasRequired<SqlSecApp>(x => x.App)
               .WithMany(x => x.Groups)
               .HasForeignKey(x => x.AppId);

            // SqlSecGroup(N) <-> SqlSecUser(N)
            mb.Entity<SqlSecGroup>()
               .HasMany<SqlSecUser>(x => x.Users)
               .WithMany(x => x.Groups)
               .Map(x => x.MapLeftKey("CGRP_ID", "CGRP_APP_ID").MapRightKey("CUSR_ID", "CUSR_APP_ID").ToTable("CARAVAN_SEC_USER_GROUP", Settings.Default.SqlSchema));

            /************************************************
             * SqlSecContext
             ************************************************/

            mb.Entity<SqlSecContext>().ToTable("CARAVAN_SEC_CONTEXT", Settings.Default.SqlSchema);
            mb.Entity<SqlSecContext>().HasKey(x => new { x.Id, x.AppId });

            mb.Entity<SqlSecContext>().Property(x => x.Id).HasColumnName("cctx_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mb.Entity<SqlSecContext>().Property(x => x.AppId).HasColumnName("capp_id");
            mb.Entity<SqlSecContext>().Property(x => x.Name).HasColumnName("CCTX_NAME");
            mb.Entity<SqlSecContext>().Property(x => x.Description).HasColumnName("CCTX_DESCRIPTION");

            // SqlSecContext(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecContext>()
               .HasRequired<SqlSecApp>(x => x.App)
               .WithMany(x => x.Contexts)
               .HasForeignKey(x => x.AppId);

            /************************************************
             * SqlSecObject
             ************************************************/

            mb.Entity<SqlSecObject>().ToTable("CARAVAN_SEC_OBJECT", Settings.Default.SqlSchema);
            mb.Entity<SqlSecObject>().HasKey(x => new { x.Id, x.ContextId, x.AppId });

            mb.Entity<SqlSecObject>().Property(x => x.Id).HasColumnName("cobj_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mb.Entity<SqlSecObject>().Property(x => x.ContextId).HasColumnName("cctx_id");
            mb.Entity<SqlSecObject>().Property(x => x.AppId).HasColumnName("capp_id");
            mb.Entity<SqlSecObject>().Property(x => x.Name).HasColumnName("COBJ_NAME");
            mb.Entity<SqlSecObject>().Property(x => x.Description).HasColumnName("COBJ_DESCRIPTION");
            mb.Entity<SqlSecObject>().Property(x => x.Type).HasColumnName("COBJ_TYPE");

            // SqlSecObject(N) <-> SqlSecContext(1)
            mb.Entity<SqlSecObject>()
               .HasRequired<SqlSecContext>(x => x.Context)
               .WithMany(x => x.Objects)
               .HasForeignKey(x => new { x.ContextId, x.AppId });

            // SqlSecObject(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecObject>()
               .HasRequired<SqlSecApp>(x => x.App)
               .WithMany(x => x.Objects)
               .HasForeignKey(x => x.AppId);

            /************************************************
             * SqlSecEntry
             ************************************************/

            mb.Entity<SqlSecEntry>().ToTable("CARAVAN_SECURITY", Settings.Default.SqlSchema);
            mb.Entity<SqlSecEntry>().HasKey(x => new { x.Id, x.AppId });

            mb.Entity<SqlSecEntry>().Property(x => x.Id).HasColumnName("csec_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mb.Entity<SqlSecEntry>().Property(x => x.AppId).HasColumnName("capp_id");
            mb.Entity<SqlSecEntry>().Property(x => x.UserId).HasColumnName("CUSR_ID");
            mb.Entity<SqlSecEntry>().Property(x => x.GroupId).HasColumnName("CGRP_ID");
            mb.Entity<SqlSecEntry>().Property(x => x.ContextId).HasColumnName("CCTX_ID");
            mb.Entity<SqlSecEntry>().Property(x => x.ObjectId).HasColumnName("COBJ_ID");

            // SqlSecEntry(N) <-> SqlSecApp(1)
            mb.Entity<SqlSecEntry>()
               .HasRequired<SqlSecApp>(x => x.App)
               .WithMany(x => x.SecEntries)
               .HasForeignKey(x => x.AppId);

            // SqlSecEntry(N) <-> SqlSecUser(1)
            mb.Entity<SqlSecEntry>()
               .HasOptional<SqlSecUser>(x => x.User)
               .WithMany(x => x.SecEntries)
               .HasForeignKey(x => new { x.UserId, x.AppId });

            // SqlSecEntry(N) <-> SqlSecGroup(1)
            mb.Entity<SqlSecEntry>()
               .HasOptional<SqlSecGroup>(x => x.Group)
               .WithMany(x => x.SecEntries)
               .HasForeignKey(x => new { x.GroupId, x.AppId });

            // SqlSecEntry(N) <-> SqlSecContext(1)
            mb.Entity<SqlSecEntry>()
               .HasRequired<SqlSecContext>(x => x.Context)
               .WithMany(x => x.SecEntries)
               .HasForeignKey(x => new { x.ContextId, x.AppId });

            // SqlSecEntry(N) <-> SqlSecObject(1)
            mb.Entity<SqlSecEntry>()
               .HasRequired<SqlSecObject>(x => x.Object)
               .WithMany(x => x.SecEntries)
               .HasForeignKey(x => new { x.ObjectId, x.ContextId, x.AppId });

            /************************************************
             * SqlLogSettings
             ************************************************/

            mb.Entity<SqlLogSetting>().ToTable("CARAVAN_LOG_SETTINGS", Settings.Default.SqlSchema);
            mb.Entity<SqlLogSetting>().HasKey(x => new { x.AppId, x.LogType });

            mb.Entity<SqlLogSetting>().Property(x => x.AppId).HasColumnName("capp_id");
            mb.Entity<SqlLogSetting>().Property(x => x.LogType).HasColumnName("CLOS_TYPE");
            mb.Entity<SqlLogSetting>().Property(x => x.Enabled).HasColumnName("CLOS_ENABLED");
            mb.Entity<SqlLogSetting>().Property(x => x.Days).HasColumnName("CLOS_DAYS");
            mb.Entity<SqlLogSetting>().Property(x => x.MaxEntries).HasColumnName("CLOS_MAX_ENTRIES");

            // SqlLogSettings(N) <-> SqlSecApp(1)
            mb.Entity<SqlLogSetting>()
               .HasRequired<SqlSecApp>(x => x.App)
               .WithMany(x => x.LogSettings)
               .HasForeignKey(x => x.AppId);

            /************************************************
             * SqlLogEntry
             ************************************************/

            mb.Entity<SqlLogEntry>().ToTable("CARAVAN_LOG", Settings.Default.SqlSchema);
            mb.Entity<SqlLogEntry>().HasKey(x => new { x.Id, x.AppId });

            mb.Entity<SqlLogEntry>().Property(x => x.Id).HasColumnName("clog_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mb.Entity<SqlLogEntry>().Property(x => x.AppId).HasColumnName("capp_id");
            mb.Entity<SqlLogEntry>().Property(x => x.LogType).HasColumnName("CLOS_TYPE");
            mb.Entity<SqlLogEntry>().Property(x => x.Date).HasColumnName("CLOG_DATE");
            mb.Entity<SqlLogEntry>().Property(x => x.UserLogin).HasColumnName("CLOG_USER");
            mb.Entity<SqlLogEntry>().Property(x => x.CodeUnit).HasColumnName("CLOG_CODE_UNIT");
            mb.Entity<SqlLogEntry>().Property(x => x.Function).HasColumnName("CLOG_FUNCTION");
            mb.Entity<SqlLogEntry>().Property(x => x.ShortMessage).HasColumnName("CLOG_SHORT_MSG");
            mb.Entity<SqlLogEntry>().Property(x => x.LongMessage).HasColumnName("CLOG_LONG_MSG");
            mb.Entity<SqlLogEntry>().Property(x => x.Context).HasColumnName("CLOG_CONTEXT");
            mb.Entity<SqlLogEntry>().Property(x => x.Key0).HasColumnName("CLOG_KEY_0");
            mb.Entity<SqlLogEntry>().Property(x => x.Value0).HasColumnName("CLOG_VALUE_0");
            mb.Entity<SqlLogEntry>().Property(x => x.Key1).HasColumnName("CLOG_KEY_1");
            mb.Entity<SqlLogEntry>().Property(x => x.Value1).HasColumnName("CLOG_VALUE_1");
            mb.Entity<SqlLogEntry>().Property(x => x.Key2).HasColumnName("CLOG_KEY_2");
            mb.Entity<SqlLogEntry>().Property(x => x.Value2).HasColumnName("CLOG_VALUE_2");
            mb.Entity<SqlLogEntry>().Property(x => x.Key3).HasColumnName("CLOG_KEY_3");
            mb.Entity<SqlLogEntry>().Property(x => x.Value3).HasColumnName("CLOG_VALUE_3");
            mb.Entity<SqlLogEntry>().Property(x => x.Key4).HasColumnName("CLOG_KEY_4");
            mb.Entity<SqlLogEntry>().Property(x => x.Value4).HasColumnName("CLOG_VALUE_4");
            mb.Entity<SqlLogEntry>().Property(x => x.Key5).HasColumnName("CLOG_KEY_5");
            mb.Entity<SqlLogEntry>().Property(x => x.Value5).HasColumnName("CLOG_VALUE_5");
            mb.Entity<SqlLogEntry>().Property(x => x.Key6).HasColumnName("CLOG_KEY_6");
            mb.Entity<SqlLogEntry>().Property(x => x.Value6).HasColumnName("CLOG_VALUE_6");
            mb.Entity<SqlLogEntry>().Property(x => x.Key7).HasColumnName("CLOG_KEY_7");
            mb.Entity<SqlLogEntry>().Property(x => x.Value7).HasColumnName("CLOG_VALUE_7");
            mb.Entity<SqlLogEntry>().Property(x => x.Key8).HasColumnName("CLOG_KEY_8");
            mb.Entity<SqlLogEntry>().Property(x => x.Value8).HasColumnName("CLOG_VALUE_8");
            mb.Entity<SqlLogEntry>().Property(x => x.Key9).HasColumnName("CLOG_KEY_9");
            mb.Entity<SqlLogEntry>().Property(x => x.Value9).HasColumnName("CLOG_VALUE_9");

            // SqlLogEntry(N) <-> SqlSecApp(1)
            mb.Entity<SqlLogEntry>()
               .HasRequired<SqlSecApp>(x => x.App)
               .WithMany(x => x.LogEntries)
               .HasForeignKey(x => x.AppId);

            // SqlLogEntry(N) <-> SqlLogSettings(1)
            mb.Entity<SqlLogEntry>()
               .HasRequired<SqlLogSetting>(x => x.LogSetting)
               .WithMany(x => x.LogEntries)
               .HasForeignKey(x => new { x.AppId, x.LogType });
        }
    }
}