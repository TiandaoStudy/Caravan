using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess;
using Finsa.CodeServices.Common.Portability;
using Finsa.CodeServices.Security.PasswordHashing;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.AspNet.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
            //var t = new AspNetIdentityUserService<SecUser, string>(new UserManager<SecUser, string>(new CaravanUserStore(CaravanDataSource.Security)));

            var factory = new IdentityServerServiceFactory
            {
                UserService = new Registration<IUserService>(resolver => new CaravanUserService())
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
                // Dettagli sul nome del servizio.
                SiteName = CaravanCommonConfiguration.Instance.AppDescription,
                IssuerUri = "https://idsrv3/mixit",

                // Gestione della sicurezza della comunicazione.
                SigningCertificate = LoadCertificate(),
                RequireSsl = true,

                EnableWelcomePage = true,

                Factory = Configure("MyIdentityDb"),

                //CorsPolicy = CorsPolicy.AllowAll
            }));
        }

        static X509Certificate2 LoadCertificate()
        {
            var certificatePath = CaravanWebServiceConfiguration.Instance.Identity_SigningCertificatePath;
            var mappedCertificatePath = PortableEnvironment.MapPath(certificatePath);
            var certificatePassword = CaravanWebServiceConfiguration.Instance.Identity_SigningCertificatePassword;
            return new X509Certificate2(mappedCertificatePath, certificatePassword);
        }

        class CaravanUserService : IUserService
        {
            public Task PreAuthenticateAsync(PreAuthenticationContext context)
            {
                return Task.FromResult(0);
            }

            public Task AuthenticateLocalAsync(LocalAuthenticationContext context)
            {
                context.AuthenticateResult = (context.UserName == context.Password)
                    ? new AuthenticateResult("a", "b")
                    : new AuthenticateResult("You shall not pass!");
                return Task.FromResult(0);
            }

            public Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
            {
                throw new NotImplementedException();
            }

            public Task PostAuthenticateAsync(PostAuthenticationContext context)
            {
                return Task.FromResult(0);
            }

            public Task SignOutAsync(SignOutContext context)
            {
                return Task.FromResult(0);
            }

            public Task GetProfileDataAsync(ProfileDataRequestContext context)
            {
                throw new NotImplementedException();
            }

            public Task IsActiveAsync(IsActiveContext context)
            {
                context.IsActive = true;
                return Task.FromResult(0);
            }
        }
    }
}
