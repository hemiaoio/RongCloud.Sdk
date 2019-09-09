using Newtonsoft.Json;
using System;

namespace io.rong.messages
{
    /**
     *
     * 添加联系人消息。
     *
     */
    public class ContactNtfMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:ContactNtf";

        [JsonIgnore] [field: JsonProperty(PropertyName = "operation")] public string Operation { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "extra")] public string Extra { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "sourceUserId")] public string SourceUserId { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "targetUserId")] public string TargetUserId { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "message")] public string Message { get; set; } = "";

        public ContactNtfMessage(string operation, string extra, string sourceUserId, string targetUserId, string message)
        {
            Operation = operation;
            Extra = extra;
            SourceUserId = sourceUserId;
            TargetUserId = targetUserId;
            Message = message;
        }
        override
        public string GetType()
        {
            return TYPE;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}