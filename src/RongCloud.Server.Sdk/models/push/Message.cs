﻿using Newtonsoft.Json;

namespace RongCloud.Server.models.push
{
    /**
     * 发送消息内容（必传）
     */
    public class Message
    {

        /**
         * 发送消息内容，参考融云 Server API 消息类型表.示例说明；如果 objectName 为自定义消息类型，该参数可自定义格式。（必传）
         */
        [JsonProperty(PropertyName = "content")]
        private string content;

        /**
         * 消息类型，参考融云 Server API 消息类型表.消息标志；可自定义消息类型，长度不超过 32 个字符。（必传）
         */
        [JsonProperty(PropertyName = "objectName")]
        private string objectName;

        public string GetContent()
        {
            return content;
        }

        public void SetContent(string content)
        {
            this.content = content;
        }

        public string GetObjectName()
        {
            return objectName;
        }

        public void SetObjectName(string objectName)
        {
            this.objectName = objectName;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
