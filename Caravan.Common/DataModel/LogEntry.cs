using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class LogEntry
   {
      public const string AutomaticallyFilled = "Automatically filled parameter";
      public const string NotSpecified = "...";

      private CKeyValuePair<string, string>[] _cachedArguments;
      
      [JsonProperty(Order = 0)]
      public long Id { get; set; }

      [JsonProperty(Order = 1)]
      public long AppId { get; set; }
      
      [JsonIgnore]
      public SecApp App { get; set; }
      
      [JsonProperty(Order = 3)]
      public DateTime Date { get; set; }
      
      [JsonProperty(Order = 4)]
      public string UserLogin { get; set; }
      
      [JsonProperty(Order = 5)]
      public string CodeUnit { get; set; }
      
      [JsonProperty(Order = 6)]
      public string Function { get; set; }
      
      [JsonProperty(Order = 7)]
      public string ShortMessage { get; set; }
      
      [JsonProperty(Order = 8)]
      public string LongMessage { get; set; }
      
      [JsonProperty(Order = 9)]
      public string Context { get; set; }

      [JsonIgnore]
      public LogSettings LogSettings { get; set; }
      
      [JsonIgnore]
      public string TypeId { get; set; }
      
      [JsonProperty(Order = 2)]
      [JsonConverter(typeof(StringEnumConverter))]
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
      
      [JsonIgnore]
      public string Key0 { get; set; }
      [JsonIgnore]
      public string Value0 { get; set; }
      [JsonIgnore]
      public string Key1 { get; set; }
      [JsonIgnore]
      public string Value1 { get; set; }
      [JsonIgnore]
      public string Key2 { get; set; }
      [JsonIgnore]
      public string Value2 { get; set; }
      [JsonIgnore]
      public string Key3 { get; set; }
      [JsonIgnore]
      public string Value3 { get; set; }
      [JsonIgnore]
      public string Key4 { get; set; }
      [JsonIgnore]
      public string Value4 { get; set; }
      [JsonIgnore]
      public string Key5 { get; set; }
      [JsonIgnore]
      public string Value5 { get; set; }
      [JsonIgnore]
      public string Key6 { get; set; }
      [JsonIgnore]
      public string Value6 { get; set; }
      [JsonIgnore]
      public string Key7 { get; set; }
      [JsonIgnore]
      public string Value7 { get; set; }
      [JsonIgnore]
      public string Key8 { get; set; }
      [JsonIgnore]
      public string Value8 { get; set; }
      [JsonIgnore]
      public string Key9 { get; set; }
      [JsonIgnore]
      public string Value9 { get; set; }
      
      [JsonProperty(Order = 10)]
      public CKeyValuePair<string, string>[] Arguments
      {
         get
         {
            if (_cachedArguments != null)
            {
               return _cachedArguments;
            }
            var list = new List<CKeyValuePair<string, string>>();
            // Pair 0
            if (Key0 != null)
            {
               list.Add(CKeyValuePair.Create(Key0, Value0));
            }
            // Pair 1
            if (Key1 != null)
            {
               list.Add(CKeyValuePair.Create(Key1, Value1));
            }
            // Pair 2
            if (Key2 != null)
            {
               list.Add(CKeyValuePair.Create(Key2, Value2));
            }
            // Pair 3
            if (Key3 != null)
            {
               list.Add(CKeyValuePair.Create(Key3, Value3));
            }
            // Pair 4
            if (Key4 != null)
            {
               list.Add(CKeyValuePair.Create(Key4, Value4));
            }
            // Pair 5
            if (Key5 != null)
            {
               list.Add(CKeyValuePair.Create(Key5, Value5));
            }
            // Pair 6
            if (Key6 != null)
            {
               list.Add(CKeyValuePair.Create(Key6, Value6));
            }
            // Pair 7
            if (Key7 != null)
            {
               list.Add(CKeyValuePair.Create(Key7, Value7));
            }
            // Pair 8
            if (Key8 != null)
            {
               list.Add(CKeyValuePair.Create(Key8, Value8));
            }
            // Pair 9
            if (Key9 != null)
            {
               list.Add(CKeyValuePair.Create(Key9, Value9));
            }
            _cachedArguments = list.ToArray();
            return _cachedArguments;
         }
         set
         {
            if (value == null)
            {
               return;
            }
            
            // Argument cache is not invalidated.
            _cachedArguments = null;

            for (var i = 0; i < value.Length; ++i)
            {
               var key = value[i].Key;
               var val = value[i].Value;
               switch (i)
               {
                  case 0:
                     Key0 = key;
                     Value0 = val;
                     break;
                  case 1:
                     Key1 = key;
                     Value1 = val;
                     break;
                  case 2:
                     Key2 = key;
                     Value2 = val;
                     break;
                  case 3:
                     Key3 = key;
                     Value3 = val;
                     break;
                  case 4:
                     Key4 = key;
                     Value4 = val;
                     break;
                  case 5:
                     Key5 = key;
                     Value5 = val;
                     break;
                  case 6:
                     Key6 = key;
                     Value6 = val;
                     break;
                  case 7:
                     Key7 = key;
                     Value7 = val;
                     break;
                  case 8:
                     Key8 = key;
                     Value8 = val;
                     break;
                  case 9:
                     Key9 = key;
                     Value9 = val;
                     break;
               }
            }
         }
      }
   }
}