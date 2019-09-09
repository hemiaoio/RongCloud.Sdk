using Newtonsoft.Json;
using System;

namespace io.rong.models.response
{
    public class ChatroomWhitelistMsgResult : Result

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "objectNames")] public string[] ObjectNames { get; set; }

        public ChatroomWhitelistMsgResult()
        {

        }

        public ChatroomWhitelistMsgResult(int code, string msg, string[] objectNames) : base(code, msg)
        {
            ObjectNames = objectNames;
        }

        public ChatroomWhitelistMsgResult(string[] objectNames)
        {
            ObjectNames = objectNames;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
