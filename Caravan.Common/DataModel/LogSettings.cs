using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class LogSettings
   {
      [JsonProperty(Order = 0)]
      public long AppId { get; set; }
      
      [JsonIgnore]
      public SecApp App { get; set; }
      
      [JsonProperty(Order = 2)]
      public bool Enabled { get; set; }
      
      [JsonProperty(Order = 3)]
      public int Days { get; set; }
      
      [JsonProperty(Order = 4)]
      public int MaxEntries { get; set; }

      [JsonIgnore]
      public virtual ICollection<LogEntry> LogEntries { get; set; }
         
      [JsonIgnore]
      public string TypeId { get; set; }

      [JsonConverter(typeof(StringEnumConverter))]
      [JsonProperty(Order = 1)]
      public LogType Type
      {
         get
         {
            LogType logType;
            Enum.TryParse(TypeId, true, out logType);
            return logType;
         }
         set { TypeId = value.ToString().ToLower(); }
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
