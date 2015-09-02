using Common.Logging;
using Common.Logging.NLog;

namespace Finsa.Caravan.Common.Logging
{
    sealed class CaravanLogger : NLogLogger
    {
        public CaravanLogger(NLog.Logger logger) : base(logger)
        {
        }

        public override IVariablesContext GlobalVariablesContext => CaravanVariablesContext.GlobalVariables;

        public override IVariablesContext ThreadVariablesContext => CaravanVariablesContext.ThreadVariables;
    }
}
