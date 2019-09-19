using Newtonsoft.Json;

namespace RongCloud.Server.models
{
    public class BlockUsers

    {
        // 被封禁用户 ID。
        // 封禁结束时间。

        [JsonIgnore] [field: JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "blockEndTime")] public string BlockEndTime { get; set; }

        public BlockUsers(string id, string blockEndTime)
        {
            Id = id;
            BlockEndTime = blockEndTime;
        }

        override
         public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
