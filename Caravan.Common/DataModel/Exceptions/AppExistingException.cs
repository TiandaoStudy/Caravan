using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.DataModel.Exceptions
{
   [Serializable]
   public class AppExistingException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public AppExistingException():base(TheMessage)
      {
      }

      public AppExistingException(string message) : base(message)
      {
      }

      public AppExistingException(string message, Exception inner) : base(message, inner)
      {
      }

      protected AppExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "Application already existing"; }
      }
   }
}