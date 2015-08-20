using Common.Logging.Configuration;
using Common.Logging.NLog;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class CaravanLoggerFactoryAdapter : NLogLoggerFactoryAdapter
    {
        public CaravanLoggerFactoryAdapter(NameValueCollection properties) : base(properties)
        {
        }
    }
}
