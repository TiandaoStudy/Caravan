using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Finsa.Caravan.WebApi.Folders
{
    public sealed class AuthorizeForCaravanAttribute : ExceptionFilterAttribute
    {
        public override async void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            await OnExceptionAsync(actionExecutedContext, CancellationToken.None);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}
