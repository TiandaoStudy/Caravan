using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.DataModel.Exceptions
{
   [Serializable]
   public class SettingsExistingException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public SettingsExistingException():base(TheMessage)
      {
      }

      public SettingsExistingException(string message) : base(message)
      {
      }

      public SettingsExistingException(string message, Exception inner) : base(message, inner)
      {
      }

      protected SettingsExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "Setting already existing"; }
      }
   }
}