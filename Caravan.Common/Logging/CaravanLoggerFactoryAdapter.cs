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

using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Factory;
using System;
using System.IO;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Concrete subclass of ILoggerFactoryAdapter specific to Caravan.
    /// </summary>
    public sealed class CaravanLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        public CaravanLoggerFactoryAdapter(NameValueCollection properties) : base(true)
        {
            var str = string.Empty;
            var path = string.Empty;
            if (properties != null)
            {
                if (properties["configType"] != null)
                {
                    str = properties["configType"].ToUpperInvariant();
                }
                if (properties["configFile"] != null)
                {
                    path = properties["configFile"];
                    if (path.StartsWith("~/", StringComparison.Ordinal) || path.StartsWith("~\\", StringComparison.Ordinal))
                    {
                        path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/", path.Substring(2));
                    }
                }
                if (str == "FILE")
                {
                    if (path == string.Empty)
                    {
                        throw new ConfigurationException("Configuration property 'configFile' must be set for NLog configuration of type 'FILE'.");
                    }
                    if (!File.Exists(path))
                    {
                        throw new ConfigurationException("NLog configuration file '" + path + "' does not exists");
                    }
                }
            }
            switch (str)
            {
                case "FILE":
                    NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(path);
                    break;
            }
        }

        protected override ILog CreateLogger(string name)
        {
            if (name.ToLowerInvariant() == "pommalabs.kvlite.memorycache")
            {
                // Fatto per evitare loop - anche MemoryCache usa il log, si corre il rischio di
                // lavorare con istanze statiche nulle o di entrare in qualche ciclo durante ogni
                // singolo log...
                return new CaravanNoOpLogger();
            }
            return new CaravanLogger(NLog.LogManager.GetLogger(name));
        }
    }
}
