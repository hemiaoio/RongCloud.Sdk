using io.rong.messages;
using Newtonsoft.Json;
using System;

namespace io.rong.models.message
{
    public class MentionMessageContent

    {
        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] internal BaseMessage Content { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "mentionedInfo")] public MentionedInfo MentionedInfo
        {
            get;
            set;
        }

        public MentionMessageContent(BaseMessage content, MentionedInfo mentionedInfo)
        {
            Content = content;
            MentionedInfo = mentionedInfo;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
