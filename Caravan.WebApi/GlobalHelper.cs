using AutoMapper;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.Mvc.Core.Models.Security;
using Newtonsoft.Json;
using System.Web.Http;

namespace Finsa.Caravan.Mvc.Core
{
    public sealed class GlobalHelper
    {
        public static void Application_Start()
        {
            // Personalizzo le impostazioni del serializzatore JSON.
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Linked object mappings.
            Mapper.CreateMap<SecApp, LinkedSecApp>();
        }
    }
}