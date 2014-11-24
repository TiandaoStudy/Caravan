using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.DataModel.Exceptions
{
   [Serializable]
   public class SettingsNotFoundException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public SettingsNotFoundException(): base(TheMessage)
      {
      }

      public SettingsNotFoundException(string message) : base(message)
      {
      }

      public SettingsNotFoundException(string message, Exception inner) : base(message, inner)
      {
      }

      protected SettingsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "Setting not found"; }
      }
   }
}