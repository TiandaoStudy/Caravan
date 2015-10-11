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

using Finsa.CodeServices.Common.Portability;
using Westwind.Utilities.Configuration;

namespace Finsa.Caravan.WebService
{
    /// <summary>
    ///   Configuration class for Caravan.Common. Default values are set inside the configuration
    ///   file itself.
    /// </summary>
    public sealed class CaravanWebServiceConfiguration : AppConfiguration
    {
        #region Static instance

        /// <summary>
        ///   Gets the static configuration instance.
        /// </summary>
        /// <value>The static configuration instance.</value>
        public static CaravanWebServiceConfiguration Instance { get; } = InitializeInstance();

        private static CaravanWebServiceConfiguration InitializeInstance()
        {
            var configurationFile = "Caravan.config";
            if (PortableEnvironment.AppIsRunningOnAspNet)
            {
                // If application is running on ASP.NET, then we look for the configuration file
                // inside the root of the project. Usually, configuration file are not stored into
                // the "bin" directory, because every change would make the application restart.
                configurationFile = "~/" + configurationFile;
            }

            var instance = new CaravanWebServiceConfiguration();
            instance.Initialize(new ConfigurationFileConfigurationProvider<CaravanWebServiceConfiguration>
            {
                ConfigurationFile = PortableEnvironment.MapPath(configurationFile),
                ConfigurationSection = "caravan.webService"
            });

            OnStart();

            return instance;
        }

        #endregion Static instance

        /// <summary>
        ///   Il percorso del certificato richiesto dal servizio di autorizzazione/autenticazione
        ///   per firmare i TOKEN emessi.
        /// </summary>
        public string Identity_SigningCertificatePath { get; set; } = "~/Caravan.pfx";

        /// <summary>
        ///   La password del certificato richiesto dal servizio di autorizzazione/autenticazione
        ///   per firmare i TOKEN emessi.
        /// </summary>
        public string Identity_SigningCertificatePassword { get; set; } = "FinsaPassword";

        #region OnStart

        private static void OnStart()
        {
        }

        #endregion OnStart
    }
}