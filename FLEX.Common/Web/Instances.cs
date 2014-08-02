using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FLEX.Common.Web
{
    public static class ErrorManager
    {
        private static readonly IErrorManager CachedInstance = ServiceLocator.Load<IErrorManager>(Configuration.Instance.ErrorManagerTypeInfo);

        public static IErrorManager Instance
        {
            get { return CachedInstance; }
        }
    }
}
