Imports System.Data.Common

Namespace SqlServerCe
   Public NotInheritable Class SqlServerCeQueryExecutor
      Inherits QueryExecutorBase

      Private Shared ReadOnly DbFactory As DbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0")

      Public Overrides Function OpenConnection() As IDbConnection
         Dim connection = DbFactory.CreateConnection()
         connection.ConnectionString = Common.Configuration.Instance.ConnectionString
         connection.Open()
         Return connection
      End Function
   End Class
End Namespace