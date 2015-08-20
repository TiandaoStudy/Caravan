using Common.Logging.Configuration;
using Common.Logging.NLog;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class CaravanNLogLoggerFactoryAdapter : NLogLoggerFactoryAdapter
    {
        public CaravanNLogLoggerFactoryAdapter(NameValueCollection properties) : base(properties)
        {
        }
    }
}
