using Newtonsoft.Json;
using System;

namespace io.rong.messages
{

    /**
     *
     * 提示条（小灰条）通知消息。此类型消息没有 Push 通知。
     *
     */
    public class InfoNtfMessage : BaseMessage

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Message { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Extra { get; set; } = "";

        public static string TYPE1 { get; } = "RC:InfoNtf";

        public InfoNtfMessage(string message, string extra)
        {
            Message = message;
            Extra = extra;
        }
        override
        public string GetType()
        {
            return TYPE1;
        }


        /**
         * @param extra 设置附加信息(如果开发者自己需要，可以自己在 App 端进行解析)。
         *
         *
         */
        public void setExtra(string extra)
        {
            Extra = extra;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}