using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Security
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
                return (Id.GetHashCode() * 397) ^ AppId.GetHashCode();
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
}