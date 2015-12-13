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
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   RoleStore personalizzato per Caravan.
    /// </summary>
    public interface ICaravanRoleStore : IQueryableRoleStore<SecRole, int>
    {
        /// <summary>
        ///   L'identificativo dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        long AppId { get; }

        /// <summary>
        ///   Il nome dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        string AppName { get; }

        /// <summary>
        ///   Stringa da usare come segnaposto quando si vogliono indicare tutti i ruoli.
        /// </summary>
        string AllRolesPlaceholder { get; }

        /// <summary>
        ///   Il repository della sicurezza di Caravan.
        /// </summary>
        ICaravanSecurityRepository SecurityRepository { get; }

        /// <summary>
        ///   Finds a role by Caravan group name and role name.
        /// </summary>
        /// <param name="groupName">Il nome del gruppo su Caravan.</param>
        /// <param name="roleName">Il nome del ruolo su Caravan.</param>
        /// <returns>Il ruolo corrispondente al gruppo e al nome dati.</returns>
        Task<SecRole> FindByNameAsync(string groupName, string roleName);

        /// <summary>
        ///   Trova tutti gli utenti che hanno un determinato ruolo e gruppo.
        /// 
        ///   Se al posto del ruolo si inserisce <see cref="AllRolesPlaceholder"/>, vengono
        ///   restituiti tutti gli utenti che appartengono al gruppo, indipendentemente dal ruolo svolto.
        /// </summary>
        /// <param name="identityRoleName">
        ///   Il nome del ruolo su identity, composto nel formato "gruppo/ruolo". Se si passa come
        ///   parametro "gruppo/ <see cref="AllRolesPlaceholder"/>", verranno presi tutti gli utenti
        ///   che appartengono al gruppo dato.
        /// </param>
        /// <returns>Tutti gli utenti che appartengono ad un determinato ruolo e gruppo.</returns>
        Task<IQueryable<SecUser>> FindUsersInRoleAsync(string identityRoleName);

        /// <summary>
        ///   Trova tutti gli utenti che hanno un determinato ruolo e gruppo.
        /// 
        ///   Se al posto del ruolo si inserisce <see cref="AllRolesPlaceholder"/>, vengono
        ///   restituiti tutti gli utenti che appartengono al gruppo, indipendentemente dal ruolo svolto.
        /// </summary>
        /// <param name="groupName">Il nome del gruppo su Caravan.</param>
        /// <param name="roleName">
        ///   Il nome del ruolo su Caravan, oppure <see cref="AllRolesPlaceholder"/> per prenderli tutti.
        /// </param>
        /// <returns>Tutti gli utenti che appartengono ad un determinato ruolo e gruppo.</returns>
        Task<IQueryable<SecUser>> FindUsersInRoleAsync(string groupName, string roleName);
    }
}
