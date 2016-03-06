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
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Valida l'access token di OAuth2 con una richiesta verso l'apposito servizio.
    /// </summary>
    public interface IAccessTokenValidator
    {
        /// <summary>
        ///   Le impostazioni legate al server di OAuth2.
        /// </summary>
        OAuth2AuthorizationSettings AuthorizationSettings { get; }

        /// <summary>
        ///   Istanza del log per questo filtro.
        /// </summary>
        ILog Log { get; }

        /// <summary>
        ///   Inoltra l'access token al server di OAuth2 al fine di validarlo.
        /// </summary>
        /// <param name="actionContext">La richiesta HTTP.</param>
        /// <param name="accessToken">L'access token da validare.</param>
        /// <returns>
        ///   Vero se il token è stato validato, falso altrimenti. Il campo
        ///   <see cref="AuthorizationResult{TPayload}.Payload"/> deve essere valorizzato con le
        ///   informazioni sull'utente, se ricevute.
        /// </returns>
        Task<AuthorizationResult<dynamic>> ValidateAccessTokenAsync(HttpActionContext actionContext, string accessToken);
    }
}
