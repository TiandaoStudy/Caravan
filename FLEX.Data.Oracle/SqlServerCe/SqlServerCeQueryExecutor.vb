Imports System.Data.Common
Imports FLEX.Common.Data

Namespace SqlServerCe

    Public NotInheritable Class SqlServerCeQueryExecutor
        Implements IQueryExecutor

        Private Shared ReadOnly DbFactory As DbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0")

        Public Function FillDataTableFromQuery(query As String) As DataTable Implements IQueryExecutor.FillDataTableFromQuery
            Using connection = OpenConnection()
                Dim command = TryCast(connection.CreateCommand(), DbCommand)
                command.CommandText = query
                command.CommandType = CommandType.Text
                Dim table As New DataTable
                Dim adapter = DbFactory.CreateDataAdapter()
                adapter.SelectCommand = command
                adapter.Fill(table)
                Return table
            End Using
        End Function

        Public Function OpenConnection() As IDbConnection Implements IQueryExecutor.OpenConnection
            Dim connection = DbFactory.CreateConnection()
            connection.ConnectionString = Common.Configuration.Instance.ConnectionString
            connection.Open()
            Return connection
        End Function
    End Class

End Namespace