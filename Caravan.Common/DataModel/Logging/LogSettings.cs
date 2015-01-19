using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.Caravan.Common.DataModel.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Finsa.Caravan.Common.DataModel.Logging
{
   [Serializable, DataContract(IsReference = true)]
   public class LogSettings : IEquatable<LogSettings>
   {
      [JsonProperty(Order = 0)]
      public long AppId { get; set; }

      [JsonIgnore]
      public SecApp App { get; set; }

      [JsonProperty(Order = 2)]
      public int Enabled { get; set; }

      [JsonProperty(Order = 3)]
      public int Days { get; set; }

      [JsonProperty(Order = 4)]
      public int MaxEntries { get; set; }

      [JsonIgnore]
      public virtual ICollection<LogEntry> LogEntries { get; set; }

      [JsonIgnore]
      public string TypeId { get; set; }

      [JsonConverter(typeof (StringEnumConverter))]
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

      public bool Equals(LogSettings other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return AppId == other.AppId && string.Equals(TypeId, other.TypeId);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((LogSettings) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (AppId.GetHashCode()*397) ^ TypeId.GetHashCode();
         }
      }

      public static bool operator ==(LogSettings left, LogSettings right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(LogSettings left, LogSettings right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable]
   public class LogSettingsSingle : IEquatable<LogSettingsSingle>
   {
      public LogSettings Settings { get; set; }

      public bool Equals(LogSettingsSingle other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Settings.Equals(other.Settings);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((LogSettingsSingle) obj);
      }

      public override int GetHashCode()
      {
         return Settings.GetHashCode();
      }

      public static bool operator ==(LogSettingsSingle left, LogSettingsSingle right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(LogSettingsSingle left, LogSettingsSingle right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable]
   public class LogSettingsList : IEquatable<LogSettingsList>
   {
      public IEnumerable<LogSettings> Settings { get; set; }

      public bool Equals(LogSettingsList other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Settings.Equals(other.Settings);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((LogSettingsList) obj);
      }

      public override int GetHashCode()
      {
         return Settings.GetHashCode();
      }

      public static bool operator ==(LogSettingsList left, LogSettingsList right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(LogSettingsList left, LogSettingsList right)
      {
         return !Equals(left, right);
      }
   }

   public enum LogType : byte
   {
      Debug = 0,
      Info = 1,
      Warn = 2,
      Error = 3,
      Fatal = 4
   }
}