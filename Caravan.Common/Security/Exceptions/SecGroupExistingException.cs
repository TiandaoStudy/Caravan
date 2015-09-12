using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Exceptions
{
    [Serializable]
    public class SecGroupExistingException : Exception
    {
        public SecGroupExistingException()
            : base(TheMessage)
        {
        }

        public SecGroupExistingException(string message)
            : base(message)
        {
        }

        public SecGroupExistingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SecGroupExistingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage { get; } = "Group already existing";
    }
}