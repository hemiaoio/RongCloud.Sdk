using Newtonsoft.Json;

namespace RongCloud.Server.messages
{

    /**
     *
     * 图文消息。
     *
     */
    public class ImgTextMessage : BaseMessage

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Content { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "extra")] public string Extra { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "title")] public string Title { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "imageUri")] public string ImageUri { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "conturlent")] public string Url { get; set; } = "";

        public static string TYPE1 { get; } = "RC:ImgTextMsg";

        public ImgTextMessage(string content, string extra, string title, string imageUri, string url)
        {
            Content = content;
            Extra = extra;
            Title = title;
            ImageUri = imageUri;
            Url = url;
        }
        override
        public string GetType()
        {
            return TYPE1;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}