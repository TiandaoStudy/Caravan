Imports FLEX.Common.Data
Imports PommaLabs.Armando

Namespace DAO

    Public Class SampleDaoBase
        Inherits DaoBase

        Public Overrides Function CreateContext() As IDbConnection
            Return QueryExecutor.Instance.OpenConnection()
        End Function

        Public Overrides Function CreateContext(ByVal connectionString As String) As IDbConnection
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace