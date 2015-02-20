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

using System.Collections.Generic;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace Finsa.Caravan.WebService.Controllers
{
    [RoutePrefix("values")]
    public sealed class ValuesController : ApiController
    {
        // GET api/values
        /// <summary>
        ///   Returns all values
        /// </summary>
        /// <returns>All values</returns>
        [CacheOutput(ServerTimeSpan = 30)]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        ///   Looks up some data from ID
        /// </summary>
        /// <param name="id">The id of the data</param>
        /// <returns>The value of the data</returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        ///   Looks up api from value
        /// </summary>
        /// <param name="value">Value of the api</param>
        /// <returns></returns>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        ///   Add at the api with id "id", the value "value"
        /// </summary>
        /// <param name="id">id of the api</param>
        /// <param name="value">the value to add</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        ///   Delete the api with the Id retrieve from the body
        /// </summary>
        /// <param name="id">The id of the api to remove</param>
        public void Delete(int id)
        {
        }
    }
}