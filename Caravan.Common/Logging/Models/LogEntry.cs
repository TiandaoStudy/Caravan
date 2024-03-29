// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Common.Logging;
using Finsa.CodeServices.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Finsa.Caravan.Common.Logging.Models
{
    [Serializable, DataContract]
    public class LogEntry : EquatableObject<LogEntry>
    {
        [DataMember(Order = 0), YamlIgnore]
        public long Id { get; set; }

        [DataMember(Order = 1)]
        public string AppName { get; set; }

        [DataMember(Order = 2), JsonConverter(typeof(StringEnumConverter))]
        public LogLevel LogLevel { get; set; }

        [IgnoreDataMember]
        internal string LogLevelString
        {
            set { LogLevel = (LogLevel) Enum.Parse(typeof(LogLevel), value); }
        }

        [DataMember(Order = 3)]
        public DateTime Date { get; set; }

        [DataMember(Order = 4)]
        public string UserLogin { get; set; }

        [DataMember(Order = 5)]
        public string CodeUnit { get; set; }

        [DataMember(Order = 6)]
        public string Function { get; set; }

        [DataMember(Order = 7)]
        public string ShortMessage { get; set; }

        [DataMember(Order = 8)]
        public string LongMessage { get; set; }

        [DataMember(Order = 9)]
        public string Context { get; set; }

        [DataMember(Order = 10)]
        public IList<KeyValuePair<string, string>> Arguments { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return KeyValuePair.Create(nameof(Id), Id.ToString(CultureInfo.InvariantCulture));
            yield return KeyValuePair.Create(nameof(AppName), AppName);
            yield return KeyValuePair.Create(nameof(LogLevel), LogLevel.ToString());
            yield return KeyValuePair.Create(nameof(Date), Date.ToString(CultureInfo.InvariantCulture));
        }

        protected override IEnumerable<object> GetIdentifyingMembers()
        {
            yield return Id;
        }
    }
}
