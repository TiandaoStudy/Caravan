﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecContext : IEquatable<SecContext>
   {
      [JsonProperty(Order = 0)]
      public long Id { get; set; }

      [JsonIgnore]
      public long AppId { get; set; }

      [JsonIgnore]
      public SecApp App { get; set; }

      [JsonProperty(Order = 1)]
      public string Name { get; set; }

      [JsonProperty(Order = 2)]
      public string Description { get; set; }

      [JsonProperty(Order = 3)]
      public virtual ICollection<SecObject> Objects { get; set; }

      [JsonIgnore]
      public virtual ICollection<SecEntry> SecEntries { get; set; } 

      public bool Equals(SecContext other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Id == other.Id && AppId == other.AppId;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != this.GetType()) return false;
         return Equals((SecContext) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (Id.GetHashCode()*397) ^ AppId.GetHashCode();
         }
      }

      public static bool operator ==(SecContext left, SecContext right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecContext left, SecContext right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable]
   public class SecContextSingle
   {
      public SecContext Context { get; set; }
   }

   [Serializable]
   public class SecContextList
   {
      public IEnumerable<SecContext> Contexts { get; set; }
   }
}
