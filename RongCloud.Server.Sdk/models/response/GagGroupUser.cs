using Newtonsoft.Json;

namespace RongCloud.Server.models.response
{

    /**
     * 群组用户封禁信息。
     */
    public class GagGroupUser

    {
        // 解禁时间。
        string time;
        // 群成员 Id。
        string id;

        public GagGroupUser(string time, string id)
        {
            this.time = time;
            this.id = id;
        }

        /**
         * 设置time
         *
         */
        public GagGroupUser setTime(string time)
        {
            this.time = time;
            return this;
        }

        /**
         * 获取time
         *
         * @return String
         */
        public string getTime()
        {
            return time;
        }

        /**
         * 获取userId
         *
         * @return String
         */
        public string getId()
        {
            return id;
        }

        /**
         * 设置userId
         *
         */
        public void setId(string id)
        {
            this.id = id;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }
    }
}
