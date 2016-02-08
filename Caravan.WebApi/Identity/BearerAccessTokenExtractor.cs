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

using Common.Logging;
using Finsa.Caravan.WebApi.Identity.Models;
using PommaLabs.Thrower;
using System.Net.Http;
using System.Threading.Tasks;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Estrattore che recupera l'access token dall'header Authorization.
    /// </summary>
    public sealed class BearerAccessTokenExtractor : IAccessTokenExtractor
    {
        /// <summary>
        ///   Inietta la dipendenze necessarie.
        /// </summary>
        /// <param name="authorizationSettings">Le impostazioni legate al server di OAuth2.</param>
        /// <param name="log">Istanza del log per questo filtro.</param>
        public BearerAccessTokenExtractor(OAuth2AuthorizationSettings authorizationSettings, ILog log)
        {
            RaiseArgumentNullException.IfIsNull(authorizationSettings, nameof(authorizationSettings));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            AuthorizationSettings = authorizationSettings;
            Log = log;
        }

        /// <summary>
        ///   Le impostazioni legate al server di OAuth2.
        /// </summary>
        public OAuth2AuthorizationSettings AuthorizationSettings { get; }

        /// <summary>
        ///   Istanza del log per questo filtro.
        /// </summary>
        public ILog Log { get; }

        /// <summary>
        ///   Recupera l'access token dalla richiesta HTTP.
        /// </summary>
        /// <param name="request">La richiesta HTTP.</param>
        /// <param name="accessToken">L'access token, se presente.</param>
        /// <returns>Vero se l'access token era presente, falso altrimenti.</returns>
        public Task<bool> ExtractFromRequestAsync(HttpRequestMessage request, out string accessToken)
        {
            accessToken = request.Headers.Authorization?.Parameter;
            return Task.FromResult(!string.IsNullOrWhiteSpace(accessToken));
        }
    }
}
