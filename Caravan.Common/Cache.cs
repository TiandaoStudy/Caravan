using Finsa.Caravan.Common.Properties;
using Finsa.Caravan.Common.Utilities.Reflection;
using PommaLabs.KVLite;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   A timed cache which can be used to store any serializable object.
    /// </summary>
    public static class Cache
    {
        private static readonly ICache CachedInstance = ServiceLocator.Load<ICache>(Settings.Default.CacheType);

        /// <summary>
        ///   The cache instance.
        /// </summary>
        public static ICache Instance
        {
            get { return CachedInstance; }
        }
    }
}