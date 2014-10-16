using System;
using Finsa.Caravan.DataModel;
using Nancy;

namespace Finsa.Caravan.RestService.Core
{
   public abstract class CaravanModule : NancyModule
   {
      protected CaravanModule()
      {
      }

      protected CaravanModule(string modulePath) : base(modulePath)
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