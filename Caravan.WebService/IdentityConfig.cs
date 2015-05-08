using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess;
using Finsa.CodeServices.Common.Portability;
using Microsoft.AspNet.Identity;
using Owin;
using Thinktecture.IdentityServer.AspNetIdentity;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace Finsa.Caravan.WebService
{
    public static class IdentityConfig
    {
        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientName = "Caravan.WebService.Demo",
                    Enabled = true,

                    ClientId = "CaravanWebServiceDemo",
                    ClientSecrets = new List<ClientSecret> {new ClientSecret("CaravanSecret".Sha256())},

                    Flow = Flows.ResourceOwner,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600
                }
            };
        }

        public static IEnumerable<Scope> Scopes()
        {
            var scopes = new List<Scope>
            {
                new Scope
                {
                    Enabled = true,
                    Name = "publicApi",
                    Description = "Access to our public API",
                    Type = ScopeType.Resource
                }
            };

            scopes.AddRange(StandardScopes.All);

            return scopes;
        }

        public static IdentityServerServiceFactory Configure(string connString)
        {
            var factory = new IdentityServerServiceFactory
            {
                UserService = new Registration<IUserService>(resolver => new AspNetIdentityUserService<SecUser, string>(new UserManager<SecUser, string>(new CaravanUserStore(Db.Security))))
            };

            var scopeStore = new InMemoryScopeStore(Scopes());
            factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStore);

            var clientStore = new InMemoryClientStore(Clients());
            factory.ClientStore = new Registration<IClientStore>(resolver => clientStore);

            return factory;
        }

        public static void Build(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp => idsrvApp.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Identity Server",
                IssuerUri = "https://idsrv3/mixit",
                SigningCertificate = LoadCertificate(),
                EnableWelcomePage = true,

                Factory = Configure("MyIdentityDb"),

                CorsPolicy = CorsPolicy.AllowAll
            }));
        }

        private static X509Certificate2 LoadCertificate()
        {
            var certificatePath = PortableEnvironment.MapPath("~/CaravanDevCert.pfx");
            return new X509Certificate2(certificatePath, "FinsaPassword");
        }

        private class CaravanUserService : IUserService
        {
            public Task<AuthenticateResult> PreAuthenticateAsync(SignInMessage message)
            {
                throw new NotImplementedException();
            }

            public Task<AuthenticateResult> AuthenticateLocalAsync(string username, string password, SignInMessage message)
            {
                throw new NotImplementedException();
            }

            public Task<AuthenticateResult> AuthenticateExternalAsync(ExternalIdentity externalUser, SignInMessage message)
            {
                throw new NotImplementedException();
            }

            public Task SignOutAsync(ClaimsPrincipal subject)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Claim>> GetProfileDataAsync(ClaimsPrincipal subject, IEnumerable<string> requestedClaimTypes = null)
            {
                throw new NotImplementedException();
            }

            public Task<bool> IsActiveAsync(ClaimsPrincipal subject)
            {
                throw new NotImplementedException();
            }
        }
    }
}