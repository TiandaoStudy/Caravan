using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.DataModel.Exceptions
{
   [Serializable]
   public class AppNotFoundException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public AppNotFoundException() : base(TheMessage)
      {
      }

      public AppNotFoundException(string message) : base(message)
      {
      }

      public AppNotFoundException(string message, Exception inner) : base(message, inner)
      {
      }

      protected AppNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "Application not found"; }
      }
   }
}