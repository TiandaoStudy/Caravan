using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Utilities;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Finsa.Caravan.DataAccess.Drivers.Sql
{
    public sealed class SqlDbCommandLogger : IDbCommandInterceptor
    {
        private const string CommandIdVariable = "command_id";

        private readonly ICaravanLog _log;

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

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            var commandId = _log.ThreadVariablesContext.Get(CommandIdVariable);
            _log.TraceArgs(() => new LogMessage
            {
                ShortMessage = String.Format("Non query command \"{0}\" ended with result {1}", commandId, interceptionContext.Result),
                LongMessage = Constants.EmptyString,
                Context = "Executed a non query command"
            });
            _log.ThreadVariablesContext.Remove(CommandIdVariable);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
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

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
            var commandId = _log.ThreadVariablesContext.Get(CommandIdVariable);
            _log.TraceArgs(() => new LogMessage
            {
                ShortMessage = String.Format("Reader command \"{0}\" ended", commandId),
                LongMessage = Constants.EmptyString,
                Context = "Executed a reader command"
            });
            _log.ThreadVariablesContext.Remove(CommandIdVariable);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
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

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (IsCaravanContext(interceptionContext))
            {
                return;
            }
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
            _log.ThreadVariablesContext.Remove(CommandIdVariable);
        }

        #region Private members

        private static bool IsCaravanContext(DbInterceptionContext interceptionContext)
        {
            // Required to avoid an infinite loop...
            return interceptionContext.DbContexts.Any(ctx => ctx is SqlDbContext);
        }

        private static List<ParameterInfo> ReadParameters(DbParameterCollection parameterCollection)
        {
            return new List<ParameterInfo>(from DbParameter p in parameterCollection select new ParameterInfo
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