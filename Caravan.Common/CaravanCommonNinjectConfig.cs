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
using Finsa.Caravan.Common.Logging;
using Finsa.CodeServices.Clock;
using Ninject.Modules;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Modulo che dichiara le dipendenze di Caravan.Common.
    /// </summary>
    public sealed class CaravanCommonNinjectConfig : NinjectModule
    {
        readonly DependencyHandling _dependencyHandling;

        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        public CaravanCommonNinjectConfig(DependencyHandling dependencyHandling)
        {
            _dependencyHandling = dependencyHandling;
        }

        /// <summary>
        ///   Configura le dipendenze di Caravan.Common. In questo momento esse sono:
        /// 
        ///   * <see cref="IClock"/> via <see cref="NtpClock"/>, oppure <see cref="MockClock"/> per
        ///     gli unit test.
        ///   * <see cref="ILog"/> via Caravan LOG, oppure <see cref="CaravanNoOpLogger"/> per gli
        ///     unit test.
        ///   * <see cref="ICaravanLog"/> via Caravan LOG, oppure <see cref="CaravanNoOpLogger"/>
        ///     per gli unit test.
        /// </summary>
        public override void Load()
        {
            switch (_dependencyHandling)
            {
                case DependencyHandling.Default:
                    Bind<IClock>().To<NtpClock>();
                    Bind<ILog, ICaravanLog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.Target.Member.ReflectedType) as ICaravanLog);
                    break;

                case DependencyHandling.UnitTesting:
                    Bind<IClock>().To<MockClock>();
                    Bind<ILog, ICaravanLog>().To<CaravanNoOpLogger>();
                    break;
            }
        }
    }
}
