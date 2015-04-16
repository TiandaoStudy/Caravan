using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Logging;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Finsa.Caravan.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILog _log;
        private AppFunc _next;

        public ExceptionHandlingMiddleware(ILog log)
        {
            Raise<ArgumentNullException>.IfIsNull(log);
            _log = log;
        }

        public void Initialize(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            try
            {
                await _next.Invoke(environment);
            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
                // Response???
            }
        }
    }
}