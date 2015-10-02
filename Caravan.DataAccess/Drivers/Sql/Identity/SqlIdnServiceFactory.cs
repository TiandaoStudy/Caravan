using Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Services;
using Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Stores;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity
{
    public static class SqlIdnServiceFactory
    {
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory
            {
                AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, SqlIdnAuthorizationCodeStore>(),
                TokenHandleStore = new Registration<ITokenHandleStore, SqlIdnTokenHandleStore>(),
                ConsentStore = new Registration<IConsentStore, SqlIdnConsentStore>(),
                RefreshTokenStore = new Registration<IRefreshTokenStore, SqlIdnRefreshTokenStore>(),
                CorsPolicyService = new Registration<ICorsPolicyService>(r => new SqlIdnClientConfigurationCorsPolicyService(r.Resolve<SqlDbContext>()))
            };

            return factory;
        }
    }
}
