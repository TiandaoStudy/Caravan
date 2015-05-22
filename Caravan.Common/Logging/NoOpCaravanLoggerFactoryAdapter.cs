using Common.Logging;
using Common.Logging.Factory;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class NoOpCaravanLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        protected override ILog CreateLogger(string name)
        {
            return new NoOpCaravanLogger();
        }
    }
}