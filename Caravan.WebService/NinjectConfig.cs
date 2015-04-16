using Common.Logging;
using Finsa.Caravan.Common.Logging;
using Ninject.Modules;

namespace Finsa.Caravan.WebService
{
    internal sealed class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog, ICaravanLog>().ToMethod(ctx => LogManager.GetLogger(ctx.GetScope().GetType()) as ICaravanLog);
        }
    }
}