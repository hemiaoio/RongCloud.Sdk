using Newtonsoft.Json;

namespace RongCloud.Server.models.sensitiveword
{
    /**
     * 敏感词、替换词信息
     */
    public class SensitiveWordModel

    {
        /**
         * 敏感词类型
         */
        /**
         *敏感词
         */
        /**
         *替换词
         */

        [JsonIgnore] [field: JsonProperty(PropertyName = "type")] public int Type { get; set; } = 1;

        [JsonIgnore] [field: JsonProperty(PropertyName = "keyword")] public string Keyword { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "replace")] public string Replace { get; set; }

        public SensitiveWordModel(int type, string keyword, string replace)
        {
            Type = type;
            Keyword = keyword;
            Replace = replace;
        }

        public SensitiveWordModel()
        {
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
