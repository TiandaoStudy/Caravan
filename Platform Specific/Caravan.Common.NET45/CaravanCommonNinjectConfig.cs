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

using Common.Logging;
using Finsa.Caravan.Common.Core;
using Finsa.Caravan.Common.Identity;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security;
using Finsa.CodeServices.Clock;
using IdentityServer3.Core.Services;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using PommaLabs.Thrower;
using System;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Modulo che dichiara le dipendenze di Caravan.Common.
    /// </summary>
    public sealed class CaravanCommonNinjectConfig : NinjectModule
    {
        private readonly DependencyHandling _dependencyHandling;
        private readonly string _appName;

        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        /// <param name="appName">Il nome dell'applicativo su Caravan.</param>
        public CaravanCommonNinjectConfig(DependencyHandling dependencyHandling, string appName)
        {
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(DependencyHandling), dependencyHandling), ErrorMessages.InvalidEnumValue, nameof(dependencyHandling));
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
            _dependencyHandling = dependencyHandling;
            _appName = appName;
        }

        /// <summary>
        ///   Configura le dipendenze di Caravan.Common. In questo momento esse sono:
        /// 
        ///   * <see cref="IClock"/> via <see cref="SystemClock"/>, oppure <see cref="MockClock"/>
        ///     per gli unit test.
        ///   * <see cref="ILog"/> via Caravan LOG configurabile con NLog, oppure
        ///     <see cref="CaravanNoOpLogger"/> per gli unit test.
        ///   * <see cref="ICaravanLog"/> via Caravan LOG configurabile con NLog, oppure
        ///     <see cref="CaravanNoOpLogger"/> per gli unit test.
        ///   * <see cref="ICaravanUserStore"/> via <see cref="CaravanUserStore"/>,
        ///     indipendentemente dall'ambiente di esecuzione.
        ///   * <see cref="ICaravanRoleStore"/> via <see cref="CaravanRoleStore"/>,
        ///     indipendentemente dall'ambiente di esecuzione.
        ///   * <see cref="ICaravanUserManagerFactory"/> via
        ///     <see cref="CaravanUserManagerFactory"/>, indipendentemente dall'ambiente di esecuzione.
        ///   * <see cref="ICaravanRoleManagerFactory"/> via
        ///     <see cref="CaravanRoleManagerFactory"/>, indipendentemente dall'ambiente di esecuzione.
        ///   * <see cref="ICaravanVariablesContextIdentifier"/> via
        ///     <see cref="CaravanVariablesContextIdentifier"/>, indipendentemente dall'ambiente di esecuzione.
        /// </summary>
        public override void Load()
        {
            switch (_dependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    Bind<IClock>().To<SystemClock>().InSingletonScope();
                    Bind<ILog, ICaravanLog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request?.Target?.Member?.ReflectedType ?? typeof(CaravanServiceProvider)) as ICaravanLog);
                    break;

                case DependencyHandling.UnitTesting:
                    Bind<IClock>().To<MockClock>().InSingletonScope();
                    Bind<ILog, ICaravanLog>().To<CaravanNoOpLogger>().InSingletonScope();
                    break;
            }

            // Bind indipendenti dall'ambiente di esecuzione:
            Bind<ICaravanUserStore>().To<CaravanUserStore>().InRequestOrThreadScope().WithConstructorArgument("appName", _appName);
            Bind<ICaravanRoleStore>().To<CaravanRoleStore>().InRequestOrThreadScope().WithConstructorArgument("appName", _appName);

            Bind<ICaravanUserManagerFactory>().To<CaravanUserManagerFactory>().InRequestOrThreadScope();
            Bind<ICaravanRoleManagerFactory>().To<CaravanRoleManagerFactory>().InRequestOrThreadScope();

            Bind<CaravanUserManager>().ToMethod(ctx => ctx.Kernel.Get<ICaravanUserManagerFactory>().CreateAsync().Result).InRequestOrThreadScope();
            Bind<CaravanRoleManager>().ToMethod(ctx => ctx.Kernel.Get<ICaravanRoleManagerFactory>().CreateAsync().Result).InRequestOrThreadScope();

            Bind<ICaravanVariablesContextIdentifier>().To<CaravanVariablesContextIdentifier>().InSingletonScope();

            Bind<IUserService>().To<CaravanUserService>();
        }
    }
}
