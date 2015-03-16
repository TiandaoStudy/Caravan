using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Security.Exceptions
{
   [Serializable]
   public class SecAppExistingException : Exception
   {
      public SecAppExistingException() : base(TheMessage)
      {
      }

      public SecAppExistingException(string message) : base(message)
      {
      }

      public SecAppExistingException(string message, Exception inner) : base(message, inner)
      {
      }

      protected SecAppExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "Application already existing"; }
      }
   }
}