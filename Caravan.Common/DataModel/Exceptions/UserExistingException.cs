﻿using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.DataModel.Exceptions
{
   [Serializable]
   public class UserExistingException : Exception
   {
      //
      // For guidelines regarding the creation of new exception types, see
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
      // and
      //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
      //

      public UserExistingException() : base(TheMessage)
      {
      }

      public UserExistingException(string message) : base(TheMessage)
      {
      }

      public UserExistingException(Exception inner) : base(TheMessage, inner)
      {
      }

      protected UserExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "User already existing in the current application"; }
      }
   }
}