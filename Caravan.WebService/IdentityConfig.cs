using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess;
using Finsa.CodeServices.Common.Portability;
using Finsa.CodeServices.Security.PasswordHashing;
using Microsoft.AspNet.Identity;
using Owin;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using Finsa.Caravan.Common.Security.Models;

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
                    ClientSecrets = new List<Secret> {new Secret("CaravanSecret".Sha256())},

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
            var t = new AspNetIdentityUserService<SecUser, string>(new UserManager<SecUser, string>(new CaravanUserStore(CaravanDataSource.Security)));

            var factory = new IdentityServerServiceFactory
            {
                UserService = new Registration<IUserService>(resolver => new AspNetIdentityUserService<SecUser, string>(new CaravanUserManager(CaravanDataSource.Security, new NoOpPasswordHasher())))
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
                
                //CorsPolicy = CorsPolicy.AllowAll
            }));
        }

        static X509Certificate2 LoadCertificate()
        {
            var certificatePath = PortableEnvironment.MapPath("~/CaravanDevCert.pfx");
            return new X509Certificate2(certificatePath, "FinsaPassword");
        }

        class CaravanUserService : IUserService
        {
            public Task PreAuthenticateAsync(PreAuthenticationContext context)
            {
                throw new NotImplementedException();
            }

            public Task AuthenticateLocalAsync(LocalAuthenticationContext context)
            {
                throw new NotImplementedException();
            }

            public Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
            {
                throw new NotImplementedException();
            }

            public Task PostAuthenticateAsync(PostAuthenticationContext context)
            {
                throw new NotImplementedException();
            }

            public Task SignOutAsync(SignOutContext context)
            {
                throw new NotImplementedException();
            }

            public Task GetProfileDataAsync(ProfileDataRequestContext context)
            {
                throw new NotImplementedException();
            }

            public Task IsActiveAsync(IsActiveContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}