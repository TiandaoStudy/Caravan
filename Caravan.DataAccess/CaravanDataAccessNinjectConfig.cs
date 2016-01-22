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
using Finsa.Caravan.DataAccess.Core;
using Ninject.Modules;
using PommaLabs.Thrower;
using System;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Dipendenze di Caravan.DataAccess.
    /// </summary>
    public abstract class CaravanDataAccessNinjectConfig : NinjectModule
    {
        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        /// <param name="dataSourceKind">
        ///   Il tipo della sorgente dati che verrà usato dalla componente di accesso ai dati.
        /// </param>
        protected CaravanDataAccessNinjectConfig(DependencyHandling dependencyHandling, CaravanDataSourceKind dataSourceKind)
        {
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(DependencyHandling), dependencyHandling), ErrorMessages.InvalidEnumValue, nameof(dependencyHandling));
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(CaravanDataSourceKind), dataSourceKind), ErrorMessages.InvalidEnumValue, nameof(dataSourceKind));
            DependencyHandling = dependencyHandling;
            DataSourceKind = dataSourceKind;
        }

        /// <summary>
        ///   La modalità di gestione delle dipendenze.
        /// </summary>
        protected DependencyHandling DependencyHandling { get; }

        /// <summary>
        ///   La tipologia di accesso alla sorgente dati di Caravan.
        /// </summary>
        protected CaravanDataSourceKind DataSourceKind { get; }

        /// <summary>
        ///   Configura i servizi di Caravan.DataAccess. In questo momento essi sono configurati nel
        ///   seguente modo:
        /// 
        ///   * Nulla, per ora.
        /// </summary>
        public override void Load()
        {
            switch (DependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    // Nulla, per ora.
                    break;

                case DependencyHandling.UnitTesting:
                    // Nulla, per ora.
                    break;
            }
        }
    }
}