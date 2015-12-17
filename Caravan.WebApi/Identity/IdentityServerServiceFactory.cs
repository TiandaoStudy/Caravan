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

using Finsa.Caravan.WebApi.Core;
using IdentityServer3.Core.Services;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Configura IdentityServer in modo da usare il driver SQL per l'accesso ai dati.
    /// </summary>
    public sealed class IdentityServerServiceFactory : IdentityServer3.Core.Configuration.IdentityServerServiceFactory
    {
        /// <summary>
        ///   Configura IdentityServer in modo da usare il driver SQL per l'accesso ai dati.
        /// </summary>
        public IdentityServerServiceFactory()
        {
            UserService = new IdentityServerNinjectRegistration<IUserService>();

            // Operational stores
            AuthorizationCodeStore = new IdentityServerNinjectRegistration<IAuthorizationCodeStore>();
            TokenHandleStore = new IdentityServerNinjectRegistration<ITokenHandleStore>();
            ConsentStore = new IdentityServerNinjectRegistration<IConsentStore>();
            RefreshTokenStore = new IdentityServerNinjectRegistration<IRefreshTokenStore>();

            // Client configuration
            ClientStore = new IdentityServerNinjectRegistration<IClientStore>();

            // Scope configuration
            ScopeStore = new IdentityServerNinjectRegistration<IScopeStore>();

            // CORS handling
            CorsPolicyService = new IdentityServerNinjectRegistration<ICorsPolicyService>();
        }
    }
}
