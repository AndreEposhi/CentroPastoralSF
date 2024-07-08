using System.Net;
using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses
{
    public class Response<TData> where TData : class
    {
        [JsonConstructor]
        public Response(HttpStatusCode statusCode, bool success, TData? data = null, IEnumerable<string>? errors = null)
        {
            StatusCode = statusCode;
            Success = success;
            Data = data;
            Errors = errors;
        }

        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; private set; }

        [JsonPropertyName("success")]
        public bool Success { get; private set; }

        [JsonPropertyName("data")]
        public TData? Data { get; private set; }

        [JsonPropertyName("errors")]
        public IEnumerable<string>? Errors { get; private set; } = [];
    }
}