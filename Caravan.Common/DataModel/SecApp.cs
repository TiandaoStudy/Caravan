using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecApp
   {
      [JsonProperty(Order = 0)]
      public long Id { get; set; }
      
      [JsonProperty(Order = 1)]
      public string Name { get; set; }
      
      [JsonProperty(Order = 2)]
      public string Description { get; set; }
      
      [JsonProperty(Order = 3)]
      public virtual ICollection<SecUser> Users { get; set; }
      
      [JsonProperty(Order = 4)]
      public virtual ICollection<SecGroup> Groups { get; set; }
      
      [JsonProperty(Order = 5)]
      public virtual ICollection<SecContext> Contexts { get; set; }
         
      [JsonProperty(Order = 6)]
      public virtual ICollection<LogSettings> LogSettings { get; set; }

      [JsonIgnore]
      public virtual ICollection<LogEntry> LogEntries { get; set; } 
   }
}
