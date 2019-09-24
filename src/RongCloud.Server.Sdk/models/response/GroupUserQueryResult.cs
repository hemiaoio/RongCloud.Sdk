using System.Collections.Generic;
using Newtonsoft.Json;

namespace RongCloud.Server.models.response
{
    public class GroupUserQueryResult : Result

    {
        /// <summary>
        /// 群成员用户Id。
        /// </summary>
        [JsonIgnore]
        [field: JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// 群成员列表。
        /// </summary>
        [JsonIgnore]
        [field: JsonProperty(PropertyName = "members")]
        public List<GroupUser> Members { get; set; }

        public GroupUserQueryResult()
        {
        }

        public GroupUserQueryResult(int code, string msg, string id, List<GroupUser> members) : base(code, msg)
        {
            Id = id;
            Members = members;
        }

        public GroupUserQueryResult(int code, List<GroupUser> members) : base(code, "")
        {
            Members = members;
        }


        public GroupUserQueryResult(string id, List<GroupUser> members)
        {
            Id = id;
            Members = members;
        }

        override
            public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        }
    }
}