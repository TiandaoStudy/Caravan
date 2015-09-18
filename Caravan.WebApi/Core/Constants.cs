namespace Finsa.Caravan.WebApi.Core
{
    static class Constants
    {
        /// <summary>
        ///   ID usato come correlazione nei log.
        /// </summary>
        public const string RequestId = "request_id";

        /// <summary>
        ///   Header usato per facilitare il tracciamento dei log.
        /// </summary>
        public const string RequestIdHeader = "x-caravan-request-id";
    }
}
