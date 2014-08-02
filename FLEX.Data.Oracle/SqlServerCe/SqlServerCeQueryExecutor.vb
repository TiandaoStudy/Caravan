Imports System.Data.Common
Imports FLEX.Common.Data
Imports Glimpse.Ado.AlternateType

Namespace SqlServerCe

    Public NotInheritable Class SqlServerCeQueryExecutor
        Implements IQueryExecutor

        Public Function FillDataTableFromQuery(query As String) As DataTable Implements IQueryExecutor.FillDataTableFromQuery
            Using connection = OpenConnection()
                Dim command = connection.CreateCommand()
                command.CommandText = query
                command.CommandType = CommandType.Text
                Dim table As New DataTable
                Dim adapter = New GlimpseDbDataAdapter(Nothing)
                adapter.Fill(table)
                Return table
            End Using
        End Function

        Public Function OpenConnection() As IDbConnection Implements IQueryExecutor.OpenConnection
            Dim connection = DbProviderFactories.GetFactory("System.Data.SqlServerCe").CreateConnection()
            connection.ConnectionString = Common.Configuration.Instance.ConnectionString
            connection.Open()
            Return connection
        End Function
    End Class

End Namespace