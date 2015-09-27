using Common.Logging;
using Finsa.CodeServices.Common;
using PommaLabs.KVLite;
using PommaLabs.Thrower;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finsa.Caravan.Common.Logging
{
    internal sealed class CaravanVariablesContext : IVariablesContext, IEnumerable<KeyValuePair<string, object>>
    {
        #region Static members

        private static CaravanVariablesContext _cachedGlobal;

        [ThreadStatic]
        private static CaravanVariablesContext _cachedThread;

        public static CaravanVariablesContext GlobalVariables => _cachedGlobal ?? (_cachedGlobal = new CaravanVariablesContext(CaravanServiceProvider.MemoryCache, CaravanVariablesContextMode.Global));

        public static CaravanVariablesContext ThreadVariables => _cachedThread ?? (_cachedThread = new CaravanVariablesContext(CaravanServiceProvider.MemoryCache, CaravanVariablesContextMode.Thread));

        #endregion Static members

        public CaravanVariablesContext(ICache cache, CaravanVariablesContextMode mode)
        {
            RaiseArgumentNullException.IfIsNull(cache, nameof(cache));
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(CaravanVariablesContextMode), mode), nameof(mode));
            Cache = cache;
            Mode = mode;
        }

        public ICache Cache { get; }

        public CaravanVariablesContextMode Mode { get; }

        #region IEnumerable members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => GetAndSetVariablesMap()
            .ToGTuple()
            .OrderBy(x => x.Value.Item1)
            .Select(x => KeyValuePair.Create(x.Key, x.Value.Item2))
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion IEnumerable members

        #region IVariablesContext members

        public void Clear() => GetAndSetVariablesMap().Clear();

        public bool Contains(string key) => GetAndSetVariablesMap().ContainsKey(key);

        public object Get(string key)
        {
            GTuple2<long, object> tuple;
            return GetAndSetVariablesMap().TryGetValue(key, out tuple) ? tuple.Item2 : null;
        }

        public void Remove(string key)
        {
            GTuple2<long, object> tuple;
            GetAndSetVariablesMap().TryRemove(key, out tuple);
        }

        public void Set(string key, object newValue)
        {
            var map = GetAndSetVariablesMap();
            var newTuple = GTuple.Create(CaravanServiceProvider.Clock.UtcNow.Ticks, newValue);
            GTuple2<long, object> oldTuple;
            if (map.TryGetValue(key, out oldTuple))
            {
                map.TryUpdate(key, newTuple, oldTuple);
            }
            else
            {
                map.TryAdd(key, newTuple);
            }
        }

        #endregion IVariablesContext members

        #region Private members

        private const string CachePartition = "Caravan.VariablesContexts";
        private const string CacheKeyForGlobal = "global";
        private const string CacheKeyPrefixForRequest = "request_";
        private const string CacheKeyPrefixForThread = "thread_";

        private ConcurrentDictionary<string, GTuple2<long, object>> GetAndSetVariablesMap()
        {
            switch (Mode)
            {
                case CaravanVariablesContextMode.Global:
                    var maybeGlobalMap = Cache.Get<ConcurrentDictionary<string, GTuple2<long, object>>>(CachePartition, CacheKeyForGlobal);

                    // If requested map exists, then return it.
                    if (maybeGlobalMap.HasValue)
                    {
                        return maybeGlobalMap.Value;
                    }

                    // Otherwise, it must be added to the cache and then returned.
                    var newGlobalMap = new ConcurrentDictionary<string, GTuple2<long, object>>();
                    var globalInterval = CaravanCommonConfiguration.Instance.Logging_CaravanVariablesContext_Interval;
                    Cache.AddSliding(CachePartition, CacheKeyForGlobal, newGlobalMap, globalInterval);
                    return newGlobalMap;

                case CaravanVariablesContextMode.Thread:
                    var cacheKeyForThread = GetThreadCacheKey();
                    var maybeThreadMap = Cache.Get<ConcurrentDictionary<string, GTuple2<long, object>>>(CachePartition, cacheKeyForThread);

                    // If requested map exists, then return it.
                    if (maybeThreadMap.HasValue)
                    {
                        return maybeThreadMap.Value;
                    }

                    // Otherwise, it must be added to the cache and then returned.
                    var newThreadMap = new ConcurrentDictionary<string, GTuple2<long, object>>();
                    var threadInterval = CaravanCommonConfiguration.Instance.Logging_CaravanVariablesContext_Interval;
                    Cache.AddSliding(CachePartition, cacheKeyForThread, newThreadMap, threadInterval);
                    return newThreadMap;

                default:
                    throw new Exception("Caravan variables context is inconsistent, found an invalid mode!");
            }
        }

        private static string GetThreadCacheKey()
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

    internal enum CaravanVariablesContextMode
    {
        Global = 1,
        Thread = 2
    }
}