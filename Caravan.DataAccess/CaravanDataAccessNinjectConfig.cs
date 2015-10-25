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
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security;
using Ninject.Modules;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Dipendenze di Caravan.DataAccess.
    /// </summary>
    public sealed class CaravanDataAccessNinjectConfig : NinjectModule
    {
        readonly DependencyHandling _dependencyHandling;

        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        public CaravanDataAccessNinjectConfig(DependencyHandling dependencyHandling)
        {
            _dependencyHandling = dependencyHandling;
        }

        /// <summary>
        ///   Configura i servizi di Caravan.DataAccess. In questo momento esse sono:
        /// 
        ///   * <see cref="ICaravanLogRepository"/> via <see cref="CaravanDataSource.Logger"/>.
        ///   * <see cref="ICaravanSecurityRepository"/> via <see cref="CaravanDataSource.Security"/>.
        /// </summary>
        public override void Load()
        {
            switch (_dependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    Bind<ICaravanLogRepository>().ToMethod(ctx => CaravanDataSource.Logger).InSingletonScope();
                    Bind<ICaravanSecurityRepository>().ToMethod(ctx => CaravanDataSource.Security).InSingletonScope();
                    break;

                case DependencyHandling.UnitTesting:
                    // TODO Migliorare questa gestione...
                    Bind<ICaravanLogRepository>().ToMethod(ctx => CaravanDataSource.Logger).InSingletonScope();
                    Bind<ICaravanSecurityRepository>().ToMethod(ctx => CaravanDataSource.Security).InSingletonScope();
                    break;
            }            
        }
    }
}
