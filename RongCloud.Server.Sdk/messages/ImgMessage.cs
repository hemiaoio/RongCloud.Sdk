using io.rong.messages;
using Newtonsoft.Json;
using System;

namespace io.rong.message
{

    /**
     *
     * 图片消息。
     *
     */
    public class ImgMessage : BaseMessage

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Content { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "extra")] public string Extra { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "imageUri")] public string ImageUri { get; set; } = "";

        public static string TYPE1 { get; } = "RC:ImgMsg";

        public ImgMessage(string content, string extra, string imageUri)
        {
            Content = content;
            Extra = extra;
            ImageUri = imageUri;
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