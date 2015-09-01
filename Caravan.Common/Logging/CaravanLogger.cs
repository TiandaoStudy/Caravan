using Common.Logging;
using Common.Logging.NLog;
using PommaLabs.KVLite;

namespace Finsa.Caravan.Common.Logging
{
    sealed class CaravanLogger : NLogLogger
    {
        public CaravanLogger(NLog.Logger logger) : base(logger)
        {
        }

        public override IVariablesContext GlobalVariablesContext { get; } = new CaravanVariablesContext(MemoryCache.DefaultInstance, CaravanVariablesContextMode.Global);

        public override IVariablesContext ThreadVariablesContext { get; } = new CaravanVariablesContext(MemoryCache.DefaultInstance, CaravanVariablesContextMode.Thread);
    }
}
