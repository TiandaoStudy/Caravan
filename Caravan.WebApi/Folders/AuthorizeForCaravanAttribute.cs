using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Finsa.Caravan.Common.Logging;
using PommaLabs.Thrower;

namespace Finsa.Caravan.WebApi.Folders
{
    public sealed class AuthorizeForCaravanAttribute : ActionFilterAttribute
    {
        private readonly ICaravanLog _log;

        public AuthorizeForCaravanAttribute(ICaravanLog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _log = log;
        }

        public static Func<HttpActionContext, CancellationToken, ICaravanLog, Task<bool>> AuthorizationGranted { get; set; } = (actionContext, cancellationToken, log) =>
        {
            const string className = nameof(AuthorizeForCaravanAttribute);
            const string propName = nameof(AuthorizationGranted);
            log.Warn($"Caravan actions are disabled by default, you can enabled them by changing {className}.{propName}");
            return Task.FromResult(false);
        }; 

        public override async void OnActionExecuting(HttpActionContext actionContext)
        {
            await OnActionExecutingAsync(actionContext, CancellationToken.None);
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (await AuthorizationGranted(actionContext, cancellationToken, _log))
            {
                return;
            }
            _log.Warn("");
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
