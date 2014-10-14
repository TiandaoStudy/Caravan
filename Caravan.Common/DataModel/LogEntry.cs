using System;

namespace FLEX.Common.DataModel
{
   [Serializable]
   public sealed class LogEntry
   {
      public const string AutomaticallyFilled = "Automatically filled parameter";
      public const string NotSpecified = "...";

      public DateTime Date { get; set; }
      public string TypeString { get; set; }
      public string ApplicationName { get; set; }
      public string UserName { get; set; }
      public string CodeUnit { get; set; }
      public string Function { get; set; }
      public string ShortMessage { get; set; }
      public string LongMessage { get; set; }
      public string Context { get; set; }
      public string Key0 { get; set; }
      public string Value0 { get; set; }
      public string Key1 { get; set; }
      public string Value1 { get; set; }
      public string Key2 { get; set; }
      public string Value2 { get; set; }
      public string Key3 { get; set; }
      public string Value3 { get; set; }
      public string Key4 { get; set; }
      public string Value4 { get; set; }
      public string Key5 { get; set; }
      public string Value5 { get; set; }
      public string Key6 { get; set; }
      public string Value6 { get; set; }
      public string Key7 { get; set; }
      public string Value7 { get; set; }
      public string Key8 { get; set; }
      public string Value8 { get; set; }
      public string Key9 { get; set; }
      public string Value9 { get; set; }

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
}