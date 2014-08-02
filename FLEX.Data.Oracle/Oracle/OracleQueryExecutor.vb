Imports System.Data.Common
Imports FLEX.Common
Imports FLEX.Common.Data

Namespace Oracle

    Public NotInheritable Class OracleQueryExecutor
        Implements IQueryExecutor

        Public Function FillDataTableFromQuery(query As String) As DataTable Implements IQueryExecutor.FillDataTableFromQuery
            Throw New NotImplementedException()
        End Function

        Public Function OpenConnection() As IDbConnection Implements IQueryExecutor.OpenConnection
            Dim connection = DbProviderFactories.GetFactory("System.Data.OracleClient").CreateConnection()
            connection.ConnectionString = Common.Configuration.Instance.ConnectionString
            connection.Open()
            Return connection
        End Function
    End Class

End Namespace