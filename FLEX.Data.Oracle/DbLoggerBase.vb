Public MustInherit Class DbLoggerBase

   Protected Const MaxApplicationNameLength As Integer = 30
   Protected Const MaxCodeUnitLength As Integer = 100
   Protected Const MaxFunctionLength As Integer = 100
   Protected Const MaxShortMessageLength As Integer = 400
   Protected Const MaxLongMessageLength As Integer = 4000
   Protected Const MaxContextLength As Integer = 400

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

   Protected Shared Function Truncate(str As String, maxLength As Integer) As String
      If String.IsNullOrEmpty(str) Then Return str
      maxLength = Math.Max(maxLength, 0) ' To avoid negative lengths...
      Return If(str.Length <= maxLength, str, str.Substring(0, maxLength))
   End Function

End Class
