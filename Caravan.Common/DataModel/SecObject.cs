using System;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecObject
   {
      [JsonProperty(Order = 0)]
      public long Id { get; set; }
      
      [JsonIgnore]
      public long ContextId { get; set; }
      
      [JsonIgnore]
      public SecContext Context { get; set; }

      [JsonIgnore]
      public long AppId { get; set; }
      
      [JsonIgnore]
      public SecApp App { get; set; }
      
      [JsonProperty(Order = 1)]
      public string Name { get; set; }
      
      [JsonProperty(Order = 2)]
      public string Description { get; set; }
      
      [JsonProperty(Order = 3)]
      public string Type { get; set; }
   }
}
