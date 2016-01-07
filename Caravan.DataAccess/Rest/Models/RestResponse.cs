using Finsa.Caravan.Common.Logging.Models;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Finsa.Caravan.DataAccess.Rest.Models
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class RestResponse<TBody>
    {
        [JsonProperty(Order = 0)]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty(Order = 1)]
        public TBody Body { get; set; }
    }

    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class FailureBody
    {
        [JsonProperty(Order = 0)]
        public string Description { get; set; }

        [JsonProperty(Order = 1)]
        public Exception Exception { get; set; }
    }

    public static class RestResponse
    {
        public static RestResponse<TBody> Success<TBody>(TBody body)
        {
            return new RestResponse<TBody>
            {
                StatusCode = HttpStatusCode.OK,
                Body = body
            };
        }

        public static RestResponse<FailureBody> Failure(Exception exception)
        {
            return new RestResponse<FailureBody>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Body = new FailureBody
                {
                    Description = exception.Message,
                    Exception = exception
                }
            };
        }

        public static RestResponse<FailureBody> Failure(HttpStatusCode statusCode, string errorMessage)
        {
            return new RestResponse<FailureBody>
            {
                StatusCode = statusCode,
                Body = new FailureBody
                {
                    Description = errorMessage,
                    Exception = null
                }
            };
        }

        public static dynamic FromLogResult(LogResult result)
        {
            return result.Succeeded ? (dynamic) Success("OK") : Failure(result.Exception);
        }
    }
}