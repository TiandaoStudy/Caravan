using Finsa.Caravan.Common.Properties;
using PommaLabs.KVLite;
using System.Globalization;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   A timed cache which can be used to store any serializable object.
    /// </summary>
    public static class Cache
    {
        private static readonly ICache CachedInstance = (Settings.Default.CacheType.ToLower(CultureInfo.InvariantCulture) == "volatile")
            ? VolatileCache.DefaultInstance as ICache
            : PersistentCache.DefaultInstance;

        /// <summary>
        ///   The cache instance.
        /// </summary>
        public static ICache Instance
        {
            get { return CachedInstance; }
        }
    }
}