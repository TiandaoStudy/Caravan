using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Finsa.CodeServices.Common.Portability;
using Thinktecture.IdentityServer.Core.Models;

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
                    ClientSecrets = new List<ClientSecret> {new ClientSecret("CaravanSecret")},

                    Flow = Flows.ResourceOwner,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600
                }
            };
        }

        public static X509Certificate2 LoadCertificate()
        {
            var certificatePath = PortableEnvironment.MapPath("~/CaravanDevCert.pxf");
            return new X509Certificate2(certificatePath, "FinsaPassword");
        }
    }
}