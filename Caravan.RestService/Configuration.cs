using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Finsa.Caravan.RestService
{
   public static class Configuration
   {
      private static readonly NameValueCollection AppSettings = ConfigurationManager.AppSettings;

      public static int ShortCacheTimeoutInSeconds
      {
         get { return Convert.ToInt32(AppSettings["crvn:ShortCacheTimeoutInSeconds"]); }
      }

      public static int MediumCacheTimeoutInSeconds
      {
         get { return Convert.ToInt32(AppSettings["crvn:MediumCacheTimeoutInSeconds"]); }
      }

      public static int LongCacheTimeoutInSeconds
      {
         get { return Convert.ToInt32(AppSettings["crvn:LongCacheTimeoutInSeconds"]); }
      }
   }
}