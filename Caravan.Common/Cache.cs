using Finsa.Caravan.Common.Properties;
using PommaLabs.KVLite;
using PommaLabs.Reflection;

namespace Finsa.Caravan.Common
{
    public static class Cache
    {
        private static readonly ICache CachedInstance = ServiceLocator.Load<ICache>(Settings.Default.CacheType);

        public static ICache Instance
        {
            get { return CachedInstance; }
        }
    }
}
