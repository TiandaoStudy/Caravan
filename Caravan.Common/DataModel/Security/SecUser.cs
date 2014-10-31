using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecUser : IEquatable<SecUser>
   {
      public bool Equals(SecUser other)
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
         return Equals((SecUser) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (Id.GetHashCode()*397) ^ AppId.GetHashCode();
         }
      }

      public static bool operator ==(SecUser left, SecUser right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecUser left, SecUser right)
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
      public bool Active { get; set; }
      
      [JsonProperty(Order = 2)]
      public string Login { get; set; }
      
      [JsonProperty(Order = 3)]
      public string HashedPassword { get; set; }
      
      [JsonProperty(Order = 4)]
      public string FirstName { get; set; }
      
      [JsonProperty(Order = 5)]
      public string LastName { get; set; }
      
      [JsonProperty(Order = 6)]
      public string Email { get; set; }
      
      [JsonProperty(Order = 7)]
      public virtual ICollection<SecGroup> Groups { get; set; }

      [JsonIgnore]
      public virtual ICollection<SecEntry> SecEntries { get; set; }
   }
}
