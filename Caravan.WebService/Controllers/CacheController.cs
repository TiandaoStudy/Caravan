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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PommaLabs.KVLite;
using PommaLabs.KVLite.Web.Http;
using System.Web.Http;
using Finsa.CodeServices.Common;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller che si occupa della gestione della cache locale al server.
    /// </summary>
    [RoutePrefix("cache")]
    public sealed class CacheController : AbstractCacheController
    {
        public CacheController(ICache cache) : base(cache)
        {
        }

        public override IEnumerable<CacheItem<object>> GetItems(string partitionLike = null, string keyLike = null, DateTime? fromExpiry = null, DateTime? toExpiry = null, DateTime? fromCreation = null, DateTime? toCreation = null)
        {
            return base.GetItems(partitionLike, keyLike, fromExpiry, toExpiry, fromCreation, toCreation).Where(ItemIsNotConnectionString);
        }

        public override IEnumerable<CacheItem<object>> GetItemsWithValues(string partitionLike = null, string keyLike = null, DateTime? fromExpiry = null, DateTime? toExpiry = null, DateTime? fromCreation = null, DateTime? toCreation = null)
        {
            return base.GetItemsWithValues(partitionLike, keyLike, fromExpiry, toExpiry, fromCreation, toCreation).Where(ItemIsNotConnectionString);
        }

        public override IEnumerable<CacheItem<object>> GetPartitionItems(string partition, string keyLike = null, DateTime? fromExpiry = null, DateTime? toExpiry = null, DateTime? fromCreation = null, DateTime? toCreation = null)
        {
            return base.GetPartitionItems(partition, keyLike, fromExpiry, toExpiry, fromCreation, toCreation).Where(ItemIsNotConnectionString);
        }

        public override IEnumerable<CacheItem<object>> GetPartitionItemsWithValues(string partition, string keyLike = null, DateTime? fromExpiry = null, DateTime? toExpiry = null, DateTime? fromCreation = null, DateTime? toCreation = null)
        {
            return base.GetPartitionItemsWithValues(partition, keyLike, fromExpiry, toExpiry, fromCreation, toCreation).Where(ItemIsNotConnectionString);
        }

        public override Option<CacheItem<object>> GetItem(string partition, string key)
        {
            var item = base.GetItem(partition, key);
            if (item.HasValue && ItemIsNotConnectionString(item.Value))
            {
                return item;
            }
            return Option.None<CacheItem<object>>();
        }

        private static bool ItemIsNotConnectionString(CacheItem<object> item)
        {
            return !item.Key.ToLower(CultureInfo.InvariantCulture).Contains("connectionstring");
        }
    }
}