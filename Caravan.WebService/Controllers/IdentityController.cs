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

using System.Web.Http;

#if DEBUG

using Finsa.CodeServices.Security.Hashing;
using Finsa.CodeServices.Security.PasswordHashing;

#endif

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Servizi di utilità per la parte di gestione dell'identità.
    /// </summary>
    [RoutePrefix("identity")]
    public sealed class IdentityController : ApiController
    {
        #region Hashing

#if DEBUG

        /// <summary>
        ///   Calcola l'hash in SHA256 per il secret dato. Utile per popolare le tabelle dei secret.
        /// </summary>
        /// <param name="secret">Il secret di cui si deve calcolare l'hash in SHA256.</param>
        /// <returns>L'hash SHA256 del secret dato.</returns>
        [Route("utils/sha256")]
        public string GetSHA256(string secret) => secret.ToSHA256String();

        /// <summary>
        ///   Calcola l'hash in SHA384 per il secret dato. Utile per popolare le tabelle dei secret.
        /// </summary>
        /// <param name="secret">Il secret di cui si deve calcolare l'hash in SHA384.</param>
        /// <returns>L'hash SHA384 del secret dato.</returns>
        [Route("utils/sha384")]
        public string GetSHA384(string secret) => secret.ToSHA384String();

        /// <summary>
        ///   Calcola l'hash in SHA512 per il secret dato. Utile per popolare le tabelle dei secret.
        /// </summary>
        /// <param name="secret">Il secret di cui si deve calcolare l'hash in SHA512.</param>
        /// <returns>L'hash SHA512 del secret dato.</returns>
        [Route("utils/sha512")]
        public string GetSHA512(string secret) => secret.ToSHA512String();

        /// <summary>
        ///   Calcola l'hash adattivo per il testo dato. Utile per provare come uscirebbero le password.
        /// </summary>
        /// <param name="text">Il testo da provare.</param>
        /// <returns>L'hash adattivo del testo dato.</returns>
        [Route("utils/adaptivePwdHash")]
        public string GetAdaptivePasswordHash(string text) => new AdaptivePBKDF2PasswordHasher().HashPassword(text);

#endif

        #endregion Hashing
    }
}
