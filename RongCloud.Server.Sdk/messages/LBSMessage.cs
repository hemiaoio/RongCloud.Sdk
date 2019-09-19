using Newtonsoft.Json;

namespace RongCloud.Server.messages
{

    /**
     *
     * 位置消息。
     *
     */
    public class LBSMessage : BaseMessage

    {
        private static readonly string TYPE = "RC:LBSMsg";

        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public string Content { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "extra")] public string Extra { get; set; } = "";

        [JsonIgnore] [field: JsonProperty(PropertyName = "latitude")] public double Latitude { get; set; } = 0;

        [JsonIgnore] [field: JsonProperty(PropertyName = "longitude")] public double Longitude { get; set; } = 0;

        [JsonIgnore] [field: JsonProperty(PropertyName = "poi")] public string Poi { get; set; } = "";

        public LBSMessage(string content, string extra, double latitude, double longitude, string poi)
        {
            Content = content;
            Extra = extra;
            Latitude = latitude;
            Longitude = longitude;
            Poi = poi;
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