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
                AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, SqlIdnAuthorizationCodeStore>((string)null),
                TokenHandleStore = new Registration<ITokenHandleStore, SqlIdnTokenHandleStore>((string)null),
                ConsentStore = new Registration<IConsentStore, SqlIdnConsentStore>((string)null),
                RefreshTokenStore = new Registration<IRefreshTokenStore, SqlIdnRefreshTokenStore>((string)null)
            };

            return factory;
        }
    }
}
