using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;
using LinqToQuerystring.WebApi;
using System.Linq;
using System.Web.Http;

namespace Finsa.Caravan.Mvc.Core.Controllers
{
    [RoutePrefix("security")]
    public abstract class SecurityControllerBase : ApiController
    {
        [Route(""), LinqToQueryable]
        public IQueryable<SecApp> GetApps()
        {
            return Db.Security.Apps().AsQueryable();
        }

        [Route("{appName}/users"), LinqToQueryable]
        public IQueryable<SecUser> GetUsers(string appName)
        {
            return Db.Security.Users(appName).AsQueryable();
        }
    }
}