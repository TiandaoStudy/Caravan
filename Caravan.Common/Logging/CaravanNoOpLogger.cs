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
using Common.Logging.Factory;
using Finsa.Caravan.Common.Logging.Models;
using System;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Stub che non fa nulla di nulla, può essere utile negli unit test.
    /// </summary>
    sealed class CaravanNoOpLogger : AbstractLogger, ICaravanLog
    {
        public override bool IsDebugEnabled => false;

        public override bool IsErrorEnabled => false;

        public override bool IsFatalEnabled => false;

        public override bool IsInfoEnabled => false;

        public override bool IsTraceEnabled => false;

        public override bool IsWarnEnabled => false;

        public void Catching(Exception exception)
        {
        }

        public void Catching(Func<LogMessage> logMessageCallback)
        {
        }

        public void Catching(LogMessage logMessage)
        {
        }

        public void Catching(object message, Exception exception)
        {
        }

        public void Catching(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        public void Catching(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        public void CatchingFormat(string format, Exception exception, params object[] args)
        {
        }

        public void CatchingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        public bool Debug(Func<LogMessage> logMessageCallback) => false;

        public bool Debug(LogMessage logMessage) => false;

        public bool Error(Func<LogMessage> logMessageCallback) => false;

        public bool Error(LogMessage logMessage) => false;

        public bool Fatal(Func<LogMessage> logMessageCallback) => false;

        public bool Fatal(LogMessage logMessage) => false;

        public bool Info(Func<LogMessage> logMessageCallback) => false;

        public bool Info(LogMessage logMessage) => false;

        public bool Rethrowing(Exception exception) => false;

        public bool Rethrowing(Func<LogMessage> logMessageCallback) => false;

        public bool Rethrowing(LogMessage logMessage) => false;

        public bool Rethrowing(object message, Exception exception) => false;

        public bool Rethrowing(Action<FormatMessageHandler> formatMessageCallback, Exception exception) => false;

        public bool Rethrowing(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception) => false;

        public bool RethrowingFormat(string format, Exception exception, params object[] args) => false;

        public bool RethrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args) => false;

        public void Throwing(Func<LogMessage> logMessageCallback)
        {
        }

        public void Throwing(Exception exception)
        {
        }

        public void Throwing(LogMessage logMessage)
        {
        }

        public void Throwing(object message, Exception exception)
        {
        }

        public void Throwing(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        public void Throwing(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        public void ThrowingFormat(string format, Exception exception, params object[] args)
        {
        }

        public void ThrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        public bool Trace(Func<LogMessage> logMessageCallback) => false;

        public bool Trace(LogMessage logMessage) => false;

        public bool Warn(Func<LogMessage> logMessageCallback) => false;

        public bool Warn(LogMessage logMessage) => false;

        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            // Do nothing...
        }
    }
}
