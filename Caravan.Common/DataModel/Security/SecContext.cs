using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.DataModel.Security
{
   [Serializable, DataContract(IsReference = true)]
   public class SecContext : IEquatable<SecContext>
   {
      [JsonProperty(Order = 0), DataMember(Order = 0)]
      public long Id { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public long AppId { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public SecApp App { get; set; }

      [JsonProperty(Order = 1), DataMember(Order = 1)]
      public string Name { get; set; }

      [JsonProperty(Order = 2), DataMember(Order = 2)]
      public string Description { get; set; }

      [JsonProperty(Order = 3), DataMember(Order = 3)]
      public virtual ICollection<SecObject> Objects { get; set; }

      [JsonIgnore, IgnoreDataMember]
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
         if (obj.GetType() != GetType()) return false;
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

   [Serializable,DataContract(IsReference = true)]
   public class SecContextSingle : IEquatable<SecContextSingle>
   {
      [DataMember]
      public SecContext Context { get; set; }

      public bool Equals(SecContextSingle other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Context.Equals(other.Context);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecContextSingle) obj);
      }

      public override int GetHashCode()
      {
         return Context.GetHashCode();
      }

      public static bool operator ==(SecContextSingle left, SecContextSingle right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecContextSingle left, SecContextSingle right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable,DataContract(IsReference = true)]
   public class SecContextList : IEquatable<SecContextList>
   {
      [DataMember]
      public IEnumerable<SecContext> Contexts { get; set; }

      public bool Equals(SecContextList other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Contexts.Equals(other.Contexts);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecContextList) obj);
      }

      public override int GetHashCode()
      {
         return Contexts.GetHashCode();
      }

      public static bool operator ==(SecContextList left, SecContextList right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecContextList left, SecContextList right)
      {
         return !Equals(left, right);
      }
   }
}