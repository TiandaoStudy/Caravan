Imports Dapper
Imports FLEX.Common
Imports FLEX.Common.Data
Imports System.Data.OracleClient

NotInheritable Class OracleDbLogger
   Implements IDbLogger

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

   Private Shared Function IsLevelEnabled(type As String) As Boolean
      type = type.ToUpper()
      Using connection = OpenConnection()
         Dim enabled = connection.Query(Of Integer)("select flos_enabled from flex_log_settings where flos_type = :type", New With {type}).First()
         Return enabled = 0
      End Using
   End Function

   Private Shared Sub Log(Of TCodeUnit)(type As String, [function] As String, shortMessage As Object, longMessage As Object, context As String, args As IDictionary(Of String, Object))
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
      connection.ConnectionString = Configuration.Instance.ConnectionString
      connection.Open()
      Return connection
   End Function

End Class
