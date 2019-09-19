using Newtonsoft.Json;

namespace RongCloud.Server.exception
{
    public class Error

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "url")] public string Url { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "httpCode")] public int HttpCode { get; set; } = 200;

        [JsonIgnore] [field: JsonProperty(PropertyName = "code")] public int Code { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "errorMessage")] public string ErrorMessage { get; set; }

        public Error(int code, int httpCode, string url, string errorMessage)
        {
            Url = url;
            Code = code;
            ErrorMessage = errorMessage;
            HttpCode = httpCode;
        }

        public bool HasError()
        {
            return Code != 200;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
