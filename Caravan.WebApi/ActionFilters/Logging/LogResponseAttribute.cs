using System;
using System.Web;
using System.Web.Http.Filters;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Properties;
using Finsa.Caravan.DataAccess;
using PommaLabs.Extensions;

namespace Finsa.Caravan.WebApi.ActionFilters.Logging
{
    public sealed class LogResponseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext ctx)
        {
            try
            {
                var body = ctx.Response.Content.ReadAsStringAsync();
                var args = new[]
                {
                    KeyValuePair.Create("location", ctx.Response.Headers.Location.SafeToString()),
                };
                Db.Logger.LogRawAsync(
                    LogType.Debug,
                    Settings.Default.ApplicationName,
                    HttpContext.Current.User.Identity.Name,
                    ctx.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName,
                    ctx.ActionContext.ActionDescriptor.ActionName,
                    "Outgoing response",
                    body.Result,
                    "On action executed",
                    args
                );
            }
            catch (Exception ex)
            {
                Db.Logger.LogRawAsync(
                    LogType.Error,
                    Settings.Default.ApplicationName,
                    HttpContext.Current.User.Identity.Name,
                    ctx.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName,
                    ctx.ActionContext.ActionDescriptor.ActionName,
                    ex,
                    "On action executed"
                );
            }
        }
    }
}