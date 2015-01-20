using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Finsa.Caravan.WebApi.Results
{
    public sealed class JsonNetResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }

        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }

        private JsonNetResult()
        {
        }

        public static JsonNetResult For(object data, Encoding contentEncoding = null, string contentType = null, JsonSerializerSettings serializerSettings = null, Formatting formatting = Formatting.Indented)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                SerializerSettings = serializerSettings ?? new JsonSerializerSettings(),
                Formatting = formatting
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrWhiteSpace(ContentType)
                ? ContentType
                : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }
    }
}
