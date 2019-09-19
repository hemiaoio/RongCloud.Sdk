using System.Collections.Generic;
using Newtonsoft.Json;

namespace RongCloud.Server.models.response
{
    public class TagListResult : Result
    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "result")] public Dictionary<string, string[]> Result
        {
            get;
            set;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
