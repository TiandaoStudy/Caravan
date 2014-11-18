using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.DataModel.Exceptions
{
   [Serializable]
   public class GroupExistingException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public GroupExistingException()
      {
      }

      public GroupExistingException(string message) : base(message)
      {
      }

      public GroupExistingException(string message, Exception inner) : base(message, inner)
      {
      }

      protected GroupExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}