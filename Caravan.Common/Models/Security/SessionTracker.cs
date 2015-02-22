using System;
using System.Runtime.Serialization;
using System.Web;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, DataContract]
    public class SessionTracker
    {
        public string UserHostAddress { get; private set; }

        public string UserHostName { get; private set; }

        public DateTime LastVisit { get; set; }

        public string Login { get; set; }

        public void FillData()
        {
            if (HttpContext.Current == null)
            {
                UserHostAddress = UserHostName = "unknown";
            }
            else
            {
                UserHostAddress = HttpContext.Current.Request.UserHostAddress == "::1" ? "localhost" : HttpContext.Current.Request.UserHostAddress;
                UserHostName = HttpContext.Current.Request.UserHostName == "::1" ? "localhost" : HttpContext.Current.Request.UserHostAddress;
            }
            LastVisit = DateTime.Now;
        }
    }
}