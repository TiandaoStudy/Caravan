﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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

using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Logging.Models
{
    [Serializable, DataContract]
    public struct LogResult
    {
        public static LogResult Success { get; } = new LogResult
        {
            Succeeded = true
        };

        [DataMember(Order = 0)]
        public bool Succeeded { get; set; }

        [DataMember(Order = 1)]
        public Exception Exception { get; set; }

        public static LogResult Failure(string cause) => new LogResult
        {
            Succeeded = false,
            Exception = new Exception(cause)
        };

        public static LogResult Failure(Exception ex) => new LogResult
        {
            Succeeded = false,
            Exception = ex
        };
    }
}