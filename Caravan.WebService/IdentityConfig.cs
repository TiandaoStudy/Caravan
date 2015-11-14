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

using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Sql.Identity;
using Finsa.CodeServices.Common.Portability;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Ninject;
using Owin;
using System.Security.Cryptography.X509Certificates;

namespace Finsa.Caravan.WebService
{
    /// <summary>
    ///   Configurazione del servizio di autorizzazione/autenticazione.
    /// </summary>
    public static class IdentityConfig
    {
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

                // Gestione estetica del servizio.
                EnableWelcomePage = true,

                // Gestione della sorgente dati per gli utenti.
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
        /// <returns>La factory per IdentityServer.</returns>
        public static IdentityServerServiceFactory ConfigureFactory()
        {
            var factory = SqlIdnServiceFactory.Configure();
            var caravanUserManager = CaravanServiceProvider.NinjectKernel.Get<CaravanUserManager>();
            factory.UserService = new Registration<IUserService>(new AspNetIdentityUserService<SecUser, long>(caravanUserManager));
            return factory;
        }
    }
}
