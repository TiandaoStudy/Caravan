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

        private static readonly CommonConfiguration CachedInstance;

        static CommonConfiguration()
        {
            var configurationFile = "Caravan.config";
            if (PortableEnvironment.AppIsRunningOnAspNet)
            {
                // If application is running on ASP.NET, then we look for the configuration file
                // inside the root of the project. Usually, configuration file are not stored into
                // the "bin" directory, because every change would make the application restart.
                configurationFile = "~/" + configurationFile;
            }
            CachedInstance = new CommonConfiguration();
            CachedInstance.Initialize(new ConfigurationFileConfigurationProvider<CommonConfiguration>
            {
                ConfigurationFile = PortableEnvironment.MapPath(configurationFile),
                ConfigurationSection = "caravan.common"
            });
            OnStart();
        }

        /// <summary>
        ///   Gets the static configuration instance.
        /// </summary>
        /// <value>The static configuration instance.</value>
        public static CommonConfiguration Instance
        {
            get { return CachedInstance; }
        }

        #endregion

        /// <summary>
        ///   Initializes a new instance of the <see cref="CommonConfiguration"/> class and sets the
        ///   default values for each configuration entry.
        /// </summary>
        public CommonConfiguration()
        {
            AppName = "my_caravan_app";
            AppDescription = "My Wow Caravan App";

            // Logging
            Logging_CaravanVariablesContext_Interval = TimeSpan.FromMinutes(30);
            Logging_EmergencyLog_MaxArchiveFiles = 10;
        }

        public string AppName { get; set; }

        public string AppDescription { get; set; }
        
        /// <summary>
        ///   L'intervallo dopo il quale le variabili memorizzate nel contesto in memoria
        ///   del log di Caravan vengono automaticamente cancellate.
        /// </summary>
        public TimeSpan Logging_CaravanVariablesContext_Interval { get; set; }

        public int Logging_EmergencyLog_MaxArchiveFiles { get; set; }

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
