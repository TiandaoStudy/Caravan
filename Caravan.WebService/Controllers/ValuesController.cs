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
using System.Globalization;
using System.Linq;
using System.Web.Http;
using LinqToQuerystring.WebApi;
using PommaLabs.Extensions;
using PommaLabs.KVLite;
using WebApi.OutputCache.V2;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Sample controller with basic interactions.
    /// </summary>
    [RoutePrefix("values")]
    public sealed class ValuesController : ApiController
    {
        private const int CacheSeconds = 120;
        private const int MaxStringLength = 30;
        private const int OutputCacheSeconds = 30;

        private static readonly string CachePartition = typeof(ValuesController).FullName;

        /// <summary>
        ///   Returns all values.
        /// </summary>
        /// <returns>All values.</returns>
        [Route(""), LinqToQueryable, CacheOutput(ServerTimeSpan = OutputCacheSeconds, ClientTimeSpan = OutputCacheSeconds)]
        public IQueryable<string> Get()
        {
            return Common.Cache.Instance.GetManyItems(CachePartition).Select(i => i.Value).Cast<string>().AsQueryable();
        }

        /// <summary>
        ///   Looks up some data from ID.
        /// </summary>
        /// <param name="id">The id of the data.</param>
        /// <returns>The value of the data.</returns>
        [Route("{id}"), CacheOutput(ServerTimeSpan = OutputCacheSeconds, ClientTimeSpan = OutputCacheSeconds)]
        public string Get(int id)
        {
            return Common.Cache.Instance.Get<string>(CachePartition, id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///   Adds given value.
        /// </summary>
        /// <param name="value">Value of the api</param>
        /// <returns></returns>
        [Route("")]
        public void Post([FromBody] string value)
        {
            var random = new Random().Next();
            Common.Cache.Instance.AddTimed(CachePartition, random.ToString(CultureInfo.InvariantCulture), value.Truncate(MaxStringLength), DateTime.UtcNow.AddSeconds(CacheSeconds));
        }

        /// <summary>
        ///   Add at the api with id "id", the value "value"
        /// </summary>
        /// <param name="id">id of the api</param>
        /// <param name="value">the value to add</param>
        [Route("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Common.Cache.Instance.AddTimed(CachePartition, id.ToString(CultureInfo.InvariantCulture), value.Truncate(MaxStringLength), DateTime.UtcNow.AddSeconds(CacheSeconds));
        }

        /// <summary>
        ///   Delete the api with the Id retrieve from the body
        /// </summary>
        /// <param name="id">The id of the api to remove</param>
        [Route("{id}")]
        public void Delete(int id)
        {
            Common.Cache.Instance.Remove(CachePartition, id.ToString(CultureInfo.InvariantCulture));
        }
    }
}