using System;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Logging;
using Nancy;

namespace Finsa.Caravan.RestService.Core
{
   public abstract class CustomModule : NancyModule
   {
      protected CustomModule()
      {
      }

      protected CustomModule(string modulePath) : base(modulePath)
      {
      }

      protected static LogType? ParseLogType(string logTypeString, bool fallback = true)
      {
         LogType logType;
         return Enum.TryParse(logTypeString, true, out logType) ? logType : new LogType?();
      }

      protected static LogType SafeParseLogType(string logTypeString, bool fallback = true)
      {
         LogType logType;
         return Enum.TryParse(logTypeString, true, out logType) ? logType : LogType.Info;
      }
   }
}