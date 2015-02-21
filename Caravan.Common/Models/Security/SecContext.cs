using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Models.Security
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
                return (Id.GetHashCode() * 397) ^ AppId.GetHashCode();
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
}