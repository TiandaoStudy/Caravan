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
using Finsa.Caravan.DataAccess.Sql.Identity;
using Finsa.CodeServices.Common.Portability;
using IdentityServer3.Core.Configuration;
using InteractivePreGeneratedViews;
using Ninject;
using Ninject.Web.Common;
using System.IO;

namespace Finsa.Caravan.DataAccess.Sql
{
    /// <summary>
    ///   Dipendenze condivise da tutti i driver SQL.
    /// </summary>
    public abstract class CaravanSqlDataAccessNinjectConfig : CaravanDataAccessNinjectConfig
    {
        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        /// <param name="dataSourceKind">
        ///   Il tipo della sorgente dati che verrà usato dalla componente di accesso ai dati.
        /// </param>
        protected CaravanSqlDataAccessNinjectConfig(DependencyHandling dependencyHandling, CaravanDataSourceKind dataSourceKind)
            : base(dependencyHandling, dataSourceKind)
        {
        }

        /// <summary>
        ///   Configura i servizi di Caravan.DataAccess per il driver SQL. In questo momento essi
        ///   sono configurati nel seguente modo:
        /// 
        ///   * <see cref="ICaravanLogRepository"/> via <see cref="SqlLogRepository"/>.
        ///   * <see cref="ICaravanSecurityRepository"/> via <see cref="SqlSecurityRepository"/>.
        ///   * <see cref="IdentityServerServiceFactory"/> via <see cref="SqlIdentityServerServiceFactory"/>.
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
                case DependencyHandling.UnitTesting:
                    // Gestione dei repository base di Caravan.
                    Bind<ICaravanLogRepository>().To<SqlLogRepository>().InRequestScope();
                    Bind<ICaravanSecurityRepository>().To<SqlSecurityRepository>().InRequestScope();

                    // Gestione dell'autenticazione e dell'autorizzazione.
                    Bind<IdentityServerServiceFactory>().To<SqlIdentityServerServiceFactory>().InSingletonScope();
                    break;
            }
        }

        private void ConfigureEFPregeneratedViews()
        {
            // Configura la generazione automatica delle viste per EF.
            using (var ctx = Kernel.Get<SqlDbContext>())
            {
                // Per prima cosa, recupero la cartella di destinazione e mi assicuro che
                // esista. Se non esiste, la creo, altrimenti avrei un errore alla prima query.
                var pregenViewsPath = CaravanDataAccessConfiguration.Instance.Drivers_Sql_EFPregeneratedViews_Path;
                var mappedPregenViewsPath = PortableEnvironment.MapPath(pregenViewsPath);
                if (!Directory.Exists(mappedPregenViewsPath))
                {
                    Directory.CreateDirectory(mappedPregenViewsPath);
                }

                // Quindi, calcolo il nome del file di destinazione e applico il nuovo meccanismo.
                var caravanPregenViewsFileName = CaravanDataAccessConfiguration.Instance.Drivers_Sql_EFPregeneratedViews_CaravanViewsFileName;
                var caravanPregenViews = Path.Combine(mappedPregenViewsPath, caravanPregenViewsFileName);
                InteractiveViews.SetViewCacheFactory(ctx, new FileViewCacheFactory(caravanPregenViews));
            }
        }
    }
}