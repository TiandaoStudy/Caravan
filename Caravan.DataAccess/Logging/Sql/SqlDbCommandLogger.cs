using Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.DataAccess.Drivers.Sql;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Collections.Concurrent;
using Finsa.CodeServices.Serialization;
using PommaLabs.Thrower;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Logging.Sql
{
    /// <summary>
    ///   Logs useful information about each SQL command. It does not log queries performed by
    ///   Caravan, in order to avoid loops when Caravan itself uses the database to store logs.
    /// </summary>
    public sealed class SqlDbCommandLogger : IDbCommandInterceptor
    {
        const string CommandIdVariable = "command_id";
        const string CommandResultVariable = "command_result";
        const string CommandElapsedMillisecondsVariable = "command_elapsed_msec";
        const string CommandParametersVariable = "command_parameters";
        const string CommandTimeoutVariable = "command_timeout";

        /// <summary>
        ///   A temporary map used to link queries before and after they are executed.
        /// </summary>
        static readonly ConcurrentDictionary<DbCommand, QueryInfo> TmpQueryMap = new ConcurrentDictionary<DbCommand, QueryInfo>();

        readonly ILog _log;

        /// <summary>
        ///   Builds an SQL command logger, using given log.
        /// </summary>
        /// <param name="log">The log on which we should write.</param>
        public SqlDbCommandLogger(ILog log)
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
                // Register the query in the temporary map.
                var queryInfo = ExtractQueryInfo(command);
                TmpQueryMap.Add(command, queryInfo);

                // Start the stopwatch.
                queryInfo.Stopwatch.Restart();
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a non query command!", ex);

                // In case of error, remove the entry from the query map.
                TmpQueryMap.Remove(command);
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
                var queryInfo = TmpQueryMap[command];
                queryInfo.Stopwatch.Stop();

                _log.Trace(new LogMessage
                {
                    ShortMessage = $"Non query command '{queryInfo.CommandId}' executed with result '{interceptionContext.Result}'",
                    LongMessage = queryInfo.CommandText,
                    Context = "Executing a non query command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create(CommandIdVariable, queryInfo.CommandId),
                        KeyValuePair.Create(CommandResultVariable, interceptionContext.Result.ToString(CultureInfo.InvariantCulture)),
                        KeyValuePair.Create(CommandElapsedMillisecondsVariable, queryInfo.CommandElapsedMilliseconds),
                        KeyValuePair.Create(CommandParametersVariable, queryInfo.CommandParameters),
                        KeyValuePair.Create(CommandTimeoutVariable, queryInfo.CommandTimeout)
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a non query command!", ex);
            }
            finally
            {
                // In any case, remove the entry from the query map.
                TmpQueryMap.Remove(command);
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
                // Register the query in the temporary map.
                var queryInfo = ExtractQueryInfo(command);
                TmpQueryMap.Add(command, queryInfo);

                // Start the stopwatch.
                queryInfo.Stopwatch.Restart();
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a reader command!", ex);

                // In case of error, remove the entry from the query map.
                TmpQueryMap.Remove(command);
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
                var queryInfo = TmpQueryMap[command];
                queryInfo.Stopwatch.Stop();

                _log.Trace(new LogMessage
                {
                    ShortMessage = $"Reader command '{queryInfo.CommandId}' executed",
                    LongMessage = queryInfo.CommandText,
                    Context = "Executing a reader command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create(CommandIdVariable, queryInfo.CommandId),
                        KeyValuePair.Create(CommandResultVariable, interceptionContext.Result.ToString()),
                        KeyValuePair.Create(CommandElapsedMillisecondsVariable, queryInfo.CommandElapsedMilliseconds),
                        KeyValuePair.Create(CommandParametersVariable, queryInfo.CommandParameters),
                        KeyValuePair.Create(CommandTimeoutVariable, queryInfo.CommandTimeout)
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a reader command!", ex);
            }
            finally
            {
                // In any case, remove the entry from the query map.
                TmpQueryMap.Remove(command);
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
                // Register the query in the temporary map.
                var queryInfo = ExtractQueryInfo(command);
                TmpQueryMap.Add(command, queryInfo);

                // Start the stopwatch.
                queryInfo.Stopwatch.Restart();
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a scalar command!", ex);

                // In case of error, remove the entry from the query map.
                TmpQueryMap.Remove(command);
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
                var queryInfo = TmpQueryMap[command];
                queryInfo.Stopwatch.Stop();

                _log.Trace(new LogMessage
                {
                    ShortMessage = $"Scalar command '{queryInfo.CommandId}' executed",
                    LongMessage = queryInfo.CommandText,
                    Context = "Executing a scalar command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create(CommandIdVariable, queryInfo.CommandId),
                        KeyValuePair.Create(CommandResultVariable, interceptionContext.Result.ToYamlString(LogMessage.ReadableYamlSettings)),
                        KeyValuePair.Create(CommandElapsedMillisecondsVariable, queryInfo.CommandElapsedMilliseconds),
                        KeyValuePair.Create(CommandParametersVariable, queryInfo.CommandParameters),
                        KeyValuePair.Create(CommandTimeoutVariable, queryInfo.CommandTimeout)
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a scalar command!", ex);
            }
            finally
            {
                // In any case, remove the entry from the query map.
                TmpQueryMap.Remove(command);
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

        static QueryInfo ExtractQueryInfo(DbCommand command)
        {
            return new QueryInfo
            {
                CommandId = UniqueIdGenerator.NewBase32("-"),
                CommandText = command.CommandText,
                CommandParameters = ExtractParameters(command.Parameters).ToYamlString(LogMessage.ReadableYamlSettings),
                CommandTimeout = command.CommandTimeout.ToString(CultureInfo.InvariantCulture)
            };
        }

        static ParameterInfo[] ExtractParameters(DbParameterCollection parameterCollection)
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
                    }).ToArray();
        }

        sealed class QueryInfo
        {
            public QueryInfo()
            {
                Stopwatch = new Stopwatch();
            }

            public string CommandId { get; set; }

            public string CommandText { get; set; }

            public string CommandParameters { get; set; }

            public string CommandTimeout { get; set; }

            public string CommandElapsedMilliseconds
            {
                get { return Stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture); }
            }

            public Stopwatch Stopwatch { get; private set; }
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
