using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.DataModel.Exceptions
{
   [Serializable]
   public class SettingExistingException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public SettingExistingException() : base(TheMessage)
      {
      }

      public SettingExistingException(string message) : base(message)
      {
      }

      public SettingExistingException(string message, Exception inner) : base(message, inner)
      {
      }

      protected SettingExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "Setting already existing"; }
      }
   }
}