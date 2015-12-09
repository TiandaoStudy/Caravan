using Finsa.Caravan.DataAccess.Sql.FakeSql;
using Finsa.Caravan.DataAccess.Sql.Identity.Entities;
using Finsa.Caravan.DataAccess.Sql.Logging.Entities;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Finsa.Caravan.DataAccess.Sql
{
    /// <summary>
    ///   Contesto DB usato dalla parte di accesso ai dati di Caravan.
    /// </summary>
    public sealed class SqlDbContext : DbContext
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
            // Disabilito SEMPRE il lazy loading, è troppo pericoloso!
            Configuration.LazyLoadingEnabled = false;
        }

        public static SqlDbContext CreateReadContext()
        {
            // Il DB è già inizializzato dalla chiamata sottostante.
            var ctx = CreateUpdateContext();

            // Disabilito i proxy, dato che per un contesto di lettura (NON UPDATE) non hanno alcuna utilità.
            ctx.Configuration.ProxyCreationEnabled = false;

            return ctx;
        }

        public static SqlDbContext CreateUpdateContext()
        {
            var ctx = new SqlDbContext();

            // Provo a inizializzare il DB.
            ctx.Database.Initialize(false);

            return ctx;
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

        /// <summary>
        ///   Da usare SOLO E SOLTANTO negli unit test, resetta la connessione di Effort.
        /// </summary>
        public static void Reset()
        {
            // A new connection is created and persisted for the whole test duration.
            (CaravanDataSource.Manager as FakeSqlDataSourceManager).ResetConnection();

            // The database is recreated, since it is in-memory and probably it does not exist.
            using (var ctx = SqlDbContext.CreateUpdateContext())
            {
                ctx.Database.CreateIfNotExists();
                Database.SetInitializer(new DropCreateDatabaseAlways<SqlDbContext>());
                ctx.Database.Initialize(true);
                Database.SetInitializer(new CreateDatabaseIfNotExists<SqlDbContext>());
            }
        }

        #region DB Sets - Logging

        public DbSet<SqlLogEntry> LogEntries { get; set; }

        public DbSet<SqlLogSetting> LogSettings { get; set; }

        #endregion DB Sets - Logging

        #region DB Sets - Security

        public DbSet<SqlSecApp> SecApps { get; set; }

        public DbSet<SqlSecClaim> SecClaims { get; set; }

        public DbSet<SqlSecContext> SecContexts { get; set; }

        public DbSet<SqlSecEntry> SecEntries { get; set; }

        public DbSet<SqlSecGroup> SecGroups { get; set; }

        public DbSet<SqlSecObject> SecObjects { get; set; }

        public DbSet<SqlSecRole> SecRoles { get; set; }

        public DbSet<SqlSecUser> SecUsers { get; set; }

        #endregion DB Sets - Security

        #region DB Sets - Identity

        public DbSet<SqlIdnClient> IdnClients { get; set; }

        public DbSet<SqlIdnClientClaim> IdnClientClaims { get; set; }

        public DbSet<SqlIdnClientCorsOrigin> IdnClientCorsOrigins { get; set; }

        public DbSet<SqlIdnClientCustomGrantType> IdnClientCustomGrantTypes { get; set; }

        public DbSet<SqlIdnClientIdPRestriction> IdnClientIdPRestrictions { get; set; }

        public DbSet<SqlIdnClientPostLogoutRedirectUri> IdnClientPostLogoutRedirectUris { get; set; }

        public DbSet<SqlIdnClientRedirectUri> IdnClientRedirectUris { get; set; }

        public DbSet<SqlIdnClientScope> IdnClientScopes { get; set; }

        public DbSet<SqlIdnClientSecret> IdnClientSecrets { get; set; }

        public DbSet<SqlIdnConsent> IdnConsents { get; set; }

        public DbSet<SqlIdnScope> IdnScopes { get; set; }

        public DbSet<SqlIdnScopeClaim> IdnScopeClaims { get; set; }

        public DbSet<SqlIdnScopeSecret> IdnScopeSecrets { get; set; }

        public DbSet<SqlIdnToken> IdnTokens { get; set; }

        #endregion DB Sets - Identity

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Conventions.Remove(new PluralizingTableNameConvention());
            mb.HasDefaultSchema(CaravanDataAccessConfiguration.Instance.SqlSchema);

            base.OnModelCreating(mb);

            #region Configurations - Logging

            mb.Configurations.Add(new SqlLogSettingTypeConfiguration());
            mb.Configurations.Add(new SqlLogEntryTypeConfiguration());

            #endregion Configurations - Logging

            #region Configurations - Security

            mb.Configurations.Add(new SqlSecAppTypeConfiguration());
            mb.Configurations.Add(new SqlSecClaimTypeConfiguration());
            mb.Configurations.Add(new SqlSecContextTypeConfiguration());
            mb.Configurations.Add(new SqlSecEntryTypeConfiguration());
            mb.Configurations.Add(new SqlSecGroupTypeConfiguration());
            mb.Configurations.Add(new SqlSecObjectTypeConfiguration());
            mb.Configurations.Add(new SqlSecRoleTypeConfiguration());
            mb.Configurations.Add(new SqlSecUserTypeConfiguration());

            #endregion Configurations - Security

            #region Configuration - Identity

            mb.Configurations.Add(new SqlIdnClientTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientClaimTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientCorsOriginTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientCustomGrantTypeTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientIdPRestrictionTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientPostLogoutRedirectUriTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientRedirectUriTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientScopeTypeConfiguration());
            mb.Configurations.Add(new SqlIdnClientSecretTypeConfiguration());
            mb.Configurations.Add(new SqlIdnConsentTypeConfiguration());
            mb.Configurations.Add(new SqlIdnScopeTypeConfiguration());
            mb.Configurations.Add(new SqlIdnScopeClaimTypeConfiguration());
            mb.Configurations.Add(new SqlIdnScopeSecretTypeConfiguration());
            mb.Configurations.Add(new SqlIdnTokenTypeConfiguration());

            #endregion Configuration - Identity
        }
    }
}
