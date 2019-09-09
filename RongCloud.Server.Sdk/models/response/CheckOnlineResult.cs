using Newtonsoft.Json;
using System;

namespace io.rong.models.response
{
    public class CheckOnlineResult : Result

    {
        // 在线状态，1为在线，0为不在线。

        [field: JsonProperty(PropertyName = "status")] public string Status { get; set; }

        public CheckOnlineResult(int code, string status, string errorMessage):base(code, errorMessage)
        {
            this.code = code;
            Status = status;
            msg = errorMessage;
        }
        /**
         * 设置status
         *
         */
        public void setStatus(string status)
        {
            Status = status;
        }

        /**
         * 获取status
         *
         * @return String
         */
        public string getStatus()
        {
            return Status;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
