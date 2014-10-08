Imports System.Runtime.CompilerServices
Imports Dapper
Imports FLEX.Common.DataModel
Imports FLEX.Common.Data
Imports PommaLabs.GRAMPA
Imports PommaLabs.GRAMPA.Diagnostics
Imports PommaLabs.GRAMPA.Extensions

Namespace Oracle
   Public NotInheritable Class OracleDbLogger
      Inherits DbLoggerBase
      Implements IDbLogger

#Region "Public Properties"

      ReadOnly Property IsDebugEnabled As Boolean Implements IDbLogger.IsDebugEnabled
         Get
            Return IsLevelEnabled("DEBUG")
         End Get
      End Property

      ReadOnly Property IsInfoEnabled As Boolean Implements IDbLogger.IsInfoEnabled
         Get
            Return IsLevelEnabled("INFO")
         End Get
      End Property

      ReadOnly Property IsWarningEnabled As Boolean Implements IDbLogger.IsWarningEnabled
         Get
            Return IsLevelEnabled("WARNING")
         End Get
      End Property

      ReadOnly Property IsErrorEnabled As Boolean Implements IDbLogger.IsErrorEnabled
         Get
            Return IsLevelEnabled("ERROR")
         End Get
      End Property

      ReadOnly Property IsFatalEnabled As Boolean Implements IDbLogger.IsFatalEnabled
         Get
            Return IsLevelEnabled("FATAL")
         End Get
      End Property

#End Region

#Region "Logging Methods"

      Function LogDebug(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = LogEntry.NotSpecified,
                                      Optional context As String = LogEntry.NotSpecified,
                                      Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                      <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogDebug
         Log(Of TCodeUnit)("DEBUG", [function], shortMessage, longMessage, context, args)
      End Function

      Function LogInfo(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = LogEntry.NotSpecified,
                                     Optional context As String = LogEntry.NotSpecified,
                                     Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                     <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogInfo
         Log(Of TCodeUnit)("INFO", [function], shortMessage, longMessage, context, args)
      End Function

      Function LogWarning(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = LogEntry.NotSpecified,
                                        Optional context As String = LogEntry.NotSpecified,
                                        Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                        <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogWarning
         Log(Of TCodeUnit)("WARNING", [function], shortMessage, longMessage, context, args)
      End Function

      Function LogError(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = LogEntry.NotSpecified,
                                      Optional context As String = LogEntry.NotSpecified,
                                      Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                      <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogError
         Log(Of TCodeUnit)("ERROR", [function], shortMessage, longMessage, context, args)
      End Function

      Function LogFatal(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = LogEntry.NotSpecified,
                                      Optional context As String = LogEntry.NotSpecified,
                                      Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                      <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogFatal
         Log(Of TCodeUnit)("FATAL", [function], shortMessage, longMessage, context, args)
      End Function

      Function LogDebug(Of TCodeUnit)(exception As Exception, Optional context As String = LogEntry.NotSpecified,
                                      Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                      <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogDebug
         Log(Of TCodeUnit)("DEBUG", [function], exception, context, args)
      End Function

      Function LogInfo(Of TCodeUnit)(exception As Exception, Optional context As String = LogEntry.NotSpecified,
                                     Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                     <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogInfo
         Log(Of TCodeUnit)("INFO", [function], exception, context, args)
      End Function

      Function LogWarning(Of TCodeUnit)(exception As Exception, Optional context As String = LogEntry.NotSpecified,
                                        Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                        <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogWarning
         Log(Of TCodeUnit)("WARNING", [function], exception, context, args)
      End Function

      Function LogError(Of TCodeUnit)(exception As Exception, Optional context As String = LogEntry.NotSpecified,
                                      Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                      <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogError
         Log(Of TCodeUnit)("ERROR", [function], exception, context, args)
      End Function

      Function LogFatal(Of TCodeUnit)(exception As Exception, Optional context As String = LogEntry.NotSpecified,
                                     Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                     <CallerMemberName> Optional [function] As String = LogEntry.AutomaticallyFilled) _
         As LogResult Implements IDbLogger.LogFatal
         Log(Of TCodeUnit)("FATAL", [function], exception, context, args)
      End Function

#End Region

#Region "Logs Retrieval"

      Public Function RetrieveAllLogs() As IEnumerable(Of LogEntry) Implements IDbLogger.RetrieveAllLogs
         Dim query As String =
                <![CDATA[
               select flog_entry_date as EntryDate, flos_type as Type, flog_Application as Application, flog_code_unit as CodeUnit,
                      flog_function as Function, flog_short_msg as ShortMessage, flog_long_msg as LongMessage, flog_context as Context,
                      flog_key_0 as Key0, flog_value_0 as Value0, flog_key_1 as Key1, flog_value_1 as Value1, flog_key_2 as Key2, flog_value_2 as Value2,
                      flog_key_3 as Key3, flog_value_3 as Value3, flog_key_4 as Key4, flog_value_4 as Value4, flog_key_5 as Key5, flog_value_5 as Value5,
                      flog_key_6 as Key6, flog_value_6 as Value6, flog_key_7 as Key7, flog_value_7 as Value7, flog_key_8 as Key8, flog_value_8 as Value8,
                      flog_key_9 as Key9, flog_value_9 as Value9
                  from {0}flex_log
           ]]>.Value
         query = String.Format(query, Configuration.Instance.OracleRunner)
         Using connection = QueryExecutor.Instance.OpenConnection()
            Return connection.Query(Of LogEntry)(query)
         End Using
      End Function

      Public Function RetrieveCurrentApplicationLogs() As IEnumerable(Of LogEntry) Implements IDbLogger.RetrieveCurrentApplicationLogs
         Dim query As String =
                <![CDATA[
               select flog_entry_date as EntryDate, flos_type as Type, flog_Application as Application, flog_code_unit as CodeUnit,
                      flog_function as Function, flog_short_msg as ShortMessage, flog_long_msg as LongMessage, flog_context as Context,
                      flog_key_0 as Key0, flog_value_0 as Value0, flog_key_1 as Key1, flog_value_1 as Value1, flog_key_2 as Key2, flog_value_2 as Value2,
                      flog_key_3 as Key3, flog_value_3 as Value3, flog_key_4 as Key4, flog_value_4 as Value4, flog_key_5 as Key5, flog_value_5 as Value5,
                      flog_key_6 as Key6, flog_value_6 as Value6, flog_key_7 as Key7, flog_value_7 as Value7, flog_key_8 as Key8, flog_value_8 as Value8,
                      flog_key_9 as Key9, flog_value_9 as Value9
                from {0}flex_log
               where upper(flog_application) = upper(:ApplicationName)
           ]]>.Value
         query = String.Format(query, Configuration.Instance.OracleRunner)
         Using connection = QueryExecutor.Instance.OpenConnection()
            Return connection.Query(Of LogEntry)(query, New With {Common.Configuration.Instance.ApplicationName})
         End Using
      End Function

      Public Function RetrieveAllLogsTable() As DataTable Implements IDbLogger.RetrieveAllLogsTable
         Return RetrieveAllLogs().ToDataTable()
      End Function

      Public Function RetrieveCurrentApplicationLogsTable() As DataTable Implements IDbLogger.RetrieveCurrentApplicationLogsTable
         Return RetrieveCurrentApplicationLogs().ToDataTable()
      End Function

#End Region

#Region "Private Methods"

      Private Shared Function IsLevelEnabled(type As String) As Boolean
         type = type.ToUpper()
         Using connection = QueryExecutor.Instance.OpenConnection()
            Dim query = String.Format("select flos_enabled from {0}flex_log_settings where flos_type = :type",
                                      Configuration.Instance.OracleRunner)
            Dim enabled = connection.Query(Of Integer)(query, New With {type}).First()
            Return enabled = 0
         End Using
      End Function

      Private Shared Function Log(Of TCodeUnit)(type As String, [function] As String, shortMessage As String,
                                                longMessage As String, context As String,
                                                args As ICollection(Of GKeyValuePair(Of String, String))) As LogResult
         Try
            Raise(Of ArgumentNullException).IfIsNull(shortMessage)
            Raise(Of ArgumentOutOfRangeException).If(args IsNot Nothing AndAlso args.Count > MaxArgumentCount)

            Using connection = QueryExecutor.Instance.OpenConnection()
               Dim params = New DynamicParameters()
               params.Add("p_type", type)
               params.Add("p_application", Common.Configuration.Instance.ApplicationName.Truncate(MaxApplicationNameLength))
               params.Add("p_code_unit", GetType(TCodeUnit).FullName.Truncate(MaxCodeUnitLength))
               params.Add("p_function", [function].Truncate(MaxFunctionLength))
               params.Add("p_short_msg", If(shortMessage Is Nothing, LogEntry.NotSpecified, shortMessage.Truncate(MaxShortMessageLength)))
               params.Add("p_long_msg", If(longMessage Is Nothing, LogEntry.NotSpecified, longMessage.Truncate(MaxLongMessageLength)))
               params.Add("p_context", If(context Is Nothing, LogEntry.NotSpecified, context.Truncate(MaxContextLength)))
               If args IsNot Nothing Then
                  For i = 0 To args.Count - 1
                     Dim arg = args(i)
                     params.Add(String.Format("p_key_{0}", i), arg.Key.Truncate(MaxKeyLength))
                     params.Add(String.Format("p_value_{0}", i), If(arg.Value Is Nothing, Nothing, arg.Value.Truncate(MaxValueLength)))
                  Next
               End If
               Dim procedure = String.Format("{0}pck_flex_log.sp_log", Configuration.Instance.OracleRunner)
               connection.Execute(procedure, params, commandType:=CommandType.StoredProcedure)
            End Using

            Return LogResult.Successful
         Catch ex As Exception
            Return New LogResult With {.Succeeded = False, .Exception = ex}
         End Try
      End Function

      Private Shared Function Log(Of TCodeUnit)(type As String, [function] As String, exception As Exception,
                                                context As String, args As ICollection(Of GKeyValuePair(Of String, String))) As LogResult
         Try
            Raise(Of ArgumentNullException).IfIsNull(exception)
            exception = FindInnermostException(exception)
         Catch ex As Exception
            Return New LogResult With {.Succeeded = False, .Exception = ex}
         End Try
         Return Log(Of TCodeUnit)(type, [function], exception.Message, exception.StackTrace, context, args)
      End Function

#End Region
   End Class
End Namespace