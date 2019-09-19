using Newtonsoft.Json;

namespace RongCloud.Server.models.response
{
    class BlockUserList
    {
        /**
         * 返回码，200 为正常。
         *
         */
        private int code;
        /**
         * 黑名单用户列表
         */
        private string[] users;


        public BlockUserList(int code, string[] users)
        {
            this.code = code;
            this.users = users;
        }

        public string[] getUsers()
        {
            return users;
        }

        public void setUsers(string[] users)
        {
            this.users = users;
        }

        public int getCode()
        {
            return code;
        }

        public void setCode(int code)
        {
            this.code = code;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
