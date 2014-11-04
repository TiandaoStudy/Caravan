using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecGroup : IEquatable<SecGroup>
   {
      public bool Equals(SecGroup other)
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
         return Equals((SecGroup) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (Id.GetHashCode()*397) ^ AppId.GetHashCode();
         }
      }

      public static bool operator ==(SecGroup left, SecGroup right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecGroup left, SecGroup right)
      {
         return !Equals(left, right);
      }

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
      public int IsAdmin { get; set; }
      
      [JsonProperty(Order = 4)]
      public virtual ICollection<SecUser> Users { get; set; }

      [JsonIgnore]
      public virtual ICollection<SecEntry> SecEntries { get; set; }
   }

   [Serializable]
   public class SecGroupSingle
   {
      public SecGroup Group { get; set; } 
   }

   [Serializable]
   public class SecGroupList
   {
      public IEnumerable<SecGroup> Groups { get; set; } 
   }
}
