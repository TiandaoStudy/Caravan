using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecApp
   {
      public long Id { get; set; }

      public string Name { get; set; }

      public string Description { get; set; }

      public virtual ICollection<SecUser> Users { get; set; }
 
      //public virtual ICollection<SecGroup> Groups { get; set; }

      //public virtual ICollection<LogSettings> LogSettings { get; set; }

      //[JsonIgnore]
      //public virtual ICollection<LogEntry> LogEntries { get; set; } 
   }
}
