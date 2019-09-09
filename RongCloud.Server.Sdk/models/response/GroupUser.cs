using Newtonsoft.Json;
using System;

namespace io.rong.models.response
{
    public class GroupUser

    {
        // 用户 Id。

        public GroupUser(string id)
        {
            Id = id;
        }

        [JsonIgnore] [field: JsonProperty(PropertyName = "id")] public string Id { get; set; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
