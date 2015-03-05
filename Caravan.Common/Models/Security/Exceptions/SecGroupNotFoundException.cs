using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Security.Exceptions
{
    [Serializable]
    public class SecGroupNotFoundException : Exception
    {
        public SecGroupNotFoundException()
            : base(TheMessage)
        {
        }

        public SecGroupNotFoundException(string message)
            : base(message)
        {
        }

        public SecGroupNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SecGroupNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage
        {
            get { return "Group not found"; }
        }
    }
}