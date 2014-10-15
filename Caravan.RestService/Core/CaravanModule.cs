using System;
using Finsa.Caravan.Common.DataModel;
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

      protected static LogType ParseLogType(string logTypeString)
      {
         LogType logType;
         return Enum.TryParse(logTypeString, true, out logType) ? logType : LogType.Info;
      }
   }
}