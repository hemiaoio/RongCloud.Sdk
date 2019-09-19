using Newtonsoft.Json;

namespace RongCloud.Server.models.response
{
    public class ResponseResult : Result

    {
        public ResponseResult(int code, string msg) : base(code, msg)
        {
            this.code = code;
            this.msg = msg;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

    }
}
