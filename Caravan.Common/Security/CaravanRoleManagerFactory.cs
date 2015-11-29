﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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

using PommaLabs.Thrower;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Gestisce la creazione di RoleManager specifici per un dato applicativo Caravan.
    /// </summary>
    sealed class CaravanRoleManagerFactory : ICaravanRoleManagerFactory
    {
        public CaravanRoleManagerFactory(ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            SecurityRepository = securityRepository;
        }

        public ICaravanSecurityRepository SecurityRepository { get; }

        public Task<CaravanRoleManager> CreateAsync() => CreateAsync(CaravanCommonConfiguration.Instance.AppName);

        public Task<CaravanRoleManager> CreateAsync(string appName)
        {
            var roleStore = new CaravanRoleStore(appName, SecurityRepository);
            var roleManager = new CaravanRoleManager(roleStore);
            return Task.FromResult(roleManager);
        }
    }
}
