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

namespace Finsa.Caravan.Common.Logging.Exceptions
{
    [Serializable]
    public class LogEntryNotFoundException : Exception
    {
        public LogEntryNotFoundException()
            : base(TheMessage)
        {
        }

        public LogEntryNotFoundException(string message)
            : base(message)
        {
        }

        public LogEntryNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected LogEntryNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage { get; } = "Log not found";
    }
}
