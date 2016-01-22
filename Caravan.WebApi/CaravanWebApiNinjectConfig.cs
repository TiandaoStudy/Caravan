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
using Finsa.Caravan.Common.Core;
using Finsa.Caravan.WebApi.Identity;
using IdentityManager;
using Ninject.Modules;
using PommaLabs.Thrower;
using System;

namespace Finsa.Caravan.WebApi
{
    /// <summary>
    ///   Modulo che dichiara le dipendenze di Caravan.Common.
    /// </summary>
    public sealed class CaravanWebApiNinjectConfig : NinjectModule
    {
        private readonly DependencyHandling _dependencyHandling;

        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        public CaravanWebApiNinjectConfig(DependencyHandling dependencyHandling)
        {
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(DependencyHandling), dependencyHandling), ErrorMessages.InvalidEnumValue, nameof(dependencyHandling));
            _dependencyHandling = dependencyHandling;
        }

        /// <summary>
        ///   Configura le dipendenze di Caravan.Common. In questo momento esse sono:
        /// 
        ///   * <see cref="IAccessTokenExtractor"/> via <see cref="BearerAccessTokenExtractor"/>,
        ///     indipendentemente dall'ambiente di esecuzione.
        ///   * <see cref="IAuthorizationErrorHandler"/> via
        ///     <see cref="SimpleAuthorizationErrorHandler"/>, indipendentemente dall'ambiente di esecuzione.
        /// </summary>
        public override void Load()
        {
            switch (_dependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    // Necessarie per la GUI della gestione utenti e per OAuth2. Non richieste per
                    // gli unit test.
                    Bind<IdentityServer3.Core.Configuration.IdentityServerServiceFactory>().To<IdentityServerServiceFactory>().InSingletonScope();
                    Bind<IdentityManager.Configuration.IdentityManagerServiceFactory>().To<IdentityManagerServiceFactory>().InSingletonScope();
                    Bind<IIdentityManagerService>().To<IdentityManagerService>().InRequestOrThreadScope();
                    break;

                case DependencyHandling.UnitTesting:
                    break;
            }

            // Bind indipendenti dall'ambiente di esecuzione:
            Bind<IAccessTokenExtractor>().To<BearerAccessTokenExtractor>().InSingletonScope();
            Bind<IAuthorizationErrorHandler>().To<SimpleAuthorizationErrorHandler>().InSingletonScope();
        }
    }
}
