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
                    if (path.StartsWith("~/") || path.StartsWith("~\\"))
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
