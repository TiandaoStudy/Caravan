using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Exceptions
{
    [Serializable]
    public class SecAppNotFoundException : Exception
    {
        public SecAppNotFoundException()
            : base(TheMessage)
        {
        }

        public SecAppNotFoundException(string message)
            : base(message)
        {
        }

        public SecAppNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SecAppNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage { get; } = "Application not found";
    }
}