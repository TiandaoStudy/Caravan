Imports Dapper
Imports FLEX.Common.Data
Imports PommaLabs.GRAMPA.Extensions

Public MustInherit Class QueryExecutorBase
   Implements IQueryExecutor

   Public Function FillDataTableFromQuery(query As String) As DataTable Implements IQueryExecutor.FillDataTableFromQuery
      Using connection = OpenConnection()
         Return connection.Query(query).ToDataTable()
      End Using
   End Function

   Public Function FillDataTableFromQuery(query As String, parameters As Object) As DataTable Implements IQueryExecutor.FillDataTableFromQuery
      Using connection = OpenConnection()
         Return connection.Query(query, parameters).ToDataTable()
      End Using
   End Function

   Public MustOverride Function OpenConnection() As IDbConnection Implements IQueryExecutor.OpenConnection
End Class
