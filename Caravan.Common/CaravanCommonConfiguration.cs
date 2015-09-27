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

using System;
using AutoMapper;
using Common.Logging;
using Finsa.CodeServices.Common.Portability;
using Westwind.Utilities.Configuration;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Configuration class for Caravan.Common. Default values are set inside
    ///   the configuration file itself.
    /// </summary>
    public sealed class CaravanCommonConfiguration : AppConfiguration
    {
        #region Static instance

        /// <summary>
        ///   Gets the static configuration instance.
        /// </summary>
        /// <value>The static configuration instance.</value>
        public static CaravanCommonConfiguration Instance { get; } = InitializeInstance();

        static CaravanCommonConfiguration InitializeInstance()
        {
            var configurationFile = "Caravan.config";
            if (PortableEnvironment.AppIsRunningOnAspNet)
            {
                // If application is running on ASP.NET, then we look for the configuration file
                // inside the root of the project. Usually, configuration file are not stored into
                // the "bin" directory, because every change would make the application restart.
                configurationFile = "~/" + configurationFile;
            }

            var instance = new CaravanCommonConfiguration();
            instance.Initialize(new ConfigurationFileConfigurationProvider<CaravanCommonConfiguration>
            {
                ConfigurationFile = PortableEnvironment.MapPath(configurationFile),
                ConfigurationSection = "caravan.common"
            });

            OnStart();

            return instance;
        }

        #endregion

        /// <summary>
        ///   Il nome identificativo dell'applicazione Caravan, rigorosamente minuscolo.
        /// </summary>
        public string AppName { get; set; } = "my_caravan_app";

        /// <summary>
        ///   La descrizione estesa dell'applicazione, viene usata per mostrare il nome dell'applicativo.
        /// </summary>
        public string AppDescription { get; set; } = "My Wow Caravan App";

        /// <summary>
        ///   L'intervallo dopo il quale le variabili di log memorizzate nel contesto in memoria
        ///   di Caravan vengono automaticamente cancellate.
        /// 
        ///   L'intervallo di default è di 30 minuti.
        /// </summary>
        public TimeSpan Logging_CaravanVariablesContext_Interval { get; set; } = TimeSpan.FromMinutes(30);

        #region OnStart

        private static void OnStart()
        {
            // Mappings
            Mapper.CreateMap<string, LogLevel>().ConvertUsing(str =>
            {
                LogLevel logLevel;
                return Enum.TryParse(str, true, out logLevel) ? logLevel : LogLevel.Debug;
            });
        }

        #endregion
    }
}
