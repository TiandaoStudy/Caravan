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
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Gestisce la conferma finale se un utente possa o meno accedere a un dato servizio.
    /// </summary>
    public interface IAuthorizationValidator
    {
        /// <summary>
        ///   Istanza del log per questo filtro.
        /// </summary>
        ILog Log { get; }

        /// <summary>
        ///   Valida definitivamente l'accesso da parte di un dato utente. Se ritorna vero, l'utente
        ///   può accedere al servizio; se ritorna falso, l'utente non può accedere ed eventuali indicazioni
        /// </summary>
        /// <param name="actionContext">Il contesto HTTP da validare.</param>
        /// <param name="userClaims">I claim restituiti dal servizio che gestisce l'identità.</param>
        /// <returns>
        ///   Vero se l'utente è stato autorizzato, falso altrimenti. Il campo
        ///   <see cref="AuthorizationResult{TPayload}.Payload"/> deve essere valorizzato con il
        ///   principal legato all'utente, se autorizzato.
        /// </returns>
        Task<AuthorizationResult<IPrincipal>> ValidateRequestAsync(HttpActionContext actionContext, dynamic userClaims);
    }
}
