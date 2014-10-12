using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace FLEX.Common
{
   /// <summary>
   ///   TODO
   /// </summary>
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "FlexCommonConfiguration";
      private const string ApplicationNameKey = "ApplicationName";
      private const string BufferPoolCountForBufferedIOKey = "BufferPoolCountForBufferedIO";
      private const string BufferSizeInBytesForBufferedIOKey = "BufferSizeInBytesForBufferedIO";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      /// <summary>
      ///   TODO
      /// </summary>
      public static Configuration Instance
      {
         get { return CachedInstance; }
      }

      [ConfigurationProperty(ApplicationNameKey, IsRequired = true)]
      public string ApplicationName
      {
         get { return (string) this[ApplicationNameKey]; }
      }

      [ConfigurationProperty(BufferPoolCountForBufferedIOKey, IsRequired = false, DefaultValue = 16)]
      public int BufferPoolCountForBufferedIO
      {
         get { return (int) this[BufferPoolCountForBufferedIOKey]; }
      }

      [ConfigurationProperty(BufferSizeInBytesForBufferedIOKey, IsRequired = false, DefaultValue = 512)]
      public int BufferSizeInBytesForBufferedIO
      {
         get { return (int) this[BufferSizeInBytesForBufferedIOKey]; }
      }

      #region Utilities

      public static string MapPath(params string[] hints)
      {
         return hints.Aggregate(AppDomain.CurrentDomain.BaseDirectory, Path.Combine);
      }

      #endregion
   }
}