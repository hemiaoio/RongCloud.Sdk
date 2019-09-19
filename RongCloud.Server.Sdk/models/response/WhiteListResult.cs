using Newtonsoft.Json;
using RongCloud.Server.models.user;

namespace RongCloud.Server.models.response
{
    public class WhiteListResult : Result

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "members")] internal UserModel[] Members { get; set; }

        public WhiteListResult(int code, string msg) :base(code, msg)
        {

        }

        public WhiteListResult(int code, string msg, UserModel[] members) : base(code, msg)
        {
            Members = members;
        }

        public WhiteListResult(UserModel[] members)
        {
            Members = members;
        }

        public UserModel[] getMembers()
        {
            return Members;
        }

        public void setMembers(UserModel[] members)
        {
            Members = members;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
