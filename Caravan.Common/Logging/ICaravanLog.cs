using System;
using System.Collections.Generic;
using Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   A log customized for Caravan.
    /// </summary>
    public interface ICaravanLog : ILog
    {
        #region Trace

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        void TraceArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void TraceArgs(Func<LogMessage> logMessageCallback);

        #endregion

        #region Debug

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        void DebugArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void DebugArgs(Func<LogMessage> logMessageCallback);

        #endregion

        #region Info

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        void InfoArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void InfoArgs(Func<LogMessage> logMessageCallback);

        #endregion

        #region Warn

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        void WarnArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void WarnArgs(Func<LogMessage> logMessageCallback);

        #endregion

        #region Error

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        void ErrorArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void ErrorArgs(Func<LogMessage> logMessageCallback);

        #endregion

        #region Fatal

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        void FatalArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void FatalArgs(Func<LogMessage> logMessageCallback);

        #endregion
    }
}