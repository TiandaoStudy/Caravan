using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Properties;
using Finsa.Caravan.DataAccess;
using PommaLabs.Extensions;

namespace Finsa.Caravan.WebApi.ActionFilters.Logging
{
    public sealed class LogRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext ctx)
        {
            try
            {
                var body = ctx.Request.Content.ReadAsStringAsync();
                var args = new[]
                {
                    KeyValuePair.Create("user_agent", ctx.Request.Headers.UserAgent.SafeToString()),
                    KeyValuePair.Create("from", ctx.Request.Headers.From),
                    KeyValuePair.Create("method", ctx.Request.Method.SafeToString()),
                    KeyValuePair.Create("accept", ctx.Request.Headers.Accept.SafeToString()),
                };
                Db.Logger.LogRawAsync(
                    LogType.Debug,
                    Settings.Default.ApplicationName,
                    HttpContext.Current.User.Identity.Name,
                    ctx.ActionDescriptor.ControllerDescriptor.ControllerType.FullName,
                    ctx.ActionDescriptor.ActionName,
                    "Incoming request",
                    body.Result,
                    "On action executing",
                    args
                );
            }
            catch (Exception ex)
            {
                Db.Logger.LogRawAsync(
                    LogType.Error,
                    Settings.Default.ApplicationName,
                    HttpContext.Current.User.Identity.Name,
                    ctx.ActionDescriptor.ControllerDescriptor.ControllerType.FullName,
                    ctx.ActionDescriptor.ActionName,
                    ex,
                    "On action executing"
                );
            }
        }
    }
}
