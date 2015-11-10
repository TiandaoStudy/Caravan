using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Sql.Identity;
using Finsa.CodeServices.Common.Portability;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Finsa.Caravan.WebService
{
    /// <summary>
    ///   Configurazione del servizio di autorizzazione/autenticazione.
    /// </summary>
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
                },
                new Client
                {
                    ClientName = "ASCESI Check",
                    Enabled = true,

                    ClientId = "ascesi_check",
                    ClientSecrets = new List<Secret> {new Secret("check".Sha256())},

                    Flow = Flows.AuthorizationCode,
                    RedirectUris = new List<string> { "http://localhost/ascesicheck", "http://localhost:1731" },

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,

                    AllowedScopes = new List<string> { "publicApi" }
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

                Factory = ConfigureFactory(),
            }));
        }

        /// <summary>
        ///   Carica il certificato necessario per firmare i TOKEN.
        /// </summary>
        /// <returns>Il certificato necessario per firmare i TOKEN.</returns>
        static X509Certificate2 LoadCertificate()
        {
            var certificatePath = CaravanWebServiceConfiguration.Instance.Identity_SigningCertificatePath;
            var mappedCertificatePath = PortableEnvironment.MapPath(certificatePath);
            var certificatePassword = CaravanWebServiceConfiguration.Instance.Identity_SigningCertificatePassword;
            return new X509Certificate2(mappedCertificatePath, certificatePassword);
        }

        /// <summary>
        ///   Configura il generatore dei servizi usati dalla parte di autenticazione/autorizzazione.
        /// </summary>
        /// <returns></returns>
        public static IdentityServerServiceFactory ConfigureFactory()
        {
            //var t = new AspNetIdentityUserService<SecUser, string>(new UserManager<SecUser, string>(new CaravanUserStore(CaravanDataSource.Security)));

            var factory = SqlIdnServiceFactory.Configure();
            factory.UserService = new Registration<IUserService, CaravanUserService>();
            return factory;
        }

        class CaravanUserService : IUserService
        {
            public Task PreAuthenticateAsync(PreAuthenticationContext context)
            {
                return Task.FromResult(0);
            }

            public Task AuthenticateLocalAsync(LocalAuthenticationContext context)
            {
                var logins = JsonConvert.DeserializeObject<UserPwd[]>(File.ReadAllText(PortableEnvironment.MapPath("~/TempUsers.json")));
                context.AuthenticateResult = logins.Any(l => l.user == context.UserName && l.pwd == context.Password)
                    ? new AuthenticateResult(context.UserName, context.SignInMessage?.ClientId ?? "caravan-admin-ui")
                    : new AuthenticateResult("You shall not pass!");
                return Task.FromResult(0);
            }

            private struct UserPwd
            {
                public string user { get; set; }
                public string pwd { get; set; }
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
