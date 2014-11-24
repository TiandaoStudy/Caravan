using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Finsa.Caravan.DataModel.Security
{
    public class SessionTracker
    {
        private string _userHostName;
        private string _userHostAddress;
        private string _userName;
        private DateTime _lastVisit;

        public string UserHostAddress
        {
            get
            {
                return this._userHostAddress;
            }
        }
        public string UserHostName
        {
            get
            {
                return this._userHostName;
            }
        }
        public DateTime LastVisit
        {
            get
            {
                return this._lastVisit;
            }
            set
            {
                this._lastVisit = value;
            }
        }
        public string Login
        {
            get
            {
                return this._userName;
            }
            set
            {
                this._userName = value;
            }
        }

        public void FillData() 
        {
            this._userHostAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            this._userHostName = HttpContext.Current.Request.UserHostName.ToString();
            this._lastVisit = DateTime.Now;
        }
    }
}
