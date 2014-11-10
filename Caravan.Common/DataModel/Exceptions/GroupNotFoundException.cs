using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.DataModel.Exceptions
{
   [Serializable]
   public class GroupNotFoundException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public GroupNotFoundException()
      {
      }

      public GroupNotFoundException(string message) : base(message)
      {
      }

      public GroupNotFoundException(string message, Exception inner) : base(message, inner)
      {
      }

      protected GroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}