using Common.Logging;
using Finsa.CodeServices.Clock;
using Finsa.CodeServices.Compression;
using Finsa.CodeServices.Serialization;
using Newtonsoft.Json;
using Ninject.Modules;
using PommaLabs.KVLite;
using JsonSerializer = Finsa.CodeServices.Serialization.JsonSerializer;
using JsonSerializerSettings = Finsa.CodeServices.Serialization.JsonSerializerSettings;

namespace Finsa.Caravan.WebService
{
    internal sealed class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ICache>().To<PersistentCache>().InSingletonScope().WithConstructorArgument("settings", PersistentCacheSettings.Default);

            Bind<ICompressor>().To<GZipCompressor>();
            Bind<ICompressor>().To<SnappyCompressor>().WhenInjectedInto<PersistentCache>();

            Bind<IClock>().To<SystemClock>().InSingletonScope();

            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.Target.Member.ReflectedType));

            Bind<ISerializer>().To<JsonSerializer>().WithConstructorArgument("settings", new JsonSerializerSettings());
            Bind<ISerializer>().To<JsonSerializer>().WhenInjectedInto<PersistentCache>().WithConstructorArgument("settings", new JsonSerializerSettings
            {
                Formatting = Formatting.None
            });
        }
    }
}
