using io.rong.models.chatroom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace io.rong.models.response
{
    public class ListGagChatroomUserResult : Result

    {
        /**
         * 聊天室被禁言用户列表。
         *
         */

        [JsonIgnore] [field: JsonProperty(PropertyName = "members")] internal List<ChatroomMember> Members { get; set; }

        public ListGagChatroomUserResult()
        {

        }

        public ListGagChatroomUserResult(int code, string msg, List<ChatroomMember> members) : base(code, msg)
        {
            Members = members;
        }

        public ListGagChatroomUserResult(List<ChatroomMember> members)
        {
            Members = members;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
