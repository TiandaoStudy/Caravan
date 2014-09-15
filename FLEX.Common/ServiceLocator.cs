using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Reflection;
using FLEX.Common.Data;
using PommaLabs.GRAMPA;
using PommaLabs.GRAMPA.Diagnostics;

namespace FLEX.Common
{
   public sealed class ServiceLocator
   {
      private static readonly ConcurrentDictionary<Type, object> InstanceCache;

      static ServiceLocator()
      {
         try
         {
            InstanceCache = new ConcurrentDictionary<Type, object>();
         }
         catch (Exception ex)
         {
            DbLogger.Instance.LogError<ServiceLocator>(ex);
            // It's better NOT to rethrow in static constructors, 
            // otherwise the application will silently die. 
         }
      }

      public static T Load<T>(string configurationEntry) where T : class
      {
         try
         {
            // If cache contains an instance of the type, we return it.
            var type = typeof (T);
            object instance;
            if (InstanceCache.TryGetValue(type, out instance))
            {
               return instance as T;
            }

            // We read the type settings from the Web.config:
            //  * info.First contains the assembly name;
            //  * info.Second contains the class name.
            var info = FindConfigurationInfo(configurationEntry);

            // At first, we create the relative path for our DLL.
            var assemblyPath = String.Format("bin/{0}.dll", info.First);

            // And then we make it absolute to our server.
            assemblyPath = CommonSettings.MapPath(assemblyPath);
            var assembly = Assembly.LoadFrom(assemblyPath);

            // We load the class and we make sure it implements T.
            var klass = assembly.GetType(info.Second);
            Raise<ConfigurationException>.IfIsNotContainedIn(type, klass.GetInterfaces());
            instance = Activator.CreateInstance(klass);

            // We store the executor instance inside the cache, and then we return it.
            InstanceCache.TryAdd(type, instance);
            return instance as T;
         }
         catch (Exception ex)
         {
            DbLogger.Instance.LogError<ServiceLocator>(ex);
            throw;
         }
      }

      private static GPair<string, string> FindConfigurationInfo(string configurationEntry)
      {
         Raise<ConfigurationException>.IfIsEmpty(configurationEntry);
         var split = configurationEntry.Split(';');
         Raise<ConfigurationException>.IfIsEmpty(split[0]);
         Raise<ConfigurationException>.IfIsEmpty(split[1]);
         return GPair.Create(split[0], split[1]);
      }
   }
}
