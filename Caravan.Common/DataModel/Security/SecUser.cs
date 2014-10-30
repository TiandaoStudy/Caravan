using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecUser
   {
      [JsonProperty(Order = 0)]
      public long Id { get; set; }

      [JsonIgnore]
      public long AppId { get; set; }
      
      [JsonIgnore]
      public SecApp App { get; set; }
      
      [JsonProperty(Order = 1)]
      public bool Active { get; set; }
      
      [JsonProperty(Order = 2)]
      public string Login { get; set; }
      
      [JsonProperty(Order = 3)]
      public string HashedPassword { get; set; }
      
      [JsonProperty(Order = 4)]
      public string FirstName { get; set; }
      
      [JsonProperty(Order = 5)]
      public string LastName { get; set; }
      
      [JsonProperty(Order = 6)]
      public string Email { get; set; }
      
      [JsonProperty(Order = 7)]
      public virtual ICollection<SecGroup> Groups { get; set; }
   }
}
