using System;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Models.Security
{
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class SecSession
    {
        public string UserLogin { get; set; }

        public string UserHostAddress { get; private set; }

        public string UserHostName { get; private set; }

        public DateTime LastVisit { get; set; }

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