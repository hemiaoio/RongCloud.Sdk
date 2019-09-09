using Newtonsoft.Json;
using System;

namespace io.rong.messages
{
    /**
     *
     * 通用命令通知消息。此类型消息没有 Push 通知。此类型消息没有 Push 通知，与通用命令通知消息的区别是不存储、不计数。
     *
     */
    public class CmdMsgMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:CmdMsg";

        [JsonIgnore] [field: JsonProperty(PropertyName = "name")] public string Name { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "data")] public string Data { get; set; } = "";

        public CmdMsgMessage(string name, string data)
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