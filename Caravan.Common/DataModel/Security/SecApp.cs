﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Finsa.Caravan.Common.DataModel.Logging;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.DataModel.Security
{
   [Serializable, DataContract(IsReference = true)]
   public class SecApp : IEquatable<SecApp>
   {
      [JsonProperty(Order = 0), DataMember(Order = 0)]
      public long Id { get; set; }

      [JsonProperty(Order = 1), DataMember(Order = 1)]
      public string Name { get; set; }

      [JsonProperty(Order = 2), DataMember(Order = 2)]
      public string Description { get; set; }

      [JsonProperty(Order = 3), DataMember(Order = 3)]
      public virtual ICollection<SecUser> Users { get; set; }

      [JsonProperty(Order = 4), DataMember(Order = 4)]
      public virtual ICollection<SecGroup> Groups { get; set; }

      [JsonProperty(Order = 5), DataMember(Order = 5)]
      public virtual ICollection<SecContext> Contexts { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public virtual ICollection<SecObject> Objects { get; set; }

      [JsonProperty(Order = 6), DataMember(Order = 6)]
      public virtual ICollection<LogSettings> LogSettings { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public virtual ICollection<LogEntry> LogEntries { get; set; }

      [JsonIgnore, IgnoreDataMember]
      public virtual ICollection<SecEntry> SecEntries { get; set; }

      public bool Equals(SecApp other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Id == other.Id;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecApp) obj);
      }

      public override int GetHashCode()
      {
         return Id.GetHashCode();
      }

      public static bool operator ==(SecApp left, SecApp right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecApp left, SecApp right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable]
   public class SecAppSingle : IEquatable<SecAppSingle>
   {
      public SecApp App { get; set; }

      public bool Equals(SecAppSingle other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return App.Equals(other.App);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecAppSingle) obj);
      }

      public override int GetHashCode()
      {
         return App.GetHashCode();
      }

      public static bool operator ==(SecAppSingle left, SecAppSingle right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecAppSingle left, SecAppSingle right)
      {
         return !Equals(left, right);
      }
   }

   [Serializable]
   public class SecAppList : IEquatable<SecAppList>
   {
      public IEnumerable<SecApp> Apps { get; set; }

      public bool Equals(SecAppList other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return Apps.Equals(other.Apps);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((SecAppList) obj);
      }

      public override int GetHashCode()
      {
         return Apps.GetHashCode();
      }

      public static bool operator ==(SecAppList left, SecAppList right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(SecAppList left, SecAppList right)
      {
         return !Equals(left, right);
      }
   }
}