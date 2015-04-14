using System;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Utilities;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using LogLevel = Common.Logging.LogLevel;

namespace Finsa.Caravan.DataAccess.Logging
{
    /// <summary>
    ///   Target per il log di Caravan su database.
    /// </summary>
    [Target("CaravanLogger")]
    public class CaravanLoggerTarget : Target
    {
        private static readonly SimpleLayout DefaultLogLevel = new SimpleLayout("${level}");

        public CaravanLoggerTarget()
        {
            LogLevel = DefaultLogLevel;
            UserLogin = new SimpleLayout("${identity:name=true:lowercase=true}");
            CodeUnit = new SimpleLayout("${callsite:className=true:methodName=false:lowercase=true}");
            Function = new SimpleLayout("${callsite:className=false:methodName=true:lowercase=true}");
        }

        [RequiredParameter]
        public Layout LogLevel { get; set; }

        [RequiredParameter]
        public Layout UserLogin { get; set; }

        [RequiredParameter]
        public Layout CodeUnit { get; set; }

        [RequiredParameter]
        public Layout Function { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            var logLevel = (LogLevel) Enum.Parse(typeof (LogLevel), LogLevel.Render(logEvent));
            var userLogin = UserLogin.Render(logEvent);
            var codeUnit = CodeUnit.Render(logEvent);
            var function = Function.Render(logEvent);
            string shortMsg, longMsg, context;
            ParseMessage(logEvent.FormattedMessage, out shortMsg, out longMsg, out context);
            
            // In order to be able to use thread local information, it must _not_ be async.
            var result = Db.Logger.LogRaw(
                logLevel,
                Common.Properties.Settings.Default.ApplicationName,
                userLogin,
                codeUnit,
                function,
                shortMsg,
                longMsg,
                context
            );
        }

        private static void ParseMessage(string msg, out string shortMsg, out string longMsg, out string context)
        {
            if (!String.IsNullOrWhiteSpace(msg) && msg.StartsWith(CaravanLogger.JsonMessagePrefix))
            {
                var json = msg.Substring(CaravanLogger.JsonMessagePrefix.Length);
                var fmt = CaravanLogger.JsonSerializer.DeserializeObject<LogMessage>(json);
                shortMsg = fmt.ShortMessage.ToString();
                longMsg = fmt.LongMessage.ToString();
                context = fmt.Context.ToString();
            }
            else
            {
                shortMsg = msg;
                longMsg = Constants.EmptyString;
                context = Constants.EmptyString;
            }
        }
    }
}
