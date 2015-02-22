using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Logging.Exceptions
{
    [Serializable]
    public class LogSettingExistingException : Exception
    {
        public LogSettingExistingException()
            : base(TheMessage)
        {
        }

        public LogSettingExistingException(string message)
            : base(message)
        {
        }

        public LogSettingExistingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected LogSettingExistingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage
        {
            get { return "Setting already existing"; }
        }
    }
}