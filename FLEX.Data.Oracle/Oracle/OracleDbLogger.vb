Imports System.Runtime.CompilerServices
Imports Dapper
Imports PommaLabs.GRAMPA.Diagnostics
Imports PommaLabs.GRAMPA.Extensions
Imports FLEX.Common.Data
Imports PommaLabs.GRAMPA

Namespace Oracle
   NotInheritable Class OracleDbLogger
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

      Sub LogDebug (Of TCodeUnit)(shortMessage As String, Optional longMessage As String = DbLog.NotSpecified,
                                  Optional context As String = DbLog.NotSpecified,
                                  Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                  <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogDebug
         Log (Of TCodeUnit)("DEBUG", [function], shortMessage, longMessage, context, args)
      End Sub

      Sub LogInfo(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = DbLog.NotSpecified,
                                Optional context As String = DbLog.NotSpecified,
                                Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogInfo
         Log(Of TCodeUnit)("INFO", [function], shortMessage, longMessage, context, args)
      End Sub

      Sub LogWarning(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = DbLog.NotSpecified,
                                   Optional context As String = DbLog.NotSpecified,
                                   Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                   <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogWarning
         Log(Of TCodeUnit)("WARNING", [function], shortMessage, longMessage, context, args)
      End Sub

      Sub LogError(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = DbLog.NotSpecified,
                                 Optional context As String = DbLog.NotSpecified,
                                 Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                 <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogError
         Log(Of TCodeUnit)("ERROR", [function], shortMessage, longMessage, context, args)
      End Sub

      Sub LogFatal(Of TCodeUnit)(shortMessage As String, Optional longMessage As String = DbLog.NotSpecified,
                                 Optional context As String = DbLog.NotSpecified,
                                 Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                 <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogFatal
         Log(Of TCodeUnit)("FATAL", [function], shortMessage, longMessage, context, args)
      End Sub

      Sub LogDebug(Of TCodeUnit)(exception As Exception, Optional context As String = DbLog.NotSpecified,
                                  Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                  <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogDebug
         Log(Of TCodeUnit)("DEBUG", [function], exception, context, args)
      End Sub

      Sub LogInfo(Of TCodeUnit)(exception As Exception, Optional context As String = DbLog.NotSpecified,
                                Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogInfo
         Log(Of TCodeUnit)("INFO", [function], exception, context, args)
      End Sub

      Sub LogWarning(Of TCodeUnit)(exception As Exception, Optional context As String = DbLog.NotSpecified,
                                   Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                   <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogWarning
         Log(Of TCodeUnit)("WARNING", [function], exception, context, args)
      End Sub

      Sub LogError(Of TCodeUnit)(exception As Exception, Optional context As String = DbLog.NotSpecified,
                                 Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                 <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogError
         Log(Of TCodeUnit)("ERROR", [function], exception, context, args)
      End Sub

      Sub LogFatal(Of TCodeUnit)(exception As Exception, Optional context As String = DbLog.NotSpecified,
                                 Optional args As ICollection(Of GKeyValuePair(Of String, String)) = Nothing,
                                 <CallerMemberName> Optional [function] As String = DbLog.AutomaticallyFilled) _
         Implements IDbLogger.LogFatal
         Log(Of TCodeUnit)("FATAL", [function], exception, context, args)
      End Sub

#End Region

#Region "Logs Retrieval"

      Public Function RetrieveAllLogs() As IEnumerable(Of DbLog) Implements IDbLogger.RetrieveAllLogs
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
            Return connection.Query (Of DbLog)(query)
         End Using
      End Function

      Public Function RetrieveCurrentApplicationLogs() As IEnumerable(Of DbLog) Implements IDbLogger.RetrieveCurrentApplicationLogs
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
            Return connection.Query (Of DbLog)(query, New With {Common.Configuration.Instance.ApplicationName})
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
            Dim enabled = connection.Query (Of Integer)(query, New With {type}).First()
            Return enabled = 0
         End Using
      End Function

      Private Shared Sub Log(Of TCodeUnit)(type As String, [function] As String, shortMessage As String,
                                           longMessage As String, context As String,
                                           args As ICollection(Of GKeyValuePair(Of String, String)))
         Raise(Of ArgumentNullException).IfIsNull(shortMessage)
         Raise(Of ArgumentOutOfRangeException).If(args IsNot Nothing AndAlso args.Count > MaxArgumentCount)

         Using connection = QueryExecutor.Instance.OpenConnection()
            Dim params = New DynamicParameters()
            params.Add("p_type", type)
            params.Add("p_application", Common.Configuration.Instance.ApplicationName.Truncate(MaxApplicationNameLength))
            params.Add("p_code_unit", GetType(TCodeUnit).FullName.Truncate(MaxCodeUnitLength))
            params.Add("p_function", [function].Truncate(MaxFunctionLength))
            params.Add("p_short_msg", If(shortMessage Is Nothing, DbLog.NotSpecified, shortMessage.Truncate(MaxShortMessageLength)))
            params.Add("p_long_msg", If(longMessage Is Nothing, DbLog.NotSpecified, longMessage.Truncate(MaxLongMessageLength)))
            params.Add("p_context", If(context Is Nothing, DbLog.NotSpecified, context.Truncate(MaxContextLength)))
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
      End Sub

      Private Shared Sub Log(Of TCodeUnit)(type As String, [function] As String, exception As Exception,
                                           context As String, args As ICollection(Of GKeyValuePair(Of String, String)))
         exception = FindInnermostException(exception)
         Log(Of TCodeUnit)(type, [function], exception.Message, exception.StackTrace, context, args)
      End Sub

#End Region
   End Class
End Namespace