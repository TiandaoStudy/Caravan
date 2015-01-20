using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.DataModel.Security
{
   [Serializable, DataContract(IsReference = true)]
   public class SecEntry : IEquatable<SecEntry>
   {
      [DataMember]
      public long Id { get; set; }

       [DataMember]
      public long AppId { get; set; }

       [DataMember]
      public SecApp App { get; set; }

       [DataMember]
      public long? UserId { get; set; }

       [DataMember]
      public SecUser User { get; set; }

       [DataMember]
      public long? GroupId { get; set; }

       [DataMember]
      public SecGroup Group { get; set; }

       [DataMember]
      public long ContextId { get; set; }

       [DataMember]
      public SecContext Context { get; set; }

       [DataMember]
      public long ObjectId { get; set; }

       [DataMember]
      public SecObject Object { get; set; }

      public bool Equals(SecEntry other)
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
         return Equals((SecEntry) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (Id.GetHashCode()*397) ^ AppId.GetHashCode();
         }
      }

      public static bool operator ==(SecEntry left, SecEntry right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecEntry left, SecEntry right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable,DataContract(IsReference = true)]
   public class SecEntrySingle : IEquatable<SecEntrySingle>
   {
      [DataMember]
      public SecEntry Entry { get; set; }

      public bool Equals(SecEntrySingle other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Entry.Equals(other.Entry);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecEntrySingle) obj);
      }

      public override int GetHashCode()
      {
         return Entry.GetHashCode();
      }

      public static bool operator ==(SecEntrySingle left, SecEntrySingle right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecEntrySingle left, SecEntrySingle right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable,DataContract(IsReference = true)]
   public class SecEntryList : IEquatable<SecEntryList>
   {
      public IEnumerable<SecEntry> Entries { get; set; }

      public bool Equals(SecEntryList other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Entries.Equals(other.Entries);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecEntryList) obj);
      }

      public override int GetHashCode()
      {
         return Entries.GetHashCode();
      }

      public static bool operator ==(SecEntryList left, SecEntryList right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecEntryList left, SecEntryList right)
      {
         return !Equals(left, right);
      }
   }
}