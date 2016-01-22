// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Common.Logging;
using Finsa.CodeServices.Common;
using Ninject;
using PommaLabs.KVLite;
using PommaLabs.Thrower;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Finsa.Caravan.Common.Logging
{
    internal sealed class CaravanVariablesContext : IVariablesContext, IEnumerable<KeyValuePair<string, object>>
    {
        #region Static members

        private static CaravanVariablesContext _cachedGlobal;

        [ThreadStatic]
        private static CaravanVariablesContext _cachedThread;

        public static CaravanVariablesContext GlobalVariables
            => _cachedGlobal
            ?? (_cachedGlobal = new CaravanVariablesContext(CaravanServiceProvider.MemoryCache, CaravanVariablesContextMode.Global, CaravanServiceProvider.NinjectKernel.Get<ICaravanVariablesContextIdentifier>()));

        public static CaravanVariablesContext ThreadVariables
            => _cachedThread
            ?? (_cachedThread = new CaravanVariablesContext(CaravanServiceProvider.MemoryCache, CaravanVariablesContextMode.Thread, CaravanServiceProvider.NinjectKernel.Get<ICaravanVariablesContextIdentifier>()));

        #endregion Static members

        public CaravanVariablesContext(ICache cache, CaravanVariablesContextMode mode, ICaravanVariablesContextIdentifier identifier)
        {
            RaiseArgumentNullException.IfIsNull(cache, nameof(cache));
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(CaravanVariablesContextMode), mode), nameof(mode));
            RaiseArgumentNullException.IfIsNull(identifier, nameof(identifier));
            Cache = cache;
            Mode = mode;
            Identifier = identifier;
        }

        public ICache Cache { get; }

        public CaravanVariablesContextMode Mode { get; }

        public ICaravanVariablesContextIdentifier Identifier { get; }

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

        private ConcurrentDictionary<string, GTuple2<long, object>> GetAndSetVariablesMap()
        {
            switch (Mode)
            {
                case CaravanVariablesContextMode.Global:
                    var cacheKeyForGlobal = Identifier.GlobalIdentity;
                    var maybeGlobalMap = Cache.Get<ConcurrentDictionary<string, GTuple2<long, object>>>(CachePartition, cacheKeyForGlobal);

                    // If requested map exists, then return it.
                    if (maybeGlobalMap.HasValue)
                    {
                        return maybeGlobalMap.Value;
                    }

                    // Otherwise, it must be added to the cache and then returned.
                    var newGlobalMap = new ConcurrentDictionary<string, GTuple2<long, object>>();
                    var globalInterval = CaravanCommonConfiguration.Instance.CacheLifetime;
                    Cache.AddSliding(CachePartition, cacheKeyForGlobal, newGlobalMap, globalInterval);
                    return newGlobalMap;

                case CaravanVariablesContextMode.Thread:
                    var cacheKeyForThread = Identifier.ThreadIdentity;
                    var maybeThreadMap = Cache.Get<ConcurrentDictionary<string, GTuple2<long, object>>>(CachePartition, cacheKeyForThread);

                    // If requested map exists, then return it.
                    if (maybeThreadMap.HasValue)
                    {
                        return maybeThreadMap.Value;
                    }

                    // Otherwise, it must be added to the cache and then returned.
                    var newThreadMap = new ConcurrentDictionary<string, GTuple2<long, object>>();
                    var threadInterval = CaravanCommonConfiguration.Instance.CacheLifetime;
                    Cache.AddSliding(CachePartition, cacheKeyForThread, newThreadMap, threadInterval);
                    return newThreadMap;

                default:
                    throw new Exception("Caravan variables context is inconsistent, found an invalid mode!");
            }
        }

        #endregion Private members
    }

    internal enum CaravanVariablesContextMode
    {
        Global = 1,
        Thread = 2
    }
}
