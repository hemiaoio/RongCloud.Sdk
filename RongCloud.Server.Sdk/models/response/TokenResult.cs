using Newtonsoft.Json;
using System;

/**
 * getToken 返回结果
 */
namespace io.rong.models.response
{
    public class TokenResult : Result

    {
        // 用户 Token，可以保存应用内，长度在 256 字节以内.用户 Token，可以保存应用内，长度在 256 字节以内。
        // 用户 Id，与输入的用户 Id 相同.

        public TokenResult(int code, string token, string userId, string errorMessage)
        {
            this.code = code;
            Token = token;
            UserId = userId;
            msg = errorMessage;
        }

        [JsonIgnore]
        [field: JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "userId")] public string UserId { get; set; }

        [JsonIgnore]
        public string UserId1 { get => UserId; set => UserId = value; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

    }
}
