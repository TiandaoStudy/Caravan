Public MustInherit Class DbLoggerBase

   Protected Const MaxApplicationNameLength As Integer = 30
   Protected Const MaxCodeUnitLength As Integer = 100
   Protected Const MaxFunctionLength As Integer = 100
   Protected Const MaxShortMessageLength As Integer = 400
   Protected Const MaxLongMessageLength As Integer = 4000
   Protected Const MaxContextLength As Integer = 400
    Protected Const MaxKeyLength As Integer = 100
    Protected Const MaxValueLength As Integer = 400
    Protected Const MaxArgumentCount As Integer = 10

   ''' <summary>
   ''' 
   ''' </summary>
   ''' <param name="exception"></param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Protected Shared Function FindInnermostException(exception As Exception) As Exception
      While exception.InnerException IsNot Nothing
         exception = exception.InnerException
      End While
      Return exception
   End Function

End Class
