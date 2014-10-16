using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class LogSettings
   {
      public SecApp App { get; set; }
      public bool Enabled { get; set; }
      public int Days { get; set; }
      public int MaxEntries { get; set; }

      [JsonIgnore]
      public string TypeString { get; set; }

      [JsonConverter(typeof(StringEnumConverter))]
      public LogType Type
      {
         get
         {
            LogType logType;
            Enum.TryParse(TypeString, true, out logType);
            return logType;
         }
         set { TypeString = value.ToString().ToLower(); }
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
