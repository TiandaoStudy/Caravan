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

namespace Finsa.Caravan.DataAccess.Sql.SqlServerCe
{
    /// <summary>
    ///   Dipendenze necessarie per il driver SqlServerCe.
    /// </summary>
    public sealed class CaravanSqlServerCeDataAccessNinjectConfig : CaravanSqlDataAccessNinjectConfig
    {
        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        public CaravanSqlServerCeDataAccessNinjectConfig(DependencyHandling dependencyHandling)
            : base(dependencyHandling, CaravanDataSourceKind.SqlServerCe)
        {
        }

        /// <summary>
        ///   Configura i servizi di Caravan.DataAccess per l'accesso a SqlServerCe. In questo momento
        ///   essi sono configurati nel seguente modo:
        /// 
        ///   * <see cref="ICaravanDataSourceManager"/> via <see cref="SqlServerCeDataSourceManager"/>.
        /// </summary>
        public override void Load()
        {
            switch (DependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    Bind<ICaravanDataSourceManager>().To<SqlServerCeDataSourceManager>().InSingletonScope();
                    break;

                case DependencyHandling.UnitTesting:
                    throw new InvalidOperationException(ErrorMessages.Drivers_DriverNotForUnitTesting);
            }

            // Carica le dipendenze dei moduli precedenti.
            base.Load();
        }
    }
}