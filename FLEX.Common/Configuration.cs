using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using PommaLabs.KVLite;

namespace FLEX.Common
{
   /// <summary>
   ///   TODO
   /// </summary>
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "FlexCommonConfiguration";
      private const string CachePartitionName = "FLEX.Common";
      private const string ApplicationNameKey = "ApplicationName";
      private const string BufferPoolCountForBufferedIOKey = "BufferPoolCountForBufferedIO";
      private const string BufferSizeInBytesForBufferedIOKey = "BufferSizeInBytesForBufferedIO";
      private const string ConnectionStringKey = "ConnectionString";
      private const string DbLoggerTypeInfoKey = "DbLoggerTypeInfo";
      private const string QueryExecutorTypeInfoKey = "QueryExecutorTypeInfo";

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

      [ConfigurationProperty(DbLoggerTypeInfoKey, IsRequired = true)]
      public string DbLoggerTypeInfo
      {
         get { return (string) this[DbLoggerTypeInfoKey]; }
      }

      [ConfigurationProperty(QueryExecutorTypeInfoKey, IsRequired = true)]
      public string QueryExecutorTypeInfo
      {
         get { return (string) this[QueryExecutorTypeInfoKey]; }
      }

      public string ConnectionString
      {
         get { return (string) PersistentCache.DefaultInstance.Get(CachePartitionName, ConnectionStringKey); }
         set { PersistentCache.DefaultInstance.AddStatic(CachePartitionName, ConnectionStringKey, value); }
      }

      #region Utilities

      private static readonly List<string> EmptyStringListInstance = new List<string>();

      public static List<string> EmptyStringList
      {
         get { return EmptyStringListInstance; }
      }

      public static string MapPath(params string[] hints)
      {
         return hints.Aggregate(AppDomain.CurrentDomain.BaseDirectory, Path.Combine);
      }

      #endregion
   }
}