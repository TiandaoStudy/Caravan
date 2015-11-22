using IdentityServer3.Core.Models;
using System.Web.Http;
using BrockAllen.IdentityReboot;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Servizi di utilità per la parte di gestione dell'identità.
    /// </summary>
    [RoutePrefix("identity")]
    public sealed class IdentityController : ApiController
    {
        /// <summary>
        ///   Calcola l'hash in SHA256 per il secret dato. Utile per popolare le tabelle dei secret.
        /// </summary>
        /// <param name="secret">Il secret di cui si deve calcolare l'hash in SHA256.</param>
        /// <returns>L'hash SHA256 del secret dato.</returns>
        [Route("utils/sha256")]
        public string GetSha256(string secret) => secret.Sha256();

        /// <summary>
        ///   Calcola l'hash in SHA512 per il secret dato. Utile per popolare le tabelle dei secret.
        /// </summary>
        /// <param name="secret">Il secret di cui si deve calcolare l'hash in SHA512.</param>
        /// <returns>L'hash SHA512 del secret dato.</returns>
        [Route("utils/sha512")]
        public string GetSha512(string secret) => secret.Sha512();

#if DEBUG

        [Route("utils/adaptivePwdHash")]
        public string GetAdaptivePasswordHash(string password) => new AdaptivePasswordHasher().HashPassword(password);

#endif
    }
}
