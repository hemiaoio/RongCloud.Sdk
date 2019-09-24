using Newtonsoft.Json;

namespace RongCloud.Server.models.conversation
{
    public class ConversationModel

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "type")] public string Type { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "userId")] public string UserId { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "targetId")] public string TargetId { get; set; }

        public ConversationModel()
        {
        }

        /**
         * 构造函数。
         *
         * @param type:会话类型。
         * @param userId:设置消息免打扰的用户 Id
         * @param targetId:目标Id
         *
         **/
        public ConversationModel(string type, string userId, string targetId)
        {
            Type = type;
            UserId = userId;
            TargetId = targetId;
        }
    }
}
