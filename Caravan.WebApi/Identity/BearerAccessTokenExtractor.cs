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

using System.Net.Http;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Estrattore che recupera l'access token dall'header Authorization.
    /// </summary>
    public sealed class BearerAccessTokenExtractor : IAccessTokenExtractor
    {
        /// <summary>
        ///   Recupera l'access token dalla richiesta HTTP.
        /// </summary>
        /// <param name="request">La richiesta HTTP.</param>
        /// <param name="accessToken">L'access token, se presente.</param>
        /// <returns>Vero se l'access token era presente, falso altrimenti.</returns>
        public bool ExtractFromRequest(HttpRequestMessage request, out string accessToken)
        {
            accessToken = request.Headers.Authorization?.Parameter;
            return string.IsNullOrWhiteSpace(accessToken);
        }
    }
}
