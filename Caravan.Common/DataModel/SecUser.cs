using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecUser
   {
      public long Id { get; set; }

      [JsonIgnore]
      public long AppId { get; set; }

      public SecApp App { get; set; }

      public bool Active { get; set; }

      public string Login { get; set; }

      public string HashedPassword { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Email { get; set; }

      //public virtual ICollection<SecGroup> Groups { get; set; }
   }
}
