using Newtonsoft.Json;

namespace RongCloud.Server.models.@group
{

    public class GroupMember
    {
        /**
         * 群组成员Id。
         * */
        /**
         * 群组ID
         * */
        /**
         * 禁言时间
         * */

        public GroupMember() : base()
        {
        }

        public GroupMember(string id, string groupId, int munite)
        {
            Id = id;
            GroupId = groupId;
            Munite = munite;
        }
        [JsonIgnore] [field: JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "goupId1.NET")] public string GroupId { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "munite")] public int Munite { get; set; }

        override
    public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

    }
}
