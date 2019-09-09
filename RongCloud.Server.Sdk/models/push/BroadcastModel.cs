using System;
using Newtonsoft.Json;

namespace io.rong.models.push
{
    /**
     * 广播消息体。
     */
    public class BroadcastModel : BroadcastPushPublicPart
    {

        /**
         *  发送人用户 Id。（必传）
         */
        [JsonProperty(PropertyName = "fromuserid")]
        private string fromuserid;

        /**
         * 发送消息内容（必传）
         */
        [JsonProperty(PropertyName = "message")]
        private Message message;

        public string GetFromuserid()
        {
            return fromuserid;
        }

        public void SetFromuserid(string fromuserid)
        {
            this.fromuserid = fromuserid;
        }

        public Message GetMessage()
        {
            return message;
        }

        public void SetMessage(Message message)
        {
            this.message = message;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
