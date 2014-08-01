﻿Imports Dapper
Imports FLEX.Common
Imports FLEX.Common.Data
Imports System.Data.OracleClient
Imports Thrower

NotInheritable Class OracleDbLogger
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

   Sub LogDebug(Of TCodeUnit)([function] As String, shortMessage As Object, Optional longMessage As Object = Nothing, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogDebug
      Log(Of TCodeUnit)("DEBUG", [function], shortMessage, longMessage, context, args)
   End Sub

   Sub LogInfo(Of TCodeUnit)([function] As String, shortMessage As Object, Optional longMessage As Object = Nothing, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogInfo
      Log(Of TCodeUnit)("INFO", [function], shortMessage, longMessage, context, args)
   End Sub

   Sub LogWarning(Of TCodeUnit)([function] As String, shortMessage As Object, Optional longMessage As Object = Nothing, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogWarning
      Log(Of TCodeUnit)("WARNING", [function], shortMessage, longMessage, context, args)
   End Sub

   Sub LogError(Of TCodeUnit)([function] As String, shortMessage As Object, Optional longMessage As Object = Nothing, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogError
      Log(Of TCodeUnit)("ERROR", [function], shortMessage, longMessage, context, args)
   End Sub

   Sub LogFatal(Of TCodeUnit)([function] As String, shortMessage As Object, Optional longMessage As Object = Nothing, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogFatal
      Log(Of TCodeUnit)("FATAL", [function], shortMessage, longMessage, context, args)
   End Sub

   Sub LogDebug(Of TCodeUnit)([function] As String, exception As Exception, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogDebug
      Log(Of TCodeUnit)("DEBUG", [function], exception, context, args)
   End Sub

   Sub LogInfo(Of TCodeUnit)([function] As String, exception As Exception, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogInfo
      Log(Of TCodeUnit)("INFO", [function], exception, context, args)
   End Sub

   Sub LogWarning(Of TCodeUnit)([function] As String, exception As Exception, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogWarning
      Log(Of TCodeUnit)("WARNING", [function], exception, context, args)
   End Sub

   Sub LogError(Of TCodeUnit)([function] As String, exception As Exception, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogError
      Log(Of TCodeUnit)("ERROR", [function], exception, context, args)
   End Sub

   Sub LogFatal(Of TCodeUnit)([function] As String, exception As Exception, Optional context As String = Nothing, Optional args As IDictionary(Of String, Object) = Nothing) Implements IDbLogger.LogFatal
      Log(Of TCodeUnit)("FATAL", [function], exception, context, args)
   End Sub

#End Region

#Region "Logs Retrieval"

   Public Function RetrieveAllLogs() As IEnumerable(Of DbLog) Implements IDbLogger.RetrieveAllLogs
      Dim query As String = <![CDATA[
         select flog_entry_date as EntryDate, flos_type as Type, flog_Application as Application, flog_code_unit as CodeUnit,
                flog_function as Function, flog_short_msg as ShortMessage, flog_long_msg as LongMessage, flog_context as Context,
                flog_key_0 as Key0, flog_value_0 as Value0, flog_key_1 as Key1, flog_value_1 as Value1, flog_key_2 as Key2, flog_value_2 as Value2,
                flog_key_3 as Key3, flog_value_3 as Value3, flog_key_4 as Key4, flog_value_4 as Value4, flog_key_5 as Key5, flog_value_5 as Value5,
                flog_key_6 as Key6, flog_value_6 as Value6, flog_key_7 as Key7, flog_value_7 as Value7, flog_key_8 as Key8, flog_value_8 as Value8,
                flog_key_9 as Key9, flog_value_9 as Value9
           from flex_log
      ]]>.Value
      Using connection = OpenConnection()
         Return connection.Query(Of DbLog)(query)
      End Using
   End Function

   Public Function RetrieveCurrentApplicationLogs() As IEnumerable(Of DbLog) Implements IDbLogger.RetrieveCurrentApplicationLogs
      Dim query As String = <![CDATA[
         select flog_entry_date as EntryDate, flos_type as Type, flog_Application as Application, flog_code_unit as CodeUnit,
                flog_function as Function, flog_short_msg as ShortMessage, flog_long_msg as LongMessage, flog_context as Context,
                flog_key_0 as Key0, flog_value_0 as Value0, flog_key_1 as Key1, flog_value_1 as Value1, flog_key_2 as Key2, flog_value_2 as Value2,
                flog_key_3 as Key3, flog_value_3 as Value3, flog_key_4 as Key4, flog_value_4 as Value4, flog_key_5 as Key5, flog_value_5 as Value5,
                flog_key_6 as Key6, flog_value_6 as Value6, flog_key_7 as Key7, flog_value_7 as Value7, flog_key_8 as Key8, flog_value_8 as Value8,
                flog_key_9 as Key9, flog_value_9 as Value9
           from flex_log
          where upper(flog_application) = upper(:ApplicationName)
      ]]>.Value
      Using connection = OpenConnection()
         Return connection.Query(Of DbLog)(query, New With {Configuration.Instance.ApplicationName})
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
      Using connection = OpenConnection()
         Dim enabled = connection.Query(Of Integer)("select flos_enabled from flex_log_settings where flos_type = :type", New With {type}).First()
         Return enabled = 0
      End Using
   End Function

   Private Shared Sub Log(Of TCodeUnit)(type As String, [function] As String, shortMessage As Object, longMessage As Object, context As String, args As IDictionary(Of String, Object))
      Raise(Of ArgumentNullException).IfIsNull(shortMessage)

      Using connection = OpenConnection()
         Dim params = New With {
            .p_type = type,
            .p_application = Configuration.Instance.ApplicationName,
            .p_code_unit = GetType(TCodeUnit).FullName,
            .p_function = [function],
            .p_short_msg = shortMessage.ToString(),
            .p_long_msg = If(longMessage Is Nothing, Nothing, longMessage.ToString()),
            .p_context = context
         }
         connection.Execute("pck_flex_log.sp_log", params, commandType:=CommandType.StoredProcedure)
      End Using
   End Sub

   Private Shared Sub Log(Of TCodeUnit)(type As String, [function] As String, exception As Exception, context As String, args As IDictionary(Of String, Object))
      Log(Of TCodeUnit)(type, [function], exception.Message, exception.StackTrace, context, args)
   End Sub

   Private Shared Function OpenConnection() As IDbConnection
      Dim connection = OracleClientFactory.Instance.CreateConnection()
      connection.ConnectionString = QueryExecutor.Instance.ConnectionString
      connection.Open()
      Return connection
   End Function

#End Region

End Class