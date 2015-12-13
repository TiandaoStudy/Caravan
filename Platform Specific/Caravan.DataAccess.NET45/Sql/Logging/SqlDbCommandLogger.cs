using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Serialization;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Sql.Logging
{
    /// <summary>
    ///   Logs useful information about each SQL command. It does not log queries performed by
    ///   Caravan, in order to avoid loops when Caravan itself uses the database to store logs.
    /// </summary>
    public sealed class SqlDbCommandLogger : IDbCommandInterceptor
    {
        private const string CommandIdVariable = "command_id";
        private const string CommandResultVariable = "command_result";
        private const string CommandElapsedMillisecondsVariable = "command_elapsed_msec";
        private const string CommandParametersVariable = "command_parameters";
        private const string CommandTimeoutVariable = "command_timeout";
        private const string TmpCommandStopwatch = "tmp_command_stopwatch";

        readonly ICaravanLog _log;

        /// <summary>
        ///   Builds an SQL command logger, using given log.
        /// </summary>
        /// <param name="log">The log on which we should write.</param>
        public SqlDbCommandLogger(ICaravanLog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _log = log;
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext) || !_log.IsTraceEnabled)
            {
                return;
            }
            try
            {
                StartStopwatch();
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a non query command!", ex);

                // In case of error, remove the entry from the query map.
                _log.ThreadVariablesContext.Remove(TmpCommandStopwatch);
            }
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext) || !_log.IsTraceEnabled)
            {
                return;
            }
            try
            {
                // Retrieve the query info and immediately stop the timer.
                var queryInfo = ExtractQueryInfo(command);
                var elapsedMilliseconds = StopStopwatchAndGetElapsedMilliseconds();

                _log.Trace(() => new LogMessage
                {
                    ShortMessage = $"Non query command '{queryInfo.CommandId}' executed with result '{interceptionContext.Result}'",
                    LongMessage = command.CommandText,
                    Context = "Executing a non query command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create<string, object>(CommandIdVariable, queryInfo.CommandId),
                        KeyValuePair.Create<string, object>(CommandResultVariable, interceptionContext.Result),
                        KeyValuePair.Create<string, object>(CommandElapsedMillisecondsVariable, elapsedMilliseconds),
                        KeyValuePair.Create<string, object>(CommandParametersVariable, queryInfo.CommandParameters),
                        KeyValuePair.Create<string, object>(CommandTimeoutVariable, queryInfo.CommandTimeout)
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a non query command!", ex);

                // In any case, remove the entry from the query map.
                _log.ThreadVariablesContext.Remove(TmpCommandStopwatch);
            }
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext) || !_log.IsTraceEnabled)
            {
                return;
            }
            try
            {
                StartStopwatch();
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a reader command!", ex);

                // In case of error, remove the entry from the query map.
                _log.ThreadVariablesContext.Remove(TmpCommandStopwatch);
            }
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext) || !_log.IsTraceEnabled)
            {
                return;
            }
            try
            {
                // Retrieve the query info and immediately stop the timer.
                var queryInfo = ExtractQueryInfo(command);
                var elapsedMilliseconds = StopStopwatchAndGetElapsedMilliseconds();

                _log.Trace(new LogMessage
                {
                    ShortMessage = $"Reader command '{queryInfo.CommandId}' executed",
                    LongMessage = queryInfo.CommandText,
                    Context = "Executing a reader command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create<string, object>(CommandIdVariable, queryInfo.CommandId),
                        KeyValuePair.Create<string, object>(CommandResultVariable, interceptionContext.Result),
                        KeyValuePair.Create<string, object>(CommandElapsedMillisecondsVariable, elapsedMilliseconds),
                        KeyValuePair.Create<string, object>(CommandParametersVariable, queryInfo.CommandParameters),
                        KeyValuePair.Create<string, object>(CommandTimeoutVariable, queryInfo.CommandTimeout)
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a reader command!", ex);

                // In any case, remove the entry from the query map.
                _log.ThreadVariablesContext.Remove(TmpCommandStopwatch);
            }
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext) || !_log.IsTraceEnabled)
            {
                return;
            }
            try
            {
                StartStopwatch();
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a scalar command!", ex);

                // In case of error, remove the entry from the query map.
                _log.ThreadVariablesContext.Remove(TmpCommandStopwatch);
            }
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext) || !_log.IsTraceEnabled)
            {
                return;
            }
            try
            {
                // Retrieve the query info and immediately stop the timer.
                var queryInfo = ExtractQueryInfo(command);
                var elapsedMilliseconds = StopStopwatchAndGetElapsedMilliseconds();

                _log.Trace(new LogMessage
                {
                    ShortMessage = $"Scalar command '{queryInfo.CommandId}' executed",
                    LongMessage = queryInfo.CommandText,
                    Context = "Executing a scalar command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create<string, object>(CommandIdVariable, queryInfo.CommandId),
                        KeyValuePair.Create<string, object>(CommandResultVariable, interceptionContext.Result.ToYamlString(LogMessage.ReadableYamlSettings)),
                        KeyValuePair.Create<string, object>(CommandElapsedMillisecondsVariable, elapsedMilliseconds),
                        KeyValuePair.Create<string, object>(CommandParametersVariable, queryInfo.CommandParameters),
                        KeyValuePair.Create<string, object>(CommandTimeoutVariable, queryInfo.CommandTimeout)
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a scalar command!", ex);

                // In any case, remove the entry from the query map.
                _log.ThreadVariablesContext.Remove(TmpCommandStopwatch);
            }
        }

        #region Private members

        bool IsCaravanContext(DbInterceptionContext interceptionContext)
        {
            // Required to avoid an infinite loop... We use a try-catch to avoid any kind of problems.
            try
            {
                return interceptionContext.DbContexts.Any(ctx => ctx is SqlDbContext);
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a command!", ex);
                // In case of any kind of error, avoid logging the query.
                return true;
            }
        }

        static QueryInfo ExtractQueryInfo(DbCommand command) => new QueryInfo
        {
            CommandId = UniqueIdGenerator.NewBase32("-"),
            CommandText = command.CommandText,
            CommandParameters = ExtractParameters(command.Parameters).ToYamlString(LogMessage.ReadableYamlSettings),
            CommandTimeout = command.CommandTimeout
        };

        static IEnumerable<ParameterInfo> ExtractParameters(DbParameterCollection parameterCollection)
        {
            return (from DbParameter p in parameterCollection
                    select new ParameterInfo
                    {
                        Name = p.ParameterName,
                        Value = p.Value,
                        DbType = p.DbType,
                        Size = p.Size,
                        IsNullable = p.IsNullable,
                        Direction = p.Direction
                    });
        }

        void StartStopwatch()
        {
            // Register the query in the temporary map.
            var stopwatch = new Stopwatch();
            _log.ThreadVariablesContext.Set(TmpCommandStopwatch, stopwatch);

            // Start the stopwatch.
            stopwatch.Restart();
        }

        string StopStopwatchAndGetElapsedMilliseconds()
        {
            var stopwatch = _log.ThreadVariablesContext.Get(TmpCommandStopwatch) as Stopwatch;

            // Remove the variable, otherwise it will be logged.
            _log.ThreadVariablesContext.Remove(TmpCommandStopwatch);

            // If missing, return a placeholder.
            if (stopwatch == null)
            {
                return "undefined";
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
        }

        sealed class QueryInfo
        {
            public string CommandId { get; set; }

            public string CommandText { get; set; }

            public string CommandParameters { get; set; }

            public int CommandTimeout { get; set; }
        }

        sealed class ParameterInfo
        {
            public string Name { get; set; }

            public object Value { get; set; }

            public DbType DbType { get; set; }

            public bool IsNullable { get; set; }

            public int Size { get; set; }

            public ParameterDirection Direction { get; set; }
        }

        #endregion Private members
    }
}
