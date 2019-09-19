using System.Collections.Generic;
using Newtonsoft.Json;
using RongCloud.Server.models.chatroom;

namespace RongCloud.Server.models.response
{
    public class ChatroomUserQueryResult : Result

    {
        /**
         * 聊天室中用户数。
         *
         */
        /**
         * 聊天室成员列表。
         *
         */

        [JsonIgnore] [field: JsonProperty(PropertyName = "total")] public int Total { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "members")] internal List<ChatroomMember> Members { get; set; }

        public ChatroomUserQueryResult()
        {

        }

        public ChatroomUserQueryResult(int code, string msg, int total, List<ChatroomMember> members) : base(code, msg)
        {
            Total = total;
            Members = members;
        }

        public ChatroomUserQueryResult(int total, List<ChatroomMember> members)
        {
            Total = total;
            Members = members;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
