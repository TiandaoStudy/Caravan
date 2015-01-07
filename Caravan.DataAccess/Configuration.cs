using System;
using System.Configuration;
using Finsa.Caravan.DataAccess.Properties;
using PommaLabs.KVLite;

namespace Finsa.Caravan.DataAccess
{
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "Finsa.Caravan.DataAccess";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance
      {
         get { return CachedInstance; }
      }


      #region Internal Settings

      private string _currentAppName;

      /// <summary>
      ///   Needed because the REST service has to "act" as a proxy for apps.
      /// </summary>
      internal string CurrentAppName
      {
         get { return _currentAppName ?? Common.Properties.Settings.Default.ApplicationName; }
         set { _currentAppName = value.ToLower(); }
      }

      #endregion
   }
}