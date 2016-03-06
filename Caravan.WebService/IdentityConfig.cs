﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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
using Finsa.CodeServices.Common.Portability;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using Ninject;
using Owin;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Finsa.Caravan.WebService
{
    /// <summary>
    ///   Configurazione del servizio di autorizzazione/autenticazione.
    /// </summary>
    internal static class IdentityConfig
    {
        public static void Build(IAppBuilder app)
        {
            // RIMUOVERE APPENA POSSIBILE!!! Accetta certificati non validi...
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            app.Map("/identity", idsrv =>
            {
                var options = new IdentityServerOptions
                {
                    // Dettagli sul nome del servizio.
                    SiteName = CaravanCommonConfiguration.Instance.AppDescription,
                    IssuerUri = "https://wscaravan.finsa.it/identity",

                    // Gestione della sicurezza della comunicazione.
                    // ATTENZIONE: Il servizio di Caravan è pensato per scopi interni di sviluppo, le
                    //             impostazioni riportate di seguito vanno rivalutate in produzione.
                    SigningCertificate = LoadCertificate(),
                    RequireSsl = false,

                    // Gestione estetica del servizio.
                    EnableWelcomePage = true,

                    // Gestione della sorgente dati per gli utenti.
                    Factory = ServiceProvider.NinjectKernel.Get<IdentityServerServiceFactory>()
                };

                // Personalizzazione della parte di autenticazione.
                options.AuthenticationOptions.EnablePostSignOutAutoRedirect = true;
                options.AuthenticationOptions.RememberLastUsername = true;
                options.CspOptions.ImgSrc = "* data:";

                idsrv.UseIdentityServer(options);
            });

            app.Map("/identityManager", idmgr =>
            {
                var options = new IdentityManagerOptions
                {
                    // Gestione della sorgente dati per gli utenti.
                    Factory = ServiceProvider.NinjectKernel.Get<IdentityManagerServiceFactory>()
                };

                // Gestione della sicurezza della comunicazione.
                options.SecurityConfiguration.RequireSsl = false;

                idmgr.UseIdentityManager(options);
            });
        }

        /// <summary>
        ///   Carica il certificato necessario per firmare i TOKEN.
        /// </summary>
        /// <returns>Il certificato necessario per firmare i TOKEN.</returns>
        private static X509Certificate2 LoadCertificate()
        {
            var certificatePath = CaravanWebServiceConfiguration.Instance.Identity_SigningCertificatePath;
            var mappedCertificatePath = PortableEnvironment.MapPath(certificatePath);
            var certificatePassword = CaravanWebServiceConfiguration.Instance.Identity_SigningCertificatePassword;
            return new X509Certificate2(mappedCertificatePath, certificatePassword);
        }
    }
}
