using System;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class LogResult
   {
      public static readonly LogResult Success = new LogResult {Succeeded = true};

      public static LogResult Failure(string cause)
      {
         return new LogResult {Succeeded = false, Exception = new Exception(cause)};
      }

      public static LogResult Failure(Exception ex)
      {
         return new LogResult {Succeeded = false, Exception = ex};
      }
      
      [JsonProperty(Order = 0)]
      public bool Succeeded { get; set; }
      
      [JsonProperty(Order = 1)]
      public Exception Exception { get; set; }
   }
}
