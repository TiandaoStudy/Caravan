using System;

namespace FLEX.DataAccess.Core
{
    public abstract class DbLoggerBase
    {
        protected const int MaxApplicationNameLength = 30;
        protected const int MaxCodeUnitLength = 100;
        protected const int MaxFunctionLength = 100;
        protected const int MaxShortMessageLength = 400;
        protected const int MaxLongMessageLength = 4000;
        protected const int MaxContextLength = 400;
        protected const int MaxKeyLength = 100;
        protected const int MaxValueLength = 400;
        protected const int MaxArgumentCount = 10;

        /// <summary>
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static Exception FindInnermostException(Exception exception)
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            return exception;
        }
    }
}