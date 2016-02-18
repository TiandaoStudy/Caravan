// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using EntityFramework.Triggers;
using Finsa.Caravan.DataAccess.Sql.Identity.Entities;
using Finsa.Caravan.DataAccess.Sql.Logging.Entities;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Sql
{
    /// <summary>
    ///   Contesto DB da usare come base per i contesti degli applicativi.
    /// </summary>
    public abstract class UnitTestableDbContext<TContext> : DbContextWithTriggers
        where TContext : UnitTestableDbContext<TContext>
    {
        /// <summary>
        ///   Costruisce il contesto di base per gli applicativi Caravan.
        /// </summary>
        /// <param name="dbConnection">Una connessione al DB.</param>
        public UnitTestableDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
            // Il lazy loading viene disabilitato dalla classe che si occupa di generare i contesti.
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

        public IQueryable<SqlSecUser> SecUsersWithGroupsAndRoles => SecUsers.Include("Roles.Group");

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

        /// <summary>
        ///   This method is called when the model for a derived context has been initialized, but
        ///   before the model has been locked down and used to initialize the context. The default
        ///   implementation of this method does nothing, but it can be overridden in a derived
        ///   class such that the model can be further configured before it is locked down.
        /// </summary>
        /// <remarks>
        ///   Typically, this method is called only once when the first instance of a derived
        ///   context is created. The model for that context is then cached and is for all further
        ///   instances of the context in the app domain. This caching can be disabled by setting
        ///   the ModelCaching property on the given ModelBuidler, but note that this can seriously
        ///   degrade performance. More control over caching is provided through use of the
        ///   DbModelBuilder and DbContextFactory classes directly.
        /// </remarks>
        /// <param name="modelBuilder">
        ///   The builder that defines the model for the context being created.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove(new PluralizingTableNameConvention());
            modelBuilder.HasDefaultSchema(CaravanDataAccessConfiguration.Instance.SqlSchema);

            base.OnModelCreating(modelBuilder);

            #region Configurations - Logging

            modelBuilder.Configurations.Add(new SqlLogSettingTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlLogEntryTypeConfiguration());

            #endregion Configurations - Logging

            #region Configurations - Security

            modelBuilder.Configurations.Add(new SqlSecAppTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlSecClaimTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlSecContextTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlSecEntryTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlSecGroupTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlSecObjectTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlSecRoleTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlSecUserTypeConfiguration());

            #endregion Configurations - Security

            #region Configurations - Identity

            modelBuilder.Configurations.Add(new SqlIdnClientTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientClaimTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientCorsOriginTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientCustomGrantTypeTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientIdPRestrictionTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientPostLogoutRedirectUriTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientRedirectUriTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientScopeTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnClientSecretTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnConsentTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnScopeTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnScopeClaimTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnScopeSecretTypeConfiguration());
            modelBuilder.Configurations.Add(new SqlIdnTokenTypeConfiguration());

            #endregion Configurations - Identity
        }
    }
}
