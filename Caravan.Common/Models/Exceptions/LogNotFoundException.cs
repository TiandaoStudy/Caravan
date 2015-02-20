using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.DataModel.Exceptions
{
    [Serializable]
    public class LogNotFoundException:Exception
    {

      public LogNotFoundException() : base(TheMessage)
      {
      }

      public LogNotFoundException(string message) : base(message)
      {
      }

      public LogNotFoundException(string message, Exception inner) : base(message, inner)
      {
      }

      protected LogNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
      {
      }

      public static string TheMessage
      {
         get { return "Log not found"; }
      }
    }
}
