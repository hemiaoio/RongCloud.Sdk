using System.Collections.Generic;
using Newtonsoft.Json;
using RongCloud.Server.models.chatroom;

namespace RongCloud.Server.models.response
{
    public class ChatroomQueryResult : Result
    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "chatRooms")] internal List<ChatroomModel> ChatRooms
        {
            get;
            set;
        }

        public ChatroomQueryResult()
        {

        }
        public ChatroomQueryResult(int code, string errorMessage, List<ChatroomModel> chatRooms) : base(code, errorMessage)
        {
            ChatRooms = chatRooms;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
