using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Logging.Exceptions
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

        public static string TheMessage
        {
            get { return "Log not found"; }
        }
    }
}