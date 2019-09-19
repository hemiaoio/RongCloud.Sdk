using Newtonsoft.Json;

namespace RongCloud.Server.models.chatroom
{
    public class ChatroomModel

    {
        /**
         * 聊天室 id。
         */
        /**
         * 聊天室名。
         */
        /**
         * 聊天室创建时间。
         */
        /**
         * 聊天室成员。
         */
        /**
         * 聊天室成员数。
         */
        /**
         * 加入聊天室的先后顺序,1正序，2倒叙。
         */

        /**
         * 禁言时间
         * */

        [JsonIgnore] [field: JsonProperty(PropertyName ="id")] public string Id { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "time")] public string Time { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "members")] public ChatroomMember[] Members { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "count")] public int Count { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "order")] public int Order { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "minute")] public int Minute { get; set; }

        public ChatroomModel() : base()
        {

        }
        /**
         * 聊天室构造函数 全量
         * */
        public ChatroomModel(string id, string name, string time, ChatroomMember[] members,
                             int count, int order, int minute)
        {
            Id = id;
            Name = name;
            Time = time;
            Members = members;
            Count = count;
            Order = order;
            Minute = minute;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
