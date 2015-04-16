using Common.Logging;
using Finsa.Caravan.Common.Logging;
using Ninject.Modules;
using PommaLabs.KVLite;

namespace Finsa.Caravan.WebService
{
    internal sealed class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog, ICaravanLog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.Target.Member.ReflectedType) as ICaravanLog);
            Bind<ICache>().ToMethod(ctx => PersistentCache.DefaultInstance);
        }
    }
}