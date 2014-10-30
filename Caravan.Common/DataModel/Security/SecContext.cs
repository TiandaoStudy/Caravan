using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecContext
   {
      [JsonProperty(Order = 0)]
      public long Id { get; set; }
      
      [JsonIgnore]
      public long AppId { get; set; }
      
      [JsonIgnore]
      public SecApp App { get; set; }
      
      [JsonProperty(Order = 1)]
      public string Name { get; set; }
      
      [JsonProperty(Order = 2)]
      public string Description { get; set; }

      [JsonProperty(Order = 3)]
      public virtual ICollection<SecObject> Objects { get; set; }
      
      [JsonIgnore]
      public virtual ICollection<SecEntry> SecEntries { get; set; } 
   }
}
