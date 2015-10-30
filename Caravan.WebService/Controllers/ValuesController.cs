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

using Finsa.CodeServices.Clock;
using Finsa.CodeServices.Common;
using PommaLabs.KVLite;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Finsa.Caravan.WebApi.Folders;
using WebApi.OutputCache.V2;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Sample controller with basic interactions.
    /// </summary>
    [RoutePrefix("values"), AuthorizeForCaravan]
    public sealed class ValuesController : ApiController
    {
        const int CacheSeconds = 120;
        const int MaxStringLength = 30;
        const int OutputCacheSeconds = 30;

        static readonly string CachePartition = typeof(ValuesController).FullName;

        readonly ICache _cache;
        readonly IClock _clock;

        public ValuesController(ICache cache, IClock clock)
        {
            RaiseArgumentNullException.IfIsNull(cache, nameof(cache));
            RaiseArgumentNullException.IfIsNull(clock, nameof(clock));
            _cache = cache;
            _clock = clock;
        }

        /// <summary>
        ///   Returns all values.
        /// </summary>
        /// <returns>All values.</returns>
        [Route(""), CacheOutput(ServerTimeSpan = OutputCacheSeconds, ClientTimeSpan = OutputCacheSeconds)]
        public IEnumerable<string> Get() => _cache
            .GetItems<string>(CachePartition)
            .Select(i => i.Value);

        /// <summary>
        ///   Looks up some data from ID.
        /// </summary>
        /// <param name="id">The id of the data.</param>
        /// <returns>The value of the data.</returns>
        [Route("{id}"), CacheOutput(ServerTimeSpan = OutputCacheSeconds, ClientTimeSpan = OutputCacheSeconds)]
        public string Get(int id) => _cache
            .Get<string>(CachePartition, id.ToString(CultureInfo.InvariantCulture))
            .Value;

        /// <summary>
        ///   Adds given value.
        /// </summary>
        /// <param name="value">Value of the api</param>
        /// <returns></returns>
        [Route("")]
        public void Post([FromBody] string value)
        {
            var randomKey = new Random().Next().ToString(CultureInfo.InvariantCulture);
            _cache.AddTimed(CachePartition, randomKey, value.Truncate(MaxStringLength), _clock.UtcNow.AddSeconds(CacheSeconds));
        }

        /// <summary>
        ///   Add at the api with id "id", the value "value"
        /// </summary>
        /// <param name="id">id of the api</param>
        /// <param name="value">the value to add</param>
        [Route("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _cache.AddTimed(CachePartition, id.ToString(CultureInfo.InvariantCulture), value.Truncate(MaxStringLength), _clock.UtcNow.AddSeconds(CacheSeconds));
        }

        /// <summary>
        ///   Delete the api with the Id retrieve from the body
        /// </summary>
        /// <param name="id">The id of the api to remove</param>
        [Route("{id}")]
        public void Delete(int id)
        {
            _cache.Remove(CachePartition, id.ToString(CultureInfo.InvariantCulture));
        }
    }
}
