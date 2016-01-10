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

using Finsa.Caravan.Common.Security.Models;
using PommaLabs.Thrower;
using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace Finsa.Caravan.Common.Identity.Models
{
    /// <summary>
    ///   Oggetto "principal" personalizzato per Caravan.
    /// </summary>
    public sealed class IdnPrincipal : IPrincipal
    {
        /// <summary>
        ///   Costruisce un "principal" partendo da un utente di Caravan.
        /// </summary>
        /// <param name="user">L'utente Caravan.</param>
        public IdnPrincipal(SecUser user)
        {
            // Preconditions
            RaiseArgumentNullException.IfIsNull(user, nameof(user));

            User = user;
            Identity = new IdnIdentity(user.AppName, user.Login);
        }

        /// <summary>
        ///   Costruisce un "principal" partendo dal nome dell'applicazione e dalla login
        ///   dell'utente. Questo costruttore può essere usato quando non si ha a disposizione un
        ///   vero e proprio utente Caravan.
        /// </summary>
        /// <param name="appName">Il nome dell'applicazione Caravan, può essere fittizio.</param>
        /// <param name="userLogin">La login dell'utente Caravan, può essere fittizia.</param>
        public IdnPrincipal(string appName, string userLogin)
        {
            // Preconditions
            RaiseArgumentException.IfIsNullOrWhiteSpace(appName, nameof(appName));
            RaiseArgumentException.IfIsNullOrWhiteSpace(userLogin, nameof(userLogin));

            User = new SecUser { AppName = appName, Login = userLogin };
            Identity = new IdnIdentity(appName, userLogin);
        }

        /// <summary>
        ///   L'utente Caravan collegato in questo momento.
        /// </summary>
        public static IdnPrincipal Current => Thread.CurrentPrincipal as IdnPrincipal;

        /// <summary>
        ///   L'utente Caravan.
        /// </summary>
        public SecUser User { get; }

        /// <summary>
        ///   L'identità dell'utente.
        /// </summary>
        public IIdentity Identity { get; }

        /// <summary>
        ///   Verifica se il profilo associato a questo "principal" appartiene al ruolo dato.
        /// </summary>
        /// <param name="role">Il ruolo da verificare.</param>
        /// <returns>Vero se l'utente appartiene al ruolo dato, falso altrimenti.</returns>
        public bool IsInRole(string role) => User.Roles.Any(r => role == SecRole.ToIdentityRoleName(r.GroupName, r.Name));
    }
}
