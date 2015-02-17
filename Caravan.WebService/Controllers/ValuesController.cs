using System.Collections.Generic;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace Caravan.WebService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        /// <summary>
        ///   Returns all values
        /// </summary>
        /// <returns>All values</returns>
        [CacheOutput(ServerTimeSpan = 30)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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