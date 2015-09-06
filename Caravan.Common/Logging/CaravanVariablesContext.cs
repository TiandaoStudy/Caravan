using Common.Logging;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Extensions;
using PommaLabs.KVLite;
using PommaLabs.Thrower;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finsa.Caravan.Common.Logging
{
    sealed class CaravanVariablesContext : IVariablesContext
    {
        #region Static members

        private static CaravanVariablesContext _cachedGlobal;

        [ThreadStatic]
        private static CaravanVariablesContext _cachedThread;

        public static CaravanVariablesContext GlobalVariables => _cachedGlobal ?? (_cachedGlobal = new CaravanVariablesContext(MemoryCache.DefaultInstance, CaravanVariablesContextMode.Global));

        public static CaravanVariablesContext ThreadVariables => _cachedThread ?? (_cachedThread = new CaravanVariablesContext(MemoryCache.DefaultInstance, CaravanVariablesContextMode.Thread));

        #endregion Static members

        public CaravanVariablesContext(ICache cache, CaravanVariablesContextMode mode)
        {
            RaiseArgumentNullException.IfIsNull(cache, nameof(cache));
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(CaravanVariablesContextMode), mode));

            Cache = cache;
            Mode = mode;
        }

        public ICache Cache { get; }

        public CaravanVariablesContextMode Mode { get; }

        public IList<KeyValuePair<string, string>> Variables => GetAndSetVariablesMap().Select(x => KeyValuePair.Create(x.Key, x.Value.SafeToString())).ToArray();

        #region IVariablesContext members

        public void Clear()
        {
            var map = GetAndSetVariablesMap();
            map.Clear();
        }

        public bool Contains(string key)
        {
            var map = GetAndSetVariablesMap();
            return map.ContainsKey(key);
        }

        public object Get(string key)
        {
            var map = GetAndSetVariablesMap();
            object value;
            return map.TryGetValue(key, out value) ? value : null;
        }

        public void Remove(string key)
        {
            var map = GetAndSetVariablesMap();
            object value;
            map.TryRemove(key, out value);
        }

        public void Set(string key, object newValue)
        {
            var map = GetAndSetVariablesMap();
            object oldValue;
            if (map.TryGetValue(key, out oldValue))
            {
                map.TryUpdate(key, newValue, oldValue);
            }
            else
            {
                map.TryAdd(key, newValue);
            }
        }

        #endregion

        #region Private members

        const string CachePartition = "Caravan.VariablesContexts";
        const string CacheKeyForGlobal = "global";
        const string CacheKeyPrefixForRequest = "request_";
        const string CacheKeyPrefixForThread = "thread_";

        ConcurrentDictionary<string, object> GetAndSetVariablesMap()
        {
            switch (Mode)
            {
                case CaravanVariablesContextMode.Global:
                    var maybeGlobalMap = Cache.Get<ConcurrentDictionary<string, object>>(CachePartition, CacheKeyForGlobal);

                    // If requested map exists, then return it.
                    if (maybeGlobalMap.HasValue)
                    {
                        return maybeGlobalMap.Value;
                    }

                    // Otherwise, it must be added to the cache and then returned.
                    var newGlobalMap = new ConcurrentDictionary<string, object>();
                    var globalInterval = CommonConfiguration.Instance.Logging_CaravanVariablesContext_Interval;
                    Cache.AddSliding(CachePartition, CacheKeyForGlobal, new ConcurrentDictionary<string, object>(), globalInterval);
                    return newGlobalMap;

                case CaravanVariablesContextMode.Thread:
                    var cacheKeyForThread = GetThreadCacheKey();
                    var maybeThreadMap = Cache.Get<ConcurrentDictionary<string, object>>(CachePartition, cacheKeyForThread);

                    // If requested map exists, then return it.
                    if (maybeThreadMap.HasValue)
                    {
                        return maybeThreadMap.Value;
                    }

                    // Otherwise, it must be added to the cache and then returned.
                    var newThreadMap = new ConcurrentDictionary<string, object>();
                    var threadInterval = CommonConfiguration.Instance.Logging_CaravanVariablesContext_Interval;
                    Cache.AddSliding(CachePartition, cacheKeyForThread, new ConcurrentDictionary<string, object>(), threadInterval);
                    return newThreadMap;

                default:
                    throw new Exception("Caravan variables context is inconsistent, found an invalid mode");
            }
        }

        private string GetThreadCacheKey()
        {
            // Prima cerco di capire se mi trovo in una request IIS. Se si, non importa più in quale
            // thread io sia, perché IIS può spostare il flusso di esecuzione da un thread
            // all'altro. Perciò, se mi trovo in una request, cerco di usare quella come riferimento
            // per il flusso corrente.
            if (HttpContext.Current != null)
            {
                return CacheKeyPrefixForRequest + HttpContext.Current.Request.GetHashCode();
            }

            // Se non mi trovo in una request, allora uso il thread come riferimento per il flusso
            // di lavoro.
            return CacheKeyPrefixForThread + System.Threading.Thread.CurrentThread.Name;
        }

        #endregion Private members
    }

    enum CaravanVariablesContextMode
    {
        Global = 1,
        Thread = 2
    }
}
