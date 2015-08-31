using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.NLog;
using Common.Logging.Simple;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class CaravanLoggerFactoryAdapter : NLogLoggerFactoryAdapter
    {
        public CaravanLoggerFactoryAdapter(NameValueCollection properties) : base(properties)
        {
        }

        protected override ILog CreateLogger(string name)
        {
            if (name.ToLowerInvariant() == "pommalabs.kvlite.memorycache")
            {
                // Fatto per evitare loop - anche MemoryCache usa il log, 
                // si corre il rischio di lavorare con istanze statiche nulle o di entrare in qualche ciclo durante ogni singolo log.
                return new NoOpLogger();
            }
            return new CaravanLogger(NLog.LogManager.GetLogger(name));
        }
    }
}
