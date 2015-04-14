using Common.Logging;
using Common.Logging.Factory;

namespace Finsa.Caravan.DataAccess.Logging
{
    public class CaravanLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        protected override ILog CreateLogger(string name)
        {
            return new CaravanLogger(name);
        }
    }
}
