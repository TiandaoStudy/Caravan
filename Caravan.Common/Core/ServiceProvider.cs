using Finsa.Caravan.Common.Serialization;

namespace Finsa.Caravan.Common.Core
{
    internal static class ServiceProvider
    {
        private static readonly IJsonSerializer CachedJsonSerializer = new JsonNetSerializer();

        public static IJsonSerializer JsonSerializer
        {
            get { return CachedJsonSerializer; }
        }
    }
}