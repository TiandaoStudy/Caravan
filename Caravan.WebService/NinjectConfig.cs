using Common.Logging;
using Finsa.Caravan.Common.Logging;
using Finsa.CodeServices.Clock;
using Ninject.Modules;
using PommaLabs.KVLite;

namespace Finsa.Caravan.WebService
{
    internal sealed class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ICache>().ToMethod(ctx => PersistentCache.DefaultInstance).InSingletonScope();
            Bind<IClock>().To<SystemClock>().InSingletonScope();
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.Target.Member.ReflectedType));
        }
    }
}