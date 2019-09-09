using Newtonsoft.Json;
using System;

namespace io.rong.models.chatroom
{
    public class ChatroomMember

    {
        /**
         * 聊天室用户Id。
         * */
        /**
         * 加入聊天室时间。
         * */
        /**
         * 聊天室ID
         * */

        [JsonIgnore] [field: JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "time")] public string Time { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "chatroomId")] public string ChatroomId { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "munite")] public int Munite { get; set; }

        public ChatroomMember():base()
        {
            
        }
        public ChatroomMember(string id, string time)
        {
            Id = id;
            Time = time;
        }

        public ChatroomMember(string id, string chatroomId, string time)
        {
            Id = id;
            ChatroomId = chatroomId;
            Time = time;
        }
        
        /**
         * 获取禁言时长
         *
         * @return String
         */
        public int getMunite()
        {
            return Munite;
        }
        /**
         * 设置munite
         *
         *
         */
        public void setMunite(int munite)
        {
            Munite = munite;
        }
        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

    }
}
