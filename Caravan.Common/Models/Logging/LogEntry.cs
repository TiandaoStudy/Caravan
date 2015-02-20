using Finsa.Caravan.Common.DataModel.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.DataModel.Logging
{
    [Serializable, DataContract(IsReference = true)]
    public class LogEntry : IEquatable<LogEntry>
    {
        public const string AutoFilled = "Automatically filled parameter";
        public const string NotSpecified = "...";

        private KeyValuePair<string, string>[] _cachedArguments;

        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public long Id { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public long AppId { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public SecApp App { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public DateTime Date { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public string UserLogin { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public string CodeUnit { get; set; }

        [JsonProperty(Order = 6), DataMember(Order = 6)]
        public string Function { get; set; }

        [JsonProperty(Order = 7), DataMember(Order = 7)]
        public string ShortMessage { get; set; }

        [JsonProperty(Order = 8), DataMember(Order = 8)]
        public string LongMessage { get; set; }

        [JsonProperty(Order = 9), DataMember(Order = 9)]
        public string Context { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public LogSetting LogSettings { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public string TypeId { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
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

        [JsonProperty(Order = 10), DataMember(Order = 10)]
        public KeyValuePair<string, string>[] Arguments
        {
            get
            {
                if (_cachedArguments != null)
                {
                    return _cachedArguments;
                }
                var list = new List<KeyValuePair<string, string>>();
                // Pair 0
                if (Key0 != null)
                {
                    list.Add(KeyValuePair.Create(Key0, Value0));
                }
                // Pair 1
                if (Key1 != null)
                {
                    list.Add(KeyValuePair.Create(Key1, Value1));
                }
                // Pair 2
                if (Key2 != null)
                {
                    list.Add(KeyValuePair.Create(Key2, Value2));
                }
                // Pair 3
                if (Key3 != null)
                {
                    list.Add(KeyValuePair.Create(Key3, Value3));
                }
                // Pair 4
                if (Key4 != null)
                {
                    list.Add(KeyValuePair.Create(Key4, Value4));
                }
                // Pair 5
                if (Key5 != null)
                {
                    list.Add(KeyValuePair.Create(Key5, Value5));
                }
                // Pair 6
                if (Key6 != null)
                {
                    list.Add(KeyValuePair.Create(Key6, Value6));
                }
                // Pair 7
                if (Key7 != null)
                {
                    list.Add(KeyValuePair.Create(Key7, Value7));
                }
                // Pair 8
                if (Key8 != null)
                {
                    list.Add(KeyValuePair.Create(Key8, Value8));
                }
                // Pair 9
                if (Key9 != null)
                {
                    list.Add(KeyValuePair.Create(Key9, Value9));
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

        public bool Equals(LogEntry other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && AppId == other.AppId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LogEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ AppId.GetHashCode();
            }
        }

        public static bool operator ==(LogEntry left, LogEntry right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LogEntry left, LogEntry right)
        {
            return !Equals(left, right);
        }
    }
}