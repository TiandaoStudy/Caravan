Imports Dapper
Imports Finsa.Caravan.Extensions
Imports FLEX.Common.Data

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

   Public MustOverride Sub ElaborateConnectionString(ByRef connectionString As String) Implements IQueryExecutor.ElaborateConnectionString

   Public MustOverride Function OpenConnection() As IDbConnection Implements IQueryExecutor.OpenConnection
End Class
