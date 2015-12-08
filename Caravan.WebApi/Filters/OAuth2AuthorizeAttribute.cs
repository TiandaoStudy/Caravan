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

using Finsa.Caravan.Common;
using Finsa.Caravan.WebApi.Models.Identity;
using Ninject;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Finsa.Caravan.WebApi.Filters
{
    public sealed class OAuth2AuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///   Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override async void OnActionExecuting(HttpActionContext actionContext)
        {
            await OnActionExecutingAsync(actionContext, CancellationToken.None);
        }

        /// <summary>
        ///   Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var authorizationSettings = CaravanServiceProvider.NinjectKernel.Get<OAuth2AuthorizationSettings>();

            var accessToken = actionContext.Request.Headers.Authorization?.Parameter;
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return;
            }

            var restClient = new RestClient(authorizationSettings.AccessTokenValidationUrl);
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddParameter(new Parameter
            {
                Name = "token",
                Type = ParameterType.GetOrPost,
                Value = accessToken
            });

            var restResponse = await restClient.ExecuteTaskAsync(restRequest);
        }
    }
}
