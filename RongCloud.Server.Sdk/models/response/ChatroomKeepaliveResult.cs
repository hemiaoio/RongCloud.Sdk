using Newtonsoft.Json;
using System;

namespace io.rong.models.response
{
    public class ChatroomKeepaliveResult : Result

    {
        public ChatroomKeepaliveResult(int code, string msg, string[] chatrooms) : base(code, msg)
        {
            Chatrooms = chatrooms;
        }
        [JsonIgnore] [field: JsonProperty(PropertyName = "chatrooms")] public string[] Chatrooms { get; set; }


        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
