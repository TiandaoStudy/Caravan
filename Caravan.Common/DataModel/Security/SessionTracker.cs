using System;
using System.Runtime.Serialization;
using System.Web;

namespace Finsa.Caravan.Common.DataModel.Security
{
   [Serializable, DataContract(IsReference = true)]
   public class SessionTracker
   {
      public string UserHostAddress { get; private set; }

      public string UserHostName { get; private set; }

      public DateTime LastVisit { get; set; }

      public string Login { get; set; }

      public void FillData()
      {
         UserHostAddress = HttpContext.Current.Request.UserHostAddress;
         UserHostName = HttpContext.Current.Request.UserHostName;
         LastVisit = DateTime.Now;
      }
   }
}