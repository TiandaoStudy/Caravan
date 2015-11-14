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

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   UserStore personalizzato per Caravan.
    /// </summary>
    /// <remarks>
    ///   Le seguenti interfacce sono state volutamente NON implementate:
    ///   * <see cref="IUserLoginStore{TUser,TKey}"/>
    /// </remarks>
    public interface ICaravanUserStore : IQueryableUserStore<SecUser, long>,
        IUserEmailStore<SecUser, long>,
        IUserPhoneNumberStore<SecUser, long>,
        IUserPasswordStore<SecUser, long>,
        IUserClaimStore<SecUser, long>,
        IUserLockoutStore<SecUser, long>,
        IUserRoleStore<SecUser, long>,
        IUserSecurityStampStore<SecUser, long>,
        IUserTwoFactorStore<SecUser, long>
    {
        /// <summary>
        ///   Il nome dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        string AppName { get; }

        /// <summary>
        ///   Il repository della sicurezza di Caravan.
        /// </summary>
        ICaravanSecurityRepository SecurityRepository { get; }
    }
}
