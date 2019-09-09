using Newtonsoft.Json;
using System;

namespace io.rong.messages
{
    /**
     *
     * 自定义消息
     *
     */
    public class CustomTxtMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:TxtMsg";

        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Content { get; set; } = "";

        public CustomTxtMessage(string content)
        {
            Content = content;
        }
        override
        public string GetType()
        {
            return TYPE;
        }

        /**
         * 获取自定义消息内容。
         *
         * @return String
         */
        public string getContent()
        {
            return Content;
        }

        /**
         * @param content 设置自定义消息内容。
         *
         *
         */
        public void setContent(string content)
        {
            Content = content;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}