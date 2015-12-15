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

using Finsa.Caravan.Common.Logging.Models;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.UnitTests.Common.Logging.Models
{
    internal sealed class LogMessageTests : AbstractTests
    {
        /// <summary>
        ///   Verifica che la serializzazione non causi eccezioni se l'eccezione da serializzare
        ///   contiene proprietà che possono andare in errore.
        /// </summary>
        [Test]
        public void ToString_ExceptionWithPropertyThrowingAnError_ShouldNotThrow()
        {
            var logMessage = new LogMessage { Exception = new Exception("TEST", new ErrorThrowingException()) };
            var logMessageToString = logMessage.ToString();

            Assert.IsNotNull(logMessageToString);
            Assert.Greater(logMessageToString.Length, 0);
        }

        [Serializable]
        public class ErrorThrowingException : Exception
        {
            private ExceptionState _state = new ExceptionState();

            public ErrorThrowingException()
            {
                SubscribeToSerializeObjectState();
            }

            public ErrorThrowingException(string message) : base(message)
            {
                SubscribeToSerializeObjectState();
            }

            public ErrorThrowingException(string message, Exception inner) : base(message, inner)
            {
                SubscribeToSerializeObjectState();
            }

            protected ErrorThrowingException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                SubscribeToSerializeObjectState();
            }

            public string EntityValidationErrors => _state.TheError;

            private void SubscribeToSerializeObjectState()
            {
                SerializeObjectState += ((exception, eventArgs) => eventArgs.AddSerializedState(_state));
            }

            [Serializable]
            private class ExceptionState : ISafeSerializationData
            {
                public string TheError
                {
                    get { throw new InvalidOperationException("THE ERROR"); }
                }

                public void CompleteDeserialization(object deserialized)
                {
                    var ex = (ErrorThrowingException) deserialized;
                    ex._state = this;
                    ex.SubscribeToSerializeObjectState();
                }
            }
        }
    }
}