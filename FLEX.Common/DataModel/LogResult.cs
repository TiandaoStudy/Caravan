﻿using System;

namespace FLEX.Common.DataModel
{
   [Serializable]
   public class LogResult
   {
      public static readonly LogResult Successful = new LogResult {Succeeded = true};

      public bool Succeeded { get; set; }
      public Exception Exception { get; set; }
   }
}
