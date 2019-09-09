using io.rong.models.chatroom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace io.rong.models.response
{
    public class ListBlockChatroomUserResult : Result

    {
        /**
         * 被封禁用户列表
         *
         */

        [JsonIgnore] [field: JsonProperty(PropertyName = "members")] internal List<ChatroomMember> Members { get; set; }

        public ListBlockChatroomUserResult()
        {

        }

        public ListBlockChatroomUserResult(int code, string msg, List<ChatroomMember> members) : base(code, msg)
        {
            Members = members;
        }

        public ListBlockChatroomUserResult(List<ChatroomMember> members)
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
