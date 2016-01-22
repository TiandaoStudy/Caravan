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

using Finsa.Caravan.Common.Identity.Models;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.WebApi.Filters;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Web.Http;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   The HELP controller.
    /// </summary>
    /// <remarks>Adjust routing prefix according to your own needs.</remarks>
    /// <remarks>Questo servizio dovrebbe essere esposto pubblicamente.</remarks>
    [RoutePrefix("")]
    public sealed class HelpController : ApiController
    {
        /// <summary>
        ///   Redirects to Swagger help pages.
        /// </summary>
        [Route("")]
        public IHttpActionResult Get()
        {
            var uri = Request.RequestUri.ToString();
            var uriWithoutQuery = uri.Substring(0, uri.Length - Request.RequestUri.Query.Length);
            return Redirect(uriWithoutQuery + "swagger/ui/index");
        }

        /// <summary>
        ///   Returns the web service version written in the main assembly.
        /// </summary>
        [Route("help/version")]
        public string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        /// <summary>
        ///   Returns the authorized user info.
        /// </summary>
        [Route("help/userinfo"), OAuth2Authorize]
        public SecUser GetUserInfo() => (User as IdnPrincipal).User;
    }
}
