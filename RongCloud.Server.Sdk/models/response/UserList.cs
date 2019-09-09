using Newtonsoft.Json;
using System;

namespace io.rong.models.response
{
    public class UserList

    {
        /**
         * 返回码，200 为正常。
         *
         */
        /**
         * 黑名单用户列表
         */

        [JsonIgnore] [field: JsonProperty(PropertyName = "code")] public int Code { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "users")] public string[] Users { get; set; }

        public UserList(int code, string[] users)
        {
            Code = code;
            Users = users;
        }

        public string[] getUsers()
        {
            return Users;
        }

        public void setUsers(string[] users)
        {
            Users = users;
        }

        public int getCode()
        {
            return Code;
        }

        public void setCode(int code)
        {
            Code = code;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
