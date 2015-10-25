using Finsa.CodeServices.Compression;
using Finsa.CodeServices.Serialization;
using Ninject.Modules;
using PommaLabs.KVLite;

namespace Finsa.Caravan.WebService
{
    internal sealed class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ICache>().To<PersistentCache>().InSingletonScope().WithConstructorArgument("settings", PersistentCacheSettings.Default);
            Bind<ICompressor>().To<LZ4Compressor>().InSingletonScope();
            Bind<ISerializer>().To<JsonSerializer>().WithConstructorArgument("settings", new JsonSerializerSettings());
        }
    }
}