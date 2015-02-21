using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract(IsReference = true)]
    public class SecObject : IEquatable<SecObject>
    {
        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public long Id { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public long ContextId { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public SecContext Context { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public long AppId { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public SecApp App { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public string Description { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public string Type { get; set; }

        [JsonIgnore, IgnoreDataMember]
        public virtual ICollection<SecEntry> SecEntries { get; set; }

        public bool Equals(SecObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && ContextId == other.ContextId && AppId == other.AppId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SecObject) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ ContextId.GetHashCode();
                hashCode = (hashCode * 397) ^ AppId.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(SecObject left, SecObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SecObject left, SecObject right)
        {
            return !Equals(left, right);
        }
    }

    [Serializable, DataContract(IsReference = true)]
    public class SecObjectSingle : IEquatable<SecObjectSingle>
    {
        [DataMember]
        public SecObject Object { get; set; }

        public bool Equals(SecObjectSingle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Object.Equals(other.Object);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SecObjectSingle) obj);
        }

        public override int GetHashCode()
        {
            return Object.GetHashCode();
        }

        public static bool operator ==(SecObjectSingle left, SecObjectSingle right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SecObjectSingle left, SecObjectSingle right)
        {
            return !Equals(left, right);
        }
    }

    [Serializable, DataContract(IsReference = true)]
    public class SecObjectList : IEquatable<SecObjectList>
    {
        [DataMember]
        public IEnumerable<SecObject> Objects { get; set; }

        public bool Equals(SecObjectList other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Objects.Equals(other.Objects);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SecObjectList) obj);
        }

        public override int GetHashCode()
        {
            return Objects.GetHashCode();
        }

        public static bool operator ==(SecObjectList left, SecObjectList right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SecObjectList left, SecObjectList right)
        {
            return !Equals(left, right);
        }
    }
}