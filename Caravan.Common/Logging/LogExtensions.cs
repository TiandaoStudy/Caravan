using Finsa.Caravan.Common.Core;
using Finsa.Caravan.Common.Utilities;

namespace Finsa.Caravan.Common.Logging
{
    public static class LogExtensions
    {
        /// <summary>
        ///   Converts given object in a very compact JSON format. If given object is null, an empty
        ///   string is returned.
        /// </summary>
        /// <typeparam name="TObj">The type of the object. Used to avoid boxing.</typeparam>
        /// <param name="obj">The object that should be converted.</param>
        /// <returns>A very compact JSON corresponding to given object.</returns>
        public static string LogAsJson<TObj>(this TObj obj)
        {
            return ReferenceEquals(obj, null) ? Constants.EmptyString : ServiceProvider.JsonSerializer.SerializeObject(obj);
        }
    }
}
