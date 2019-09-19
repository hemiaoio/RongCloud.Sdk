using Newtonsoft.Json;

namespace RongCloud.Server.messages
{
    /**
     *
     * 资料通知消息。此类型消息没有 Push 通知。
     *
     */
    public class ProfileNtfMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:ProfileNtf";

        [JsonIgnore] [field: JsonProperty(PropertyName = "operation")] public string Operation { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "data")] public string Data { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "extra")] public string Extra { get; set; } = "";

        public ProfileNtfMessage(string operation, string data, string extra)
        {
            Operation = operation;
            Data = data;
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