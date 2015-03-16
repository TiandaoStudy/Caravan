using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Security.Exceptions
{
    [Serializable]
    public class SecUserExistingException : Exception
    {
        public SecUserExistingException()
            : base(TheMessage)
        {
        }

        public SecUserExistingException(string message)
            : base(TheMessage)
        {
        }

        public SecUserExistingException(Exception inner)
            : base(TheMessage, inner)
        {
        }

        protected SecUserExistingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage
        {
            get { return "User already existing in the current application"; }
        }
    }
}