﻿using System;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecEntry
   {
      public long Id { get; set; }

      public long AppId { get; set; }

      public SecApp App { get; set; }

      public long? UserId { get; set; }

      public SecUser User { get; set; }

      public long? GroupId { get; set; }

      public SecGroup Group { get; set; }

      public long ContextId { get; set; }

      public SecContext Context { get; set; }

      public long ObjectId { get; set; }

      public SecObject Object { get; set; }
   }
}
