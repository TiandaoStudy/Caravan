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
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Mongo;
using Finsa.Caravan.DataAccess.Rest;
using Finsa.Caravan.DataAccess.Sql.FakeSql;
using Finsa.Caravan.DataAccess.Sql.Identity;
using Finsa.Caravan.DataAccess.Sql.MySql;
using Finsa.Caravan.DataAccess.Sql.Oracle;
using Finsa.Caravan.DataAccess.Sql.PostgreSql;
using Finsa.Caravan.DataAccess.Sql.SqlServer;
using Finsa.Caravan.DataAccess.Sql.SqlServerCe;
using IdentityServer3.Core.Configuration;
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
        ///   Configura i servizi di Caravan.DataAccess. In questo momento esse sono:
        /// 
        ///   * <see cref="ICaravanLogRepository"/> via <see cref="CaravanDataSource.Logger"/>.
        ///   * <see cref="ICaravanSecurityRepository"/> via <see cref="CaravanDataSource.Security"/>.
        /// </summary>
        public override void Load()
        {
            switch (DependencyHandling)
            {
                case DependencyHandling.Default:
                case DependencyHandling.DevelopmentEnvironment:
                case DependencyHandling.TestEnvironment:
                case DependencyHandling.ProductionEnvironment:
                    Bind<ICaravanLogRepository>().ToMethod(ctx => CaravanDataSource.Logger).InSingletonScope();
                    Bind<ICaravanSecurityRepository>().ToMethod(ctx => CaravanDataSource.Security).InSingletonScope();
                    LoadDependingOnDataSourceKind();
                    break;

                case DependencyHandling.UnitTesting:
                    // TODO Migliorare questa gestione...
                    Bind<ICaravanLogRepository>().ToMethod(ctx => CaravanDataSource.Logger).InSingletonScope();
                    Bind<ICaravanSecurityRepository>().ToMethod(ctx => CaravanDataSource.Security).InSingletonScope();
                    break;
            }
        }

        private void LoadDependingOnDataSourceKind()
        {
            // Gestione dell'accesso ai dati - parte 1.
            switch (DataSourceKind)
            {
                case CaravanDataSourceKind.FakeSql:
                    Bind<ICaravanDataSourceManager>().To<FakeSqlDataSourceManager>();
                    break;

                case CaravanDataSourceKind.MongoDb:
                    Bind<ICaravanDataSourceManager>().To<MongoDataSourceManager>();
                    Logger = CaravanServiceProvider.NinjectKernel.Get<MongoLogRepository>();
                    Security = CaravanServiceProvider.NinjectKernel.Get<MongoSecurityRepository>();
                    break;

                case CaravanDataSourceKind.MySql:
                    Manager = new MySqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.Oracle:
                    Manager = new OracleDataSourceManager();
                    break;

                case CaravanDataSourceKind.PostgreSql:
                    Manager = new PostgreSqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.Rest:
                    Logger = CaravanServiceProvider.NinjectKernel.Get<RestLogRepository>();
                    Security = CaravanServiceProvider.NinjectKernel.Get<RestSecurityRepository>();
                    break;

                case CaravanDataSourceKind.SqlServer:
                    Manager = new SqlServerDataSourceManager();
                    break;

                case CaravanDataSourceKind.SqlServerCe:
                    Manager = new SqlServerCeDataSourceManager();
                    break;
            }

            // Gestione dell'autenticazione e dell'autorizzazione.
            switch (DataSourceKind)
            {
                case CaravanDataSourceKind.Oracle:
                case CaravanDataSourceKind.MySql:
                case CaravanDataSourceKind.PostgreSql:
                case CaravanDataSourceKind.SqlServer:
                case CaravanDataSourceKind.SqlServerCe:
                    Bind<IdentityServerServiceFactory>().To<SqlIdentityServerServiceFactory>().InSingletonScope();
                    break;
            }
        }
    }
}
