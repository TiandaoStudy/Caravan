using System;
using System.IO;

namespace Finsa.Caravan
{
   /// <summary>
   ///   TODO
   /// </summary>
   public static class CEnvironment
   {
      public static bool AppIsRunningOnAspNet
      {
         get { return "web.config".Equals(Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile), StringComparison.OrdinalIgnoreCase); }
      }
   }
}
