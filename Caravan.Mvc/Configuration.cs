using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finsa.Caravan.Mvc
{
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "Finsa.Caravan.Mvc";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance
      {
         get { return CachedInstance; }
      }


   }
}
