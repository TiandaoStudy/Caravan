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
    public sealed class CommonConfiguration : AppConfiguration
    {
        #region Static instance

        /// <summary>
        ///   Gets the static configuration instance.
        /// </summary>
        /// <value>The static configuration instance.</value>
        public static CommonConfiguration Instance { get; } = InitializeInstance();

        static CommonConfiguration InitializeInstance()
        {
            var configurationFile = "Caravan.config";
            if (PortableEnvironment.AppIsRunningOnAspNet)
            {
                // If application is running on ASP.NET, then we look for the configuration file
                // inside the root of the project. Usually, configuration file are not stored into
                // the "bin" directory, because every change would make the application restart.
                configurationFile = "~/" + configurationFile;
            }

            var instance = new CommonConfiguration();
            instance.Initialize(new ConfigurationFileConfigurationProvider<CommonConfiguration>
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
        ///   L'intervallo dopo il quale le variabili memorizzate nel contesto in memoria
        ///   del log di Caravan vengono automaticamente cancellate.
        /// 
        ///   L'intervallo di default è di 30 minuti.
        /// </summary>
        public TimeSpan Logging_CaravanVariablesContext_Interval { get; set; } = TimeSpan.FromMinutes(30);

        /// <summary>
        ///   La massima dimensione in KB che raggiungere il log di emergenza; raggiunta tale soglia,
        ///   il file viene archiviato secondo le politiche di NLog.
        /// 
        ///   Il valore di default 1024KB, cioè, un 1MB.
        /// </summary>
        public int Logging_EmergencyLog_ArchiveAboveSizeInKB { get; set; } = 1024;

        /// <summary>
        ///   Il nome del file del log di emergenza. Si tratta di un layout NLog,
        ///   perciò può anche contenere riferimenti a date e variabili di sistema.
        /// 
        ///   Il valore di default è "caravan-emergency.log".
        /// </summary>
        public string Logging_EmergencyLog_FileName { get; set; } = "${basedir}/App_Data/logs/caravan-emergency.log";

        /// <summary>
        ///   Il numero massimo di file di archivio prodotti quando il log di emergenza 
        ///   raggiunge la dimensione indicata in <see cref="Logging_EmergencyLog_ArchiveAboveSizeInKB"/>.
        /// 
        ///   Il valore di default è 9.
        /// </summary>
        public int Logging_EmergencyLog_MaxArchiveFiles { get; set; } = 9;

        /// <summary>
        ///   Backing field for <see cref="Logging_EmergencyLog_RootPath"/>.
        /// </summary>
        string _mappedEmergencyLogRootPath = PortableEnvironment.MapPath("~\\App_Data\\Logs");

        /// <summary>
        ///   La cartella radice dove andare a memorizzare il log di emergenza più i relativi file di archivio compressi.
        ///   Il file di log verrà memorizzato nel percorso dato dalla concatenazione di <see cref="Logging_EmergencyLog_RootPath"/> e <see cref="Logging_EmergencyLog_FileName"/>.
        /// 
        ///   Il percorso di default è "~/App_Data/Logs".
        /// </summary>
        public string Logging_EmergencyLog_RootPath
        {
            get { return _mappedEmergencyLogRootPath; }
            set { _mappedEmergencyLogRootPath = PortableEnvironment.MapPath(value); }
        }

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
