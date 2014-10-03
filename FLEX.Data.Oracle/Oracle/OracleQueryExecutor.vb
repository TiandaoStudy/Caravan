Imports System.Data.Common
Imports System.Globalization
Imports Oracle.ManagedDataAccess.Client

Namespace Oracle
   Public NotInheritable Class OracleQueryExecutor
      Inherits QueryExecutorBase

      Private Shared ReadOnly DbFactory As DbProviderFactory = New OracleClientFactory()

      Public Overrides Sub ElaborateConnectionString(ByRef connectionString As String)
         Dim lowerConnString = connectionString.ToLower(CultureInfo.InvariantCulture)

         'Add a semicolor in order to avoid issues
         connectionString = connectionString.TrimEnd()
         If Not connectionString.EndsWith(";"c) Then
            connectionString &= ";"c
         End If

         'Connection Pooling
         If Not lowerConnString.Contains("pooling") Then
            connectionString &= "Pooling=true;"
         End If

         'Statement Cache
         If Not lowerConnString.Contains("statement cache size") Then
            connectionString &= String.Format("Statement Cache Size={0};", Configuration.Instance.OracleStatementCacheSize)
         End If
      End Sub

      Public Overrides Function OpenConnection() As IDbConnection
         Dim connection = DbFactory.CreateConnection()
         connection.ConnectionString = Common.Configuration.Instance.ConnectionString
         connection.Open()
         Return connection
      End Function
   End Class
End Namespace