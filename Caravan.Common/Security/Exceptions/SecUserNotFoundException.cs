using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Exceptions
{
    [Serializable]
    public class SecUserNotFoundException : Exception
    {
        public SecUserNotFoundException()
            : base(TheMessage)
        {
        }

        public SecUserNotFoundException(string message)
            : base(message)
        {
        }

        public SecUserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SecUserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage { get; } = "User not found";
    }
}