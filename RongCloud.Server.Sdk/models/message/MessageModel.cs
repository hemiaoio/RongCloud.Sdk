using io.rong.messages;
using Newtonsoft.Json;
using System;

namespace io.rong.models.message
{
    public class MessageModel

    {
        /**
         *
         * 接受 Id 可能是用户Id，聊天Id ，群组Id，讨论组Id（必传）
         **/
        /**
         *消息类型 （必传）
         **/
        /**
         * 发送消息内容，参考融云消息类型表.示例说明；如果 objectName
         * 为自定义消息类型，该参数可自定义格式。（必传）。
         **/
        /**
         * 定义显示的 Push 内容，如果 objectName 为融云内置消息类型时，
         * 则发送后用户一定会收到 Push 信息。如果为自定义消息，则 pushContent
         * 为自定义消息显示的 Push 内容，如果不传则用户不会收到 Push 通知。（可选）
         */
        /**
         * 针对 iOS 平台为 Push 通知时附加到 payload 中，Android 客户端收到推送消息时对应字段名为 pushData。（可选）
         */

        [JsonIgnore] [field: JsonProperty(PropertyName = "senderId")] public string SenderId { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "targetId")] public string[] TargetId { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "objectName")] public string ObjectName { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "content")] public BaseMessage Content { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "pushContent")] public string PushContent { get; set; }

        [JsonIgnore] [field: JsonProperty(PropertyName = "pushData")] public string PushData { get; set; }

        public MessageModel()
        {
        }

        public MessageModel(string senderId, string[] targetId, string objectName, BaseMessage content,
                            string pushContent, string pushData)
        {
            SenderId = senderId;
            TargetId = targetId;
            ObjectName = objectName;
            Content = content;
            PushContent = pushContent;
            PushData = pushData;
        }
    }
}
