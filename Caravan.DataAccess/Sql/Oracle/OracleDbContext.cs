using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.DataAccess.Sql.Oracle
{
   internal sealed class OracleDbContext : DbContextBase
   {
      protected override void OnModelCreating(DbModelBuilder mb)
      {
         base.OnModelCreating(mb);
         
         /************************************************
          * SecApp
          ************************************************/

         mb.Entity<SecApp>().ToTable("CARAVAN_SEC_APP", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<SecApp>().HasKey(x => x.Id);
         mb.Entity<SecApp>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

         mb.Entity<SecApp>().Property(x => x.Id).HasColumnName("CAPP_ID");
         mb.Entity<SecApp>().Property(x => x.Name).HasColumnName("CAPP_NAME");
         mb.Entity<SecApp>().Property(x => x.Description).HasColumnName("CAPP_DESCRIPTION");

         /************************************************
          * SecUser
          ************************************************/

         mb.Entity<SecUser>().ToTable("CARAVAN_SEC_USER", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<SecUser>().HasKey(x => new {x.Id, x.AppId});

         mb.Entity<SecUser>().Property(x => x.Id).HasColumnName("CUSR_ID");
         mb.Entity<SecUser>().Property(x => x.AppId).HasColumnName("CAPP_ID");
         mb.Entity<SecUser>().Property(x => x.Active).HasColumnName("CUSR_ACTIVE");
         mb.Entity<SecUser>().Property(x => x.Login).HasColumnName("CUSR_LOGIN");
         mb.Entity<SecUser>().Property(x => x.HashedPassword).HasColumnName("CUSR_HASHED_PWD");
         mb.Entity<SecUser>().Property(x => x.FirstName).HasColumnName("CUSR_FIRST_NAME");
         mb.Entity<SecUser>().Property(x => x.LastName).HasColumnName("CUSR_LAST_NAME");
         mb.Entity<SecUser>().Property(x => x.Email).HasColumnName("CUSR_EMAIL");

         // SecUser(N) <-> SecApp(1)
         mb.Entity<SecUser>()
            .HasRequired<SecApp>(x => x.App)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.AppId);

         /************************************************
          * SecGroup
          ************************************************/
         
         mb.Entity<SecGroup>().ToTable("CARAVAN_SEC_GROUP", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<SecGroup>().HasKey(x => new { x.Id, x.AppId });

         mb.Entity<SecGroup>().Property(x => x.Id).HasColumnName("CGRP_ID");
         mb.Entity<SecGroup>().Property(x => x.AppId).HasColumnName("CAPP_ID");
         mb.Entity<SecGroup>().Property(x => x.Name).HasColumnName("CGRP_NAME");
         mb.Entity<SecGroup>().Property(x => x.Description).HasColumnName("CGRP_DESCRIPTION");
         mb.Entity<SecGroup>().Property(x => x.IsAdmin).HasColumnName("CGRP_ADMIN");

         // SecGroup(N) <-> SecApp(1)
         mb.Entity<SecGroup>()
            .HasRequired<SecApp>(x => x.App)
            .WithMany(x => x.Groups)
            .HasForeignKey(x => x.AppId);
         
         // SecGroup(N) <-> SecUser(N)
         mb.Entity<SecGroup>()
            .HasMany<SecUser>(x => x.Users)
            .WithMany(x => x.Groups)
            .Map(x => x.MapLeftKey("CGRP_ID", "CGRP_APP_ID").MapRightKey("CUSR_ID", "CUSR_APP_ID").ToTable("CARAVAN_SEC_USER_GROUP"));

         /************************************************
          * SecContext
          ************************************************/

         mb.Entity<SecContext>().ToTable("CARAVAN_SEC_CONTEXT", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<SecContext>().HasKey(x => new {x.Id, x.AppId});

         mb.Entity<SecContext>().Property(x => x.Id).HasColumnName("CCTX_ID");
         mb.Entity<SecContext>().Property(x => x.AppId).HasColumnName("CAPP_ID");
         mb.Entity<SecContext>().Property(x => x.Name).HasColumnName("CCTX_NAME");
         mb.Entity<SecContext>().Property(x => x.Description).HasColumnName("CCTX_DESCRIPTION");

         // SecContext(N) <-> SecApp(1)
         mb.Entity<SecContext>()
            .HasRequired<SecApp>(x => x.App)
            .WithMany(x => x.Contexts)
            .HasForeignKey(x => x.AppId);

         /************************************************
          * SecObject
          ************************************************/

         mb.Entity<SecObject>().ToTable("CARAVAN_SEC_OBJECT", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<SecObject>().HasKey(x => new {x.Id, x.ContextId, x.AppId});

         mb.Entity<SecObject>().Property(x => x.Id).HasColumnName("COBJ_ID");
         mb.Entity<SecObject>().Property(x => x.ContextId).HasColumnName("CCTX_ID");
         mb.Entity<SecObject>().Property(x => x.AppId).HasColumnName("CAPP_ID");
         mb.Entity<SecObject>().Property(x => x.Name).HasColumnName("COBJ_NAME");
         mb.Entity<SecObject>().Property(x => x.Description).HasColumnName("COBJ_DESCRIPTION");
         mb.Entity<SecObject>().Property(x => x.Type).HasColumnName("COBJ_TYPE");

         // SecObject(N) <-> SecContext(1)
         mb.Entity<SecObject>()
            .HasRequired<SecContext>(x => x.Context)
            .WithMany(x => x.Objects)
            .HasForeignKey(x => new {x.ContextId, x.AppId});

         // SecObject(N) <-> SecApp(1)
         mb.Entity<SecObject>()
            .HasRequired<SecApp>(x => x.App)
            .WithMany(x => x.Objects)
            .HasForeignKey(x => x.AppId);

         /************************************************
          * SecEntry
          ************************************************/

         mb.Entity<SecEntry>().ToTable("CARAVAN_SECURITY", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<SecEntry>().HasKey(x => new {x.Id, x.AppId});

         mb.Entity<SecEntry>().Property(x => x.Id).HasColumnName("CSEC_ID");
         mb.Entity<SecEntry>().Property(x => x.AppId).HasColumnName("CAPP_ID");
         mb.Entity<SecEntry>().Property(x => x.UserId).HasColumnName("CUSR_ID");
         mb.Entity<SecEntry>().Property(x => x.GroupId).HasColumnName("CGRP_ID");
         mb.Entity<SecEntry>().Property(x => x.ContextId).HasColumnName("CCTX_ID");
         mb.Entity<SecEntry>().Property(x => x.ObjectId).HasColumnName("COBJ_ID");
         
         // SecEntry(N) <-> SecApp(1)
         mb.Entity<SecEntry>()
            .HasRequired<SecApp>(x => x.App)
            .WithMany(x => x.SecEntries)
            .HasForeignKey(x => x.AppId);

         // SecEntry(N) <-> SecUser(1)
         mb.Entity<SecEntry>()
            .HasOptional<SecUser>(x => x.User)
            .WithMany(x => x.SecEntries)
            .HasForeignKey(x => new {x.UserId, x.AppId});

         // SecEntry(N) <-> SecGroup(1)
         mb.Entity<SecEntry>()
            .HasOptional<SecGroup>(x => x.Group)
            .WithMany(x => x.SecEntries)
            .HasForeignKey(x => new {x.GroupId, x.AppId});

         // SecEntry(N) <-> SecContext(1)
         mb.Entity<SecEntry>()
            .HasRequired<SecContext>(x => x.Context)
            .WithMany(x => x.SecEntries)
            .HasForeignKey(x => new {x.ContextId, x.AppId});

         // SecEntry(N) <-> SecObject(1)
         mb.Entity<SecEntry>()
            .HasRequired<SecObject>(x => x.Object)
            .WithMany(x => x.SecEntries)
            .HasForeignKey(x => new {x.ObjectId, x.ContextId, x.AppId});

         /************************************************
          * LogSettings
          ************************************************/

         mb.Entity<LogSettings>().ToTable("CARAVAN_LOG_SETTINGS", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<LogSettings>().HasKey(x => new {x.AppId, x.TypeId});

         mb.Entity<LogSettings>().Property(x => x.AppId).HasColumnName("CAPP_ID");
         mb.Entity<LogSettings>().Property(x => x.TypeId).HasColumnName("CLOS_TYPE");
         mb.Entity<LogSettings>().Property(x => x.Enabled).HasColumnName("CLOS_ENABLED");
         mb.Entity<LogSettings>().Property(x => x.Days).HasColumnName("CLOS_DAYS");
         mb.Entity<LogSettings>().Property(x => x.MaxEntries).HasColumnName("CLOS_MAX_ENTRIES");
         mb.Entity<LogSettings>().Ignore(x => x.Type);

         // LogSettings(N) <-> SecApp(1)
         mb.Entity<LogSettings>()
            .HasRequired<SecApp>(x => x.App)
            .WithMany(x => x.LogSettings)
            .HasForeignKey(x => x.AppId);

         /************************************************
          * LogEntry
          ************************************************/

         mb.Entity<LogEntry>().ToTable("CARAVAN_LOG", DataAccess.Configuration.Instance.OracleUser);
         mb.Entity<LogEntry>().HasKey(x => x.Id);
         mb.Entity<LogEntry>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

         mb.Entity<LogEntry>().Property(x => x.Id).HasColumnName("CLOG_ID");
         mb.Entity<LogEntry>().Property(x => x.AppId).HasColumnName("CAPP_ID");
         mb.Entity<LogEntry>().Property(x => x.TypeId).HasColumnName("CLOS_TYPE");
         mb.Entity<LogEntry>().Property(x => x.Date).HasColumnName("CLOG_DATE");
         mb.Entity<LogEntry>().Property(x => x.UserLogin).HasColumnName("CLOG_USER");
         mb.Entity<LogEntry>().Property(x => x.CodeUnit).HasColumnName("CLOG_CODE_UNIT");
         mb.Entity<LogEntry>().Property(x => x.Function).HasColumnName("CLOG_FUNCTION");
         mb.Entity<LogEntry>().Property(x => x.ShortMessage).HasColumnName("CLOG_SHORT_MSG");
         mb.Entity<LogEntry>().Property(x => x.LongMessage).HasColumnName("CLOG_LONG_MSG");
         mb.Entity<LogEntry>().Property(x => x.Context).HasColumnName("CLOG_CONTEXT");
         mb.Entity<LogEntry>().Property(x => x.Key0).HasColumnName("CLOG_KEY_0");
         mb.Entity<LogEntry>().Property(x => x.Value0).HasColumnName("CLOG_VALUE_0");
         mb.Entity<LogEntry>().Property(x => x.Key1).HasColumnName("CLOG_KEY_1");
         mb.Entity<LogEntry>().Property(x => x.Value1).HasColumnName("CLOG_VALUE_1");
         mb.Entity<LogEntry>().Property(x => x.Key2).HasColumnName("CLOG_KEY_2");
         mb.Entity<LogEntry>().Property(x => x.Value2).HasColumnName("CLOG_VALUE_2");
         mb.Entity<LogEntry>().Property(x => x.Key3).HasColumnName("CLOG_KEY_3");
         mb.Entity<LogEntry>().Property(x => x.Value3).HasColumnName("CLOG_VALUE_3");
         mb.Entity<LogEntry>().Property(x => x.Key4).HasColumnName("CLOG_KEY_4");
         mb.Entity<LogEntry>().Property(x => x.Value4).HasColumnName("CLOG_VALUE_4");
         mb.Entity<LogEntry>().Property(x => x.Key5).HasColumnName("CLOG_KEY_5");
         mb.Entity<LogEntry>().Property(x => x.Value5).HasColumnName("CLOG_VALUE_5");
         mb.Entity<LogEntry>().Property(x => x.Key6).HasColumnName("CLOG_KEY_6");
         mb.Entity<LogEntry>().Property(x => x.Value6).HasColumnName("CLOG_VALUE_6");
         mb.Entity<LogEntry>().Property(x => x.Key7).HasColumnName("CLOG_KEY_7");
         mb.Entity<LogEntry>().Property(x => x.Value7).HasColumnName("CLOG_VALUE_7");
         mb.Entity<LogEntry>().Property(x => x.Key8).HasColumnName("CLOG_KEY_8");
         mb.Entity<LogEntry>().Property(x => x.Value8).HasColumnName("CLOG_VALUE_8");
         mb.Entity<LogEntry>().Property(x => x.Key9).HasColumnName("CLOG_KEY_9");
         mb.Entity<LogEntry>().Property(x => x.Value9).HasColumnName("CLOG_VALUE_9");
         mb.Entity<LogEntry>().Ignore(x => x.Type);
         mb.Entity<LogEntry>().Ignore(x => x.Arguments);

         // LogEntry(N) <-> SecApp(1)
         mb.Entity<LogEntry>()
            .HasRequired<SecApp>(x => x.App)
            .WithMany(x => x.LogEntries)
            .HasForeignKey(x => x.AppId);

         // LogEntry(N) <-> LogSettings(1)
         mb.Entity<LogEntry>()
            .HasRequired<LogSettings>(x => x.LogSettings)
            .WithMany(x => x.LogEntries)
            .HasForeignKey(x => new {x.AppId, x.TypeId});
      }
   }
}
