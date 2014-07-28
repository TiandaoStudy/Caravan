using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FLEX.Common.Data
{
   public interface IDbLogger
   {
      void LogInfo<TFrom>(string infoMessage);
   }
}
