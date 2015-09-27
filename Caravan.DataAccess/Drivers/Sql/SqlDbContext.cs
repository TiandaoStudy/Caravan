using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Transactions;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

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
            switch (CaravanDataAccessConfiguration.Instance.SqlInitializer)
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
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Snapshot });
        }

        private static DbConnection GetConnection()
        {
            if (CaravanDataSource.Manager.DataSourceKind == CaravanDataSourceKind.FakeSql)
            {
                // Needed, otherwise Unit Tests fail.
                return CaravanDataSource.Manager.OpenConnection();
            }
            return CaravanDataSource.Manager.CreateConnection();
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
            mb.HasDefaultSchema(CaravanDataAccessConfiguration.Instance.SqlSchema);

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
}
