using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.DataModel.Exceptions
{
   [Serializable]
   public class EntryExistingException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public EntryExistingException()
      {
      }

      public EntryExistingException(string message) : base(message)
      {
      }

      public EntryExistingException(string message, Exception inner) : base(message, inner)
      {
      }

      protected EntryExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}
