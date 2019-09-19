using Newtonsoft.Json;

namespace RongCloud.Server.models.push
{
    /**
     * 广播跟参数公共部分
     */
    public class BroadcastPushPublicPart
    {

        /**
         * 目标操作系统，iOS、Android 最少传递一个。如果需要给两个系统推送消息时，则需要全部填写。（必传）
         */
        [JsonProperty(PropertyName = "platform")]
        private string[] platform;

        /**
         * 推送条件，包括： tag、userid、is_to_all。（必传）
         */
        [JsonProperty(PropertyName = "audience")]
        private Audience audience;

        /**
         * 按操作系统类型推送消息内容
         */
        [JsonProperty(PropertyName = "notification")]
        private Notification notification;

        public string[] GetPlatform()
        {
            return platform;
        }

        public void SetPlatform(string[] platform)
        {
            this.platform = platform;
        }

        public Audience GetAudience()
        {
            return audience;
        }

        public void SetAudience(Audience audience)
        {
            this.audience = audience;
        }

        public Notification GetNotification()
        {
            return notification;
        }

        public void SetNotification(Notification notification)
        {
            this.notification = notification;
        }

        override
                public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
