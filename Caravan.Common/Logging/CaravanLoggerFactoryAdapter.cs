using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.NLog;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class CaravanLoggerFactoryAdapter : NLogLoggerFactoryAdapter
    {
        public CaravanLoggerFactoryAdapter(NameValueCollection properties) : base(properties)
        {
        }

        protected override ILog CreateLogger(string name)
        {
            return new CaravanLogger(NLog.LogManager.GetLogger(name));
        }
    }
}
