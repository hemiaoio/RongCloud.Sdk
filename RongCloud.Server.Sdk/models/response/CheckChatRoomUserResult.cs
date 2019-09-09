using Newtonsoft.Json;
using System;

namespace io.rong.models.response
{
    public class CheckChatRoomUserResult

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "code")] public string Code { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "isInChrm")] public bool IsInChrm { get; set; }

        public CheckChatRoomUserResult(string code, bool isInChrm)
        {
            Code = code;
            IsInChrm = isInChrm;
        }

		override
		public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
