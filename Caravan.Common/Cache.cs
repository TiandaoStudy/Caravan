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
        private static readonly ICache CachedInstance;

        static Cache()
        {
            switch (Settings.Default.CacheType.ToLower(CultureInfo.InvariantCulture))
            {
                case "persistent":
                    CachedInstance = PersistentCache.DefaultInstance;
                    break;

                case "volatile":
                    CachedInstance = VolatileCache.DefaultInstance;
                    break;

                default:
                    CachedInstance = PersistentCache.DefaultInstance;
                    break;
            }
        }

        /// <summary>
        ///   The cache instance.
        /// </summary>
        public static ICache Instance
        {
            get { return CachedInstance; }
        }
    }
}