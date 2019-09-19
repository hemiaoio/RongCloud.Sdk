using Newtonsoft.Json;
using RongCloud.Server.models.user;

namespace RongCloud.Server.models.response
{
    public class BlackListResult : Result

    {
        /**
         * 黑名单用户列表
         */

        [JsonIgnore] [field: JsonProperty(PropertyName = "users")] public UserModel[] Users { get; set; }

        public BlackListResult(int code, string msg, UserModel[] users) : base(code, msg)
        {
            Users = users;
        }


        /**
         * 获取users
         *
         * @return User[]
         */
        public UserModel[] getUsers()
        {
            return Users;
        }
        /**
         * 设置users
         *
         */
        public void setUsers(UserModel[] users)
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
