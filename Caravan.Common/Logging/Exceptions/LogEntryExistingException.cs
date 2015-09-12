using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Logging.Exceptions
{
    [Serializable]
    public class LogEntryExistingException : Exception
    {
        public LogEntryExistingException()
            : base(TheMessage)
        {
        }

        public LogEntryExistingException(string message)
            : base(message)
        {
        }

        public LogEntryExistingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected LogEntryExistingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage { get; } = "Entry already existing";
    }
}