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

using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Core;
using System;

namespace Finsa.Caravan.DataAccess.Sql.SqlServer
{
    /// <summary>
    ///   Dipendenze necessarie per il driver SqlServer.
    /// </summary>
    public sealed class CaravanSqlServerDataAccessNinjectConfig : CaravanSqlDataAccessNinjectConfig
    {
        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        public CaravanSqlServerDataAccessNinjectConfig(DependencyHandling dependencyHandling)
            : base(dependencyHandling, CaravanDataSourceKind.SqlServer)
        {
        }

        /// <summary>
        ///   Configura i servizi di Caravan.DataAccess per l'accesso a SqlServer. In questo momento
        ///   essi sono configurati nel seguente modo:
        /// 
        ///   * <see cref="ICaravanDataSourceManager"/> via <see cref="SqlServerDataSourceManager"/>.
        /// </summary>
        public override void Load()
        {
            switch (DependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    Bind<ICaravanDataSourceManager>().To<SqlServerDataSourceManager>().InSingletonScope();
                    break;

                case DependencyHandling.UnitTesting:
                    throw new InvalidOperationException(ErrorMessages.Drivers_DriverNotForUnitTesting);
            }

            // Carica le dipendenze dei moduli precedenti.
            base.Load();
        }
    }
}