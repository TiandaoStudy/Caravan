using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FLEX.Common.Data
{
   public interface IDbLogger
   {
      /* Test if a level is enabled for logging */
      bool IsDebugEnabled { get; }
      bool IsInfoEnabled { get; }
      bool IsWarnEnabled { get; }
      bool IsErrorEnabled { get; }
      bool IsFatalEnabled { get; }

      void LogInfo<TFrom>(string infoMessage);
   }
}
