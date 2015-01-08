using System;
using System.Web;

namespace Finsa.Caravan.Common.DataModel.Security
{
   public class SessionTracker
   {
      private DateTime _lastVisit;
      private string _userHostAddress;
      private string _userHostName;
      private string _userName;

      public string UserHostAddress
      {
         get { return _userHostAddress; }
      }

      public string UserHostName
      {
         get { return _userHostName; }
      }

      public DateTime LastVisit
      {
         get { return _lastVisit; }
         set { _lastVisit = value; }
      }

      public string Login
      {
         get { return _userName; }
         set { _userName = value; }
      }

      public void FillData()
      {
         _userHostAddress = HttpContext.Current.Request.UserHostAddress;
         _userHostName = HttpContext.Current.Request.UserHostName;
         _lastVisit = DateTime.Now;
      }
   }
}