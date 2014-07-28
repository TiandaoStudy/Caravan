Imports FLEX.Common.Data

NotInheritable Class OracleDbLogger
   Implements IDbLogger

   Sub LogInfo(Of TFrom)(infoMessage As String) Implements IDbLogger.LogInfo
      Throw New NotImplementedException()
   End Sub
End Class
