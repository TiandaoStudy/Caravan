using Common.Logging;
using Common.Logging.NLog;
using NLog;
using PommaLabs.KVLite;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class CaravanLogger : NLogLogger
    {
        CaravanLogger(Logger logger) : base(logger)
        {
            //MemoryCache.DefaultInstance;
        }

        public override IVariablesContext GlobalVariablesContext
        {
            get
            {
                return base.GlobalVariablesContext;
            }
        }

        public override IVariablesContext ThreadVariablesContext
        {
            get
            {
                return base.ThreadVariablesContext;
            }
        }
    }
}
