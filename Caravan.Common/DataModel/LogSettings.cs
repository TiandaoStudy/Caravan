using System;

namespace FLEX.Common.DataModel
{
   [Serializable]
   public class LogSettings
   {
      public string TypeString { get; set; }
      public string ApplicationName { get; set; }
      public bool Enabled { get; set; }
      public int Days { get; set; }
      public int MaxEntries { get; set; }
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
