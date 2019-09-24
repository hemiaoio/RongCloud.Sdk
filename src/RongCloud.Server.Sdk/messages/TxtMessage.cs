using Newtonsoft.Json;

namespace RongCloud.Server.messages
{
    /**
     *
     * 文本消息。
     *
     */
    public class TxtMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:TxtMsg";

        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Content { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "extra")] public string Extra { get; set; } = "";

        public TxtMessage(string content, string extra)
        {
            Content = content;
            Extra = extra;
        }

        override
        public string GetType()
        {
            return TYPE;
        }


        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}