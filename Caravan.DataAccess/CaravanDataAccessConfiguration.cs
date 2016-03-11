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
using Finsa.CodeServices.Common.Portability;
using System;
using Westwind.Utilities.Configuration;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Configuration class for Caravan.DataAccess. Default values are set inside the
    ///   configuration file itself.
    /// </summary>
    public sealed class CaravanDataAccessConfiguration : AppConfiguration
    {
        #region Static instance

        /// <summary>
        ///   Gets the static configuration instance.
        /// </summary>
        /// <value>The static configuration instance.</value>
        public static CaravanDataAccessConfiguration Instance { get; } = InitializeInstance();

        private static CaravanDataAccessConfiguration InitializeInstance()
        {
            var configurationFile = "Caravan.config";
            if (PortableEnvironment.AppIsRunningOnAspNet)
            {
                // If application is running on ASP.NET, then we look for the configuration file
                // inside the root of the project. Usually, configuration file are not stored into
                // the "bin" directory, because every change would make the application restart.
                configurationFile = "~/" + configurationFile;
            }

            var instance = new CaravanDataAccessConfiguration();
            instance.Initialize(new ConfigurationFileConfigurationProvider<CaravanDataAccessConfiguration>
            {
                ConfigurationFile = PortableEnvironment.MapPath(configurationFile),
                ConfigurationSection = "caravan.dataAccess"
            });

            return instance;
        }

        #endregion Static instance

        /// <summary>
        ///   Initializes a new instance of the <see cref="CaravanCommonConfiguration"/> class and
        ///   sets the default values for each configuration entry.
        /// </summary>
        public CaravanDataAccessConfiguration()
        {
            ConnectionString = string.Empty;
            DataSourceKind = CaravanDataSourceKind.Effort;

            // SQL
            SqlSchema = "mydb";
            SqlInitializer = "CreateDatabaseIfNotExists";

            // SQL - Oracle
            OracleStatementCacheSize = 10;

            // REST
            RestTestAuthObject = "_TEST_TOKEN_";
        }

        public string ConnectionString { get; set; }

        public CaravanDataSourceKind DataSourceKind { get; set; }

        /// <summary>
        ///   L'intervallo dopo il quale le impostazioni di log messe in cache devono essere aggiornate.
        /// 
        ///   L'intervallo di default è di 30 minuti.
        /// </summary>
        public TimeSpan Logging_SettingsCache_Interval { get; set; } = TimeSpan.FromMinutes(30);

        #region SQL

        public string SqlSchema { get; set; }

        public string SqlInitializer { get; set; }

        #region Oracle

        public int OracleStatementCacheSize { get; set; }

        #endregion Oracle

        #endregion SQL

        #region REST

        public string RestTestAuthObject { get; set; }

        #endregion REST

        #region MongoDB

        public string MongoDbName { get; set; }

        #endregion MongoDB
    }
}
