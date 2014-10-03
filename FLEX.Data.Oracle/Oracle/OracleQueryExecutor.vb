Imports System.Data.Common
Imports Oracle.ManagedDataAccess.Client

Namespace Oracle
   Public NotInheritable Class OracleQueryExecutor
      Inherits QueryExecutorBase

      Private Shared ReadOnly DbFactory As DbProviderFactory = New OracleClientFactory()

      Public Overrides Function OpenConnection() As IDbConnection
         Dim connection = DbFactory.CreateConnection()
         connection.ConnectionString = Common.Configuration.Instance.ConnectionString
         connection.Open()
         Return connection
      End Function
   End Class
End Namespace