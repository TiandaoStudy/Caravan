using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Oracle
{
   internal sealed class OracleDbContext : CaravanDbContext<OracleDbContext>
   {
      public OracleDbContext() : base(QueryExecutor.Instance.OpenConnection(), true)
      {
      }

      protected override void OnModelCreating(DbModelBuilder mb)
      {
         base.OnModelCreating(mb);

         mb.HasDefaultSchema(DataAccess.Configuration.Instance.OracleRunner);
         
         /************************************************
          * SecApp
          ************************************************/
         
         mb.Entity<SecApp>()
            .ToTable("CARAVAN_SEC_APP", DataAccess.Configuration.Instance.OracleRunner)
            .HasKey(x => x.Id)
            .Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

         mb.Entity<SecApp>().Property(x => x.Id).HasColumnName("CAPP_ID");
         mb.Entity<SecApp>().Property(x => x.Name).HasColumnName("CAPP_NAME");
         mb.Entity<SecApp>().Property(x => x.Description).HasColumnName("CAPP_DESCRIPTION");

         /************************************************
          * SecUser
          ************************************************/

         mb.Entity<SecUser>()
            .ToTable("CARAVAN_SEC_USER", DataAccess.Configuration.Instance.OracleRunner)
            .HasKey(x => new {x.Id, x.AppId});

         mb.Entity<SecUser>().Property(x => x.Id).HasColumnName("CUSR_ID");
         mb.Entity<SecUser>().Property(x => x.AppId).HasColumnName("CAPP_ID");
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
         
      }
   }
}
