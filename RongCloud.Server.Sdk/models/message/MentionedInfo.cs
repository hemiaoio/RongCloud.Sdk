using Newtonsoft.Json;
using System;

namespace io.rong.models.message
{
    public class MentionedInfo
    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "type")] public int Type { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "userIds")] public string[] UserIds { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "pushContent")] public string PushContent { get; set; }

        public MentionedInfo(int type, string[] userIds, string pushContent)
        {
            Type = type;
            UserIds = userIds;
            PushContent = pushContent;
        }
    }
}
