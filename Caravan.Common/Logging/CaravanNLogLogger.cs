using Common.Logging.NLog;
using NLog;
using PommaLabs.KVLite;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class CaravanNLogLogger : NLogLogger
    {
        CaravanNLogLogger(Logger logger) : base(logger)
        {
            //MemoryCache.DefaultInstance;
        }
    }
}
