using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Sql.Identity.Services;
using Finsa.Caravan.DataAccess.Sql.Identity.Stores;
using Finsa.CodeServices.Clock;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Ninject;

namespace Finsa.Caravan.DataAccess.Sql.Identity
{
    /// <summary>
    ///   Configura IdentityServer in modo da usare il driver SQL per l'accesso ai dati.
    /// </summary>
    public static class SqlIdnServiceFactory
    {
        /// <summary>
        ///   Configura IdentityServer in modo da usare il driver SQL per l'accesso ai dati.
        /// </summary>
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory
            {
                // Operational stores
                AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, SqlIdnAuthorizationCodeStore>(),
                TokenHandleStore = new Registration<ITokenHandleStore, SqlIdnTokenHandleStore>(),
                ConsentStore = new Registration<IConsentStore, SqlIdnConsentStore>(),
                RefreshTokenStore = new Registration<IRefreshTokenStore, SqlIdnRefreshTokenStore>(),

                // Client configuration
                ClientStore = new Registration<IClientStore, SqlIdnClientStore>(),

                // Scope configuration
                ScopeStore = new Registration<IScopeStore, SqlIdnScopeStore>(),

                // CORS handling
                CorsPolicyService = new Registration<ICorsPolicyService>(r => new SqlIdnClientConfigurationCorsPolicyService(r.Resolve<SqlDbContext>()))
            };

            // Further services registrations...
            factory.Register(new Registration<SqlDbContext>(r => SqlDbContext.CreateUpdateContext()));
            factory.Register(new Registration<IClock>(r => CaravanServiceProvider.NinjectKernel.Get<IClock>()));

            return factory;
        }
    }
}