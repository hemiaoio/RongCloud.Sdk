using Newtonsoft.Json;

namespace RongCloud.Server.models.response
{
    /**
     * historyMessage返回结果
     */
    public class HistoryMessageResult : Result

    {

        // 历史消息下载地址。
        // 历史记录时间。（yyyymmddhh）

        [JsonIgnore] [field: JsonProperty(PropertyName = "url")] public string Url { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "date")] public string Date { get; set; }

        public HistoryMessageResult(int code, string url, string date, string errorMessage):base(code, errorMessage)
        {
            this.code = code;
            Url = url;
            Date = date;
            msg = errorMessage;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}