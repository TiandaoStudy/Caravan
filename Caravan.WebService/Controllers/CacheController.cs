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
    public sealed class CacheController : CacheControllerBase
    {
        public override IQueryable<CacheItem<object>> GetItems()
        {
            return base.GetItems().Where(ItemIsNotConnectionString).AsQueryable();
        }

        public override IQueryable<CacheItem<object>> GetItemsWithValues()
        {
            return base.GetItemsWithValues().Where(ItemIsNotConnectionString).AsQueryable();
        }

        public override IQueryable<CacheItem<object>> GetItems(string partition)
        {
            return base.GetItems(partition).Where(ItemIsNotConnectionString).AsQueryable();
        }

        public override IQueryable<CacheItem<object>> GetItemsWithValues(string partition)
        {
            return base.GetItemsWithValues(partition).Where(ItemIsNotConnectionString).AsQueryable();
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