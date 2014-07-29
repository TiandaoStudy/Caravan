Imports FLEX.Common.Data

NotInheritable Class OracleDbLogger
   Implements IDbLogger

   ReadOnly Property IsDebugEnabled As Boolean
      Get

      End Get
   End Property

   Sub LogInfo(Of TFrom)(infoMessage As String) Implements IDbLogger.LogInfo
      Throw New NotImplementedException()
   End Sub
End Class
