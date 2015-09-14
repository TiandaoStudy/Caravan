using Finsa.Caravan.Common.Logging.Models;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.UnitTests.Logging.Models
{
    sealed class LogMessageTests : AbstractTests
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
                    var ex = (ErrorThrowingException)deserialized;
                    ex._state = this;
                    ex.SubscribeToSerializeObjectState();
                }
            }
        }
    }
}
