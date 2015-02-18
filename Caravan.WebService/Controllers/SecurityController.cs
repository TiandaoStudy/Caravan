using System.Linq;
using System.Web.Http;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebApi.Models.Security;
using LinqToQuerystring.WebApi;

namespace Caravan.WebService.Controllers
{
    [RoutePrefix("security")]
    public sealed class SecurityController : ApiController
    {
        /// <summary>
        /// Returns all the applications
        /// </summary>
        /// <returns>All the applications</returns>
        [Route("", Name = "GetApps"), LinqToQueryable]
        public IQueryable<LinkedSecApp> GetApps()
        {
            return Db.Security.Apps().AsQueryable().Select(a => new LinkedSecApp(a, Url));
        }

        /// <summary>
        /// Returns all details of the specified application
        /// </summary>
        /// <param name="appName">The application to show</param>
        /// <returns>All the details of the specified application</returns>
        [Route("{appName}")]
        public SecApp GetApp(string appName)
        {
            return Db.Security.App(appName);
        }

        /// <summary>
        /// Insert a new application 
        /// </summary>
        /// <param name="app">The application to insert</param>
        [Route("")]
        public void PutApp([FromBody]SecApp app)
        {
            Db.Security.AddApp(app);
        }

        [Route("{appName}/users"), LinqToQueryable]
        public IQueryable<SecUser> GetUsers(string appName)
        {
            return Db.Security.Users(appName).AsQueryable();
        }
    }
}
