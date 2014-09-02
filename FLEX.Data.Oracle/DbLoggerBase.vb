Public MustInherit Class DbLoggerBase

   Protected Const MaxShortMessageLength As Integer = 400
   Protected Const MaxLongMessageLength As Integer = 4000

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
