using Newtonsoft.Json;

namespace RongCloud.Server.models.response
{
    public class ChatroomDemotionMsgResult : Result

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "objectNames")] public string[] ObjectNames { get; set; }

        public ChatroomDemotionMsgResult()
        {

        }

        public ChatroomDemotionMsgResult(int code, string msg, string[] objectNames) : base(code, msg)
        {
            ObjectNames = objectNames;
        }

        public ChatroomDemotionMsgResult(string[] objectNames)
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
