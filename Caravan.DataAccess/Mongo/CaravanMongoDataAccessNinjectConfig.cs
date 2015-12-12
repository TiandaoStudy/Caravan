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
using Ninject.Web.Common;

namespace Finsa.Caravan.DataAccess.Mongo
{
    /// <summary>
    ///   Dipendenze condivise da tutti i driver Mongo.
    /// </summary>
    public abstract class CaravanMongoDataAccessNinjectConfig : CaravanDataAccessNinjectConfig
    {
        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        /// <param name="dataSourceKind">
        ///   Il tipo della sorgente dati che verrà usato dalla componente di accesso ai dati.
        /// </param>
        protected CaravanMongoDataAccessNinjectConfig(DependencyHandling dependencyHandling, CaravanDataSourceKind dataSourceKind)
            : base(dependencyHandling, dataSourceKind)
        {
        }

        /// <summary>
        ///   Configura i servizi di Caravan.DataAccess per il driver Mongo. In questo momento essi
        ///   sono configurati nel seguente modo:
        /// 
        ///   * <see cref="ICaravanLogRepository"/> via <see cref="MongoLogRepository"/>.
        ///   * <see cref="ICaravanSecurityRepository"/> via <see cref="MongoSecurityRepository"/>.
        /// </summary>
        public override void Load()
        {
            // Carica le dipendenze dei moduli precedenti.
            base.Load();

            switch (DependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    // Gestione della connessione alla sorgente dati.
                    Bind<ICaravanDataSourceManager>().To<MongoDataSourceManager>().InSingletonScope();

                    // Gestione dei repository base di Caravan.
                    Bind<ICaravanLogRepository>().To<MongoLogRepository>().InRequestScope();
                    Bind<ICaravanSecurityRepository>().To<MongoSecurityRepository>().InRequestScope();

                    break;

                case DependencyHandling.UnitTesting:
                    // Valgono le dipendenze definite nel modulo base.
                    break;
            }
        }
    }
}