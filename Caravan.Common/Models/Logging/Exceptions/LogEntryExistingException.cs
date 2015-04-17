using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Logging.Exceptions
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

        public static string TheMessage
        {
            get { return "Entry already existing"; }
        }
    }
}