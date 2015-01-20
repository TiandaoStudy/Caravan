﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.DataModel.Security
{
   [Serializable,DataContract(IsReference = true)]
   public class SecUser : IEquatable<SecUser>
   {
      [JsonProperty(Order = 0), DataMember(Order = 0)]
      public long Id { get; set; }

      [JsonIgnore]
      public long AppId { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public SecApp App { get; set; }

      [JsonProperty(Order = 1), DataMember(Order = 1)]
      public int Active { get; set; }

      [JsonProperty(Order = 2), DataMember(Order = 2)]
      public string Login { get; set; }

      [JsonProperty(Order = 3), DataMember(Order = 3)]
      public string HashedPassword { get; set; }

      [JsonProperty(Order = 4), DataMember(Order = 4)]
      public string FirstName { get; set; }

      [JsonProperty(Order = 5), DataMember(Order = 5)]
      public string LastName { get; set; }

      [JsonProperty(Order = 6), DataMember(Order = 6)]
      public string Email { get; set; }

      [JsonProperty(Order = 7), DataMember(Order = 7)]
      public virtual ICollection<SecGroup> Groups { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public virtual ICollection<SecEntry> SecEntries { get; set; }

      #region Equality Members

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
         if (obj.GetType() != GetType()) return false;
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

      #endregion
   }

   [Serializable,DataContract(IsReference = true)]
   public class SecUserSingle : IEquatable<SecUserSingle>
   {
      [DataMember]
      public SecUser User { get; set; }

      #region Equality Members

      public bool Equals(SecUserSingle other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return User.Equals(other.User);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecUserSingle) obj);
      }

      public override int GetHashCode()
      {
         return User.GetHashCode();
      }

      public static bool operator ==(SecUserSingle left, SecUserSingle right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecUserSingle left, SecUserSingle right)
      {
         return !Equals(left, right);
      }

      #endregion
   }

   [Serializable,DataContract(IsReference = true)]
   public class SecUserList : IEquatable<SecUserList>
   {
      [DataMember]
      public IEnumerable<SecUser> Users { get; set; }

      #region Equality Members

      public bool Equals(SecUserList other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Users.Equals(other.Users);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecUserList) obj);
      }

      public override int GetHashCode()
      {
         return Users.GetHashCode();
      }

      public static bool operator ==(SecUserList left, SecUserList right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecUserList left, SecUserList right)
      {
         return !Equals(left, right);
      }

      #endregion
   }
}