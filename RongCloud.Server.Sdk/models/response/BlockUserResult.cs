using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace io.rong.models.response
{
    public class BlockUserResult : Result

    {
        // 被封禁用户列表。

        [JsonIgnore] [field: JsonProperty(PropertyName = "users")] public List<BlockUsers> Users { get; set; }

        public BlockUserResult(int code, string errorMessage, List<BlockUsers> users) : base(code, errorMessage)
        {
            Users = users;
        }
        
        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
