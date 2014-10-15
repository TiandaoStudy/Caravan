using System;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.DataModel
{
   [Serializable]
   public class LogSettings
   {
      public string ApplicationName { get; set; }
      public bool Enabled { get; set; }
      public int Days { get; set; }
      public int MaxEntries { get; set; }

      [JsonIgnore]
      public string TypeString { get; set; }

      public LogType Type
      {
         get
         {
            LogType logType;
            Enum.TryParse(TypeString, true, out logType);
            return logType;
         }
      }
   }

   public enum LogType : byte
   {
      Debug = 0,
      Info,
      Warn,
      Error,
      Fatal
   }
}
