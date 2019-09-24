using Newtonsoft.Json;

namespace RongCloud.Server.messages
{

    /**
     *
     * 通用命令通知消息。此类型消息没有 Push 通知。
     *
     */
    public class CmdNtfMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:CmdNtf";

        [JsonIgnore] [field: JsonProperty(PropertyName = "name")] public string Name { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "data")] public string Data { get; set; } = "";

        public CmdNtfMessage(string name, string data)
        {
            Name = name;
            Data = data;
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