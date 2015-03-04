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
                var body = (ctx.Response.Content == null) ? String.Empty : ctx.Response.Content.ReadAsStringAsync().Result;
                var args = new[]
                {
                    KeyValuePair.Create("req_id", LogRequestAttribute.RequestId.ToString()),
                    KeyValuePair.Create("status_code", ctx.Response.StatusCode.ToString()),
                    KeyValuePair.Create("headers", ctx.Response.Headers.ToString()),
                };
                Db.Logger.LogRawAsync(
                    LogType.Debug,
                    Settings.Default.ApplicationName,
                    HttpContext.Current.User.Identity.Name,
                    ctx.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName,
                    ctx.ActionContext.ActionDescriptor.ActionName,
                    "Outgoing response",
                    body,
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