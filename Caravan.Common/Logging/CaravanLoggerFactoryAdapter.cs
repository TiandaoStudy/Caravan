using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Factory;
using NLog.Config;
using System;
using System.IO;
using LogManager = NLog.LogManager;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class CaravanLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name="properties"></param>
        public CaravanLoggerFactoryAdapter(NameValueCollection properties)
            : base(true)
        {
            var configType = string.Empty;
            var configFile = string.Empty;

            if (properties != null)
            {
                if (properties["configType"] != null)
                {
                    configType = properties["configType"].ToUpper();
                }

                if (properties["configFile"] != null)
                {
                    configFile = properties["configFile"];
                    if (configFile.StartsWith("~/") || configFile.StartsWith("~\\"))
                    {
                        configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/", configFile.Substring(2));
                    }
                }

                if (configType == "FILE")
                {
                    if (configFile == string.Empty)
                    {
                        throw new ConfigurationException("Configuration property 'configFile' must be set for NLog configuration of type 'FILE'.");
                    }

                    if (!File.Exists(configFile))
                    {
                        throw new ConfigurationException("NLog configuration file '" + configFile + "' does not exists");
                    }
                }
            }
            switch (configType)
            {
                case "INLINE":
                    break;

                case "FILE":
                    LogManager.Configuration = new XmlLoggingConfiguration(configFile);
                    break;

                default:
                    break;
            }
        }

        protected override ILog CreateLogger(string name)
        {
            return new CaravanLogger(LogManager.GetLogger(name));
        }
    }
}