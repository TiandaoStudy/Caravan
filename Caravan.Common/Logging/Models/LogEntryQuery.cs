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
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Logging.Models
{
    [Serializable, DataContract]
    public sealed class LogEntryQuery
    {
        [DataMember(Order = 0)]
        public IList<string> AppNames { get; set; }

        [DataMember(Order = 1)]
        public IList<LogLevel> LogLevels { get; set; }

        [DataMember(Order = 2)]
        public bool TruncateLongMessage { get; set; }

        [DataMember(Order = 3)]
        public Option<DateTime> FromDate { get; set; }

        [DataMember(Order = 4)]
        public Option<DateTime> ToDate { get; set; }

        [DataMember(Order = 5)]
        public Option<string> UserLoginLike { get; set; }

        [DataMember(Order = 6)]
        public Option<string> CodeUnitLike { get; set; }

        [DataMember(Order = 7)]
        public Option<string> FunctionLike { get; set; }

        [DataMember(Order = 8)]
        public Option<string> ShortMessageLike { get; set; }

        [DataMember(Order = 9)]
        public Option<string> LongMessageLike { get; set; }

        [DataMember(Order = 10)]
        public Option<string> ContextLike { get; set; }

        [DataMember(Order = 11)]
        public int MaxTruncatedLongMessageLength { get; set; }
    }
}
