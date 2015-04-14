using System;
using Common.Logging;
using Common.Logging.Factory;

namespace Finsa.Caravan.DataAccess.Logging
{
    public class CaravanLogger : AbstractLogger
    {
        private readonly NLog.Logger _nlogger;

        public CaravanLogger(string name)
        {
            _nlogger = NLog.LogManager.GetLogger(name);
        }

        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool IsTraceEnabled
        {
            get { return Db.Logger.Settings(Common.Properties.Settings.Default.ApplicationName, LogLevel.Trace).Enabled; }
        }

        public override bool IsDebugEnabled
        {
            get { return Db.Logger.Settings(Common.Properties.Settings.Default.ApplicationName, LogLevel.Debug).Enabled; }
        }

        public override bool IsErrorEnabled
        {
            get { return Db.Logger.Settings(Common.Properties.Settings.Default.ApplicationName, LogLevel.Error).Enabled; }
        }

        public override bool IsFatalEnabled
        {
            get { return Db.Logger.Settings(Common.Properties.Settings.Default.ApplicationName, LogLevel.Fatal).Enabled; }
        }

        public override bool IsInfoEnabled
        {
            get { return Db.Logger.Settings(Common.Properties.Settings.Default.ApplicationName, LogLevel.Info).Enabled; }
        }

        public override bool IsWarnEnabled
        {
            get { return Db.Logger.Settings(Common.Properties.Settings.Default.ApplicationName, LogLevel.Warn).Enabled; }
        }
    }
}
