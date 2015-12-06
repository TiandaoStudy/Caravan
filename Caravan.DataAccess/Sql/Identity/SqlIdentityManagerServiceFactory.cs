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
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Security.Models;
using IdentityManager;
using IdentityManager.AspNetIdentity;
using IdentityManager.Configuration;
using Ninject;

namespace Finsa.Caravan.DataAccess.Sql.Identity
{
    /// <summary>
    ///   Configura IdentityManager in modo da usare il driver SQL per l'accesso ai dati.
    /// </summary>
    public sealed class SqlIdentityManagerServiceFactory : IdentityManagerServiceFactory
    {
        /// <summary>
        ///   Configura IdentityManager in modo da usare il driver SQL per l'accesso ai dati.
        /// </summary>
        public SqlIdentityManagerServiceFactory()
        {
            IdentityManagerService = new Registration<IIdentityManagerService, SqlIdentityManagerService>();

            Register(new Registration<CaravanUserManager>(x => CaravanServiceProvider.NinjectKernel.Get<ICaravanUserManagerFactory>().CreateAsync().Result));
            Register(new Registration<CaravanRoleManager>(x => CaravanServiceProvider.NinjectKernel.Get<ICaravanRoleManagerFactory>().CreateAsync().Result));

            // Further services registrations...
            Register(new Registration<SqlDbContext>(r => SqlDbContext.CreateUpdateContext()));
        }

        private sealed class SqlIdentityManagerService : AspNetIdentityManagerService<SecUser, long, SecRole, int>
        {
            public SqlIdentityManagerService(CaravanUserManager userManager, CaravanRoleManager roleManager)
                : base(userManager, roleManager)
            {
            }
        }
    }
}
