﻿using System.Collections.Generic;
using Finsa.Caravan.Collections;
using Finsa.Caravan.Common.DataModel;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Dummy
{
   public sealed class DummyLogger : LoggerBase
   {
      public override LogResult Log(LogType type, string applicationName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         throw new System.NotImplementedException();
      }

      protected override IEnumerable<LogEntry> GetLogs(string applicationName, LogType? logType)
      {
         return ReadOnlyList.Empty<LogEntry>();
      }

      protected override IList<LogSettings> GetLogSettings(string applicationName, LogType? logType)
      {
         return ReadOnlyList.Empty<LogSettings>();
      }
   }
}
