using Newtonsoft.Json;

namespace RongCloud.Server.messages
{
    /**
     *
     * 语音消息。
     *
     */
    public class VoiceMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:VcMsg";

        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Content { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "extra")] public string Extra { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "duration")] public long Duration { get; set; } = 0L;

        public VoiceMessage(string content, string extra, long duration)
        {
            Content = content;
            Extra = extra;
            Duration = duration;
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