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

using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Identity;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess.Sql.Identity.Services;
using Finsa.Caravan.DataAccess.Sql.Identity.Stores;
using Finsa.CodeServices.Clock;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Ninject;

namespace Finsa.Caravan.DataAccess.Sql.Identity
{
    /// <summary>
    ///   Configura IdentityServer in modo da usare il driver SQL per l'accesso ai dati.
    /// </summary>
    public sealed class SqlIdentityServerServiceFactory : IdentityServerServiceFactory
    {
        /// <summary>
        ///   Configura IdentityServer in modo da usare il driver SQL per l'accesso ai dati.
        /// </summary>
        public SqlIdentityServerServiceFactory()
        {
            UserService = new Registration<IUserService, CaravanUserService>();

            // Operational stores
            AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, SqlIdnAuthorizationCodeStore>();
            TokenHandleStore = new Registration<ITokenHandleStore, SqlIdnTokenHandleStore>();
            ConsentStore = new Registration<IConsentStore, SqlIdnConsentStore>();
            RefreshTokenStore = new Registration<IRefreshTokenStore, SqlIdnRefreshTokenStore>();

            // Client configuration
            ClientStore = new Registration<IClientStore, SqlIdnClientStore>();

            // Scope configuration
            ScopeStore = new Registration<IScopeStore, SqlIdnScopeStore>();

            // CORS handling
            CorsPolicyService = new Registration<ICorsPolicyService, SqlIdnCorsPolicyService>();

            // Further services registrations...
            Register(new Registration<SqlDbContext>(r => CaravanServiceProvider.NinjectKernel.Get<SqlDbContext>()));
            Register(new Registration<ICaravanClientStore, SqlIdnClientStore>());
            Register(new Registration<IClock>(r => CaravanServiceProvider.NinjectKernel.Get<IClock>()));
            Register(new Registration<ICaravanUserManagerFactory>(r => CaravanServiceProvider.NinjectKernel.Get<ICaravanUserManagerFactory>()));
        }
    }
}
