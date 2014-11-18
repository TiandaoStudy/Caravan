using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecEntry : IEquatable<SecEntry>
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

   [Serializable]
   public class SecEntrySingle : IEquatable<SecEntrySingle>
   {
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

   [Serializable]
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
