﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.DataModel.Security
{
   [Serializable, DataContract(IsReference = true)]
   public class SecGroup : IEquatable<SecGroup>
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
      public int IsAdmin { get; set; }

      [JsonProperty(Order = 4), DataMember(Order = 4)]
      public string Notes { get; set; }

      [JsonProperty(Order = 5), DataMember(Order = 5)]
      public virtual ICollection<SecUser> Users { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public virtual ICollection<SecEntry> SecEntries { get; set; }

      #region Equality Members

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
         if (obj.GetType() != GetType()) return false;
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

      #endregion
   }

   [Serializable, DataContract(IsReference = true)]
   public class SecGroupSingle : IEquatable<SecGroupSingle>
   {
      [DataMember]
      public SecGroup Group { get; set; }

      public bool Equals(SecGroupSingle other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Group.Equals(other.Group);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecGroupSingle) obj);
      }

      public override int GetHashCode()
      {
         return Group.GetHashCode();
      }

      public static bool operator ==(SecGroupSingle left, SecGroupSingle right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecGroupSingle left, SecGroupSingle right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable, DataContract(IsReference = true)]
   public class SecGroupList : IEquatable<SecGroupList>
   {
      [DataMember]
      public IEnumerable<SecGroup> Groups { get; set; }

      public bool Equals(SecGroupList other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Groups.Equals(other.Groups);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecGroupList) obj);
      }

      public override int GetHashCode()
      {
         return Groups.GetHashCode();
      }

      public static bool operator ==(SecGroupList left, SecGroupList right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecGroupList left, SecGroupList right)
      {
         return !Equals(left, right);
      }
   }
}