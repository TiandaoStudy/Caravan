using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Logging.Exceptions
{
    [Serializable]
    public class LogSettingNotFoundException : Exception
    {
        public LogSettingNotFoundException()
            : base(TheMessage)
        {
        }

        public LogSettingNotFoundException(string message)
            : base(message)
        {
        }

        public LogSettingNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected LogSettingNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static string TheMessage
        {
            get { return "Setting not found"; }
        }
    }
}