using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using PommaLabs.KVLite.Web;
using Thrower;

namespace FLEX.Web
{
   public static class CacheManager
   {
      private const string KeySeparator = "$";

      private static readonly CacheDependency NoCacheDependencies = null;

      public static void ClearCache()
      {
         // We make a list of cache content, since it may change during cleanup.
         var entries = (from e in HttpRuntime.Cache.Cast<DictionaryEntry>()
                        let k = e.Key.ToString()
                        where KeyCanBeDeleted(k)
                        select k).ToList();

         // We remove all valid keys from the cache.
         foreach (var rowKey in entries)
         {
            HttpRuntime.Cache.Remove(rowKey);
         }
      }

      public static void StoreValueForKey<TValue>(TValue value, object key1, params object[] key2)
      {
         StoreTimedValueForKey(value, WebSettings.CacheManager_DefaultMinutes, key1, key2);
      }

      public static void StoreSlidingValueForKey<TValue>(TValue value, object key1, params object[] key2)
      {
         StoreSlidingTimedValueForKey(value, WebSettings.CacheManager_DefaultMinutes, key1, key2);
      }

      public static void StoreTimedValueForKey<TValue>(TValue value, int minutes, object key1, params object[] key2)
      {
         Raise<ArgumentOutOfRangeException>.If(minutes <= 0);
         var aggrKey = CreateAggregateKey(key1, key2);
         var absoluteExpiration = DateTime.Now.AddMinutes(minutes);
         // We use Cache.NoSlidingExpiration because we are setting an absolute expiration.
         HttpRuntime.Cache.Insert(aggrKey, value, NoCacheDependencies, absoluteExpiration, Cache.NoSlidingExpiration);
      }

      public static void StoreSlidingTimedValueForKey<TValue>(TValue value, int minutes, object key1, params object[] key2)
      {
         Raise<ArgumentOutOfRangeException>.If(minutes <= 0);
         var aggrKey = CreateAggregateKey(key1, key2);
         //var absoluteExpiration = DateTime.Now.AddMinutes(minutes);
         // We use Cache.NoSlidingExpiration because we are setting an absolute expiration.
         //HttpRuntime.Cache.Insert(aggrKey, value, NoCacheDependencies, absoluteExpiration, Cache.NoSlidingExpiration);
         HttpRuntime.Cache.Insert(aggrKey, value, NoCacheDependencies, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes), CacheItemPriority.Default, null);
      }

      public static bool TryRetrieveValueForKey<TValue>(out TValue value, object key1, params object[] key2)
      {
         var aggrKey = CreateAggregateKey(key1, key2);
         var maybeValue = HttpRuntime.Cache[aggrKey];
         if (maybeValue == null)
         {
            // Cache does not contain given key.
            value = default(TValue);
            return false;
         }
         value = (TValue) maybeValue;
         return true;
      }

      /// <summary>
      ///   
      /// </summary>
      /// <param name="key1"></param>
      /// <param name="key2"></param>
      /// <returns></returns>
      /// <remarks>
      ///   Method is marked as internal in order to allow unit testing.
      /// </remarks>
      internal static string CreateAggregateKey(object key1, params object[] key2)
      {
         // Small optimization if we have only one key.
         if (key2.Length == 0)
         {
            return key1.ToString();
         }
         // Otherwise, we need to concatenate all strings...
         var builder = new StringBuilder(key1.ToString());
         foreach (var k in key2)
         {
            builder.Append(KeySeparator);
            builder.Append(k);
         }
         // And then generate an hash from the unified strings.
         return builder.ToString();
      }

      private static bool KeyCanBeDeleted(string key)
      {
         const string viewStateId = BaseStatePersister.HiddenFieldName;

         // Key must not be a view state or a control state.
         if (key.StartsWith(viewStateId))
         {
            return false;
         }

         // It's safe to remove the key.
         return true;
      }
   }
}