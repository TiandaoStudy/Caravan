using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Utilities;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Globalization;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Drivers.Sql
{
    /// <summary>
    ///   Logs useful information about each SQL command. It does not log queries performed by
    ///   Caravan, in order to avoid loops when Caravan itself uses the database to store logs.
    /// </summary>
    public sealed class SqlDbCommandLogger : IDbCommandInterceptor
    {
        private const string CommandIdVariable = "command_id";

        private readonly ICaravanLog _log;

        /// <summary>
        ///   Builds an SQL command logger, using given log.
        /// </summary>
        /// <param name="log">The log on which we should write.</param>
        public SqlDbCommandLogger(ICaravanLog log)
        {
            Raise<ArgumentNullException>.IfIsNull(log);
            _log = log;
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            try
            {
                var commandId = Guid.NewGuid();
                _log.ThreadVariablesContext.Set(CommandIdVariable, commandId);
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Non query command \"{0}\" started", commandId),
                    LongMessage = command.CommandText,
                    Context = "Executing a non query command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("parameters", ReadParameters(command.Parameters).LogAsJson()),
                        KeyValuePair.Create("timeout", command.CommandTimeout.ToString(CultureInfo.InvariantCulture))
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a non query command!", ex);
            }
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            try
            {
                var commandId = _log.ThreadVariablesContext.Get(CommandIdVariable);
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Non query command \"{0}\" ended with result {1}", commandId, interceptionContext.Result),
                    LongMessage = Constants.EmptyString,
                    Context = "Executed a non query command"
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a non query command!", ex);
            }
            finally
            {
                _log.ThreadVariablesContext.Remove(CommandIdVariable);
            }
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            try
            {
                var commandId = Guid.NewGuid();
                _log.ThreadVariablesContext.Set(CommandIdVariable, commandId);
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Reader command \"{0}\" started", commandId),
                    LongMessage = command.CommandText,
                    Context = "Executing a reader command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("parameters", ReadParameters(command.Parameters).LogAsJson()),
                        KeyValuePair.Create("timeout", command.CommandTimeout.ToString(CultureInfo.InvariantCulture))
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a reader command!", ex);
            }
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            try
            {
                var commandId = _log.ThreadVariablesContext.Get(CommandIdVariable);
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Reader command \"{0}\" ended", commandId),
                    LongMessage = Constants.EmptyString,
                    Context = "Executed a reader command"
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a reader command!", ex);
            }
            finally
            {
                _log.ThreadVariablesContext.Remove(CommandIdVariable);
            }
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            try
            {
                var commandId = Guid.NewGuid();
                _log.ThreadVariablesContext.Set(CommandIdVariable, commandId);
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Scalar command \"{0}\" started", commandId),
                    LongMessage = command.CommandText,
                    Context = "Executing a scalar command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("parameters", ReadParameters(command.Parameters).LogAsJson()),
                        KeyValuePair.Create("timeout", command.CommandTimeout.ToString(CultureInfo.InvariantCulture))
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a scalar command!", ex);
            }
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            try
            {
                var commandId = _log.ThreadVariablesContext.Get(CommandIdVariable);
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Scalar command \"{0}\" ended", commandId),
                    LongMessage = Constants.EmptyString,
                    Context = "Executed a scalar command",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("scalar", interceptionContext.Result.LogAsJson())
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Warn("Error while logging a scalar command!", ex);
            }
            finally
            {
                _log.ThreadVariablesContext.Remove(CommandIdVariable);
            }
        }

        #region Private members

        private bool IsCaravanContext(DbInterceptionContext interceptionContext)
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

        private static List<ParameterInfo> ReadParameters(DbParameterCollection parameterCollection)
        {
            return new List<ParameterInfo>(from DbParameter p in parameterCollection
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

        private sealed class ParameterInfo
        {
            public string Name { get; set; }

            public object Value { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public DbType DbType { get; set; }

            public bool IsNullable { get; set; }

            public int Size { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public ParameterDirection Direction { get; set; }
        }

        #endregion Private members
    }
}