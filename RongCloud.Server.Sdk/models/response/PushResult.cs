using System;
using Newtonsoft.Json;

namespace io.rong.models.response
{
    /**
     * push 返回结果
     */
    public class PushResult : Result
    {

        /**
         * 推送唯一标识。
         */
        private string id;

        public string GetId()
        {
            return id;
        }

        public void SetId(string id)
        {
            this.id = id;
        }


        public PushResult(int code, string id) : base(code, id)
        {
            this.code = code;
            this.id = id;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
