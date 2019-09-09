using Newtonsoft.Json;
using io.rong.models.sensitiveword;
using System;
using System.Collections.Generic;

namespace io.rong.models.response
{
    public class ListWordfilterResult:Result

    {
        // 敏感词内容。

        [JsonIgnore] [field: JsonProperty(PropertyName = "words")] public List<SensitiveWordModel> Words { get; set; }

        public ListWordfilterResult()
        {
        }

        public ListWordfilterResult(int code, List<SensitiveWordModel> words, string errorMessage):base(code, errorMessage)
        {
            this.code = code;
            Words = words;
            msg = errorMessage;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
