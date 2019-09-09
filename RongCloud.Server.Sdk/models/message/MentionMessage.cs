using System;

namespace io.rong.models.message
{
    public class MentionMessage

    {
        /**
         * 接收群 Id，提供多个本参数可以实现向多群发送消息，最多不超过 3 个群组。（必传）
         */
        /**
         * 消息 内容
         */

        public string[] TargetId { get; set; }

        public string ObjectName { get; set; }

        public MentionMessageContent Content { get; set; }

        public string PushContent { get; set; }

        public string PushData { get; set; }

        public int IsPersisted { get; set; }

        public int IsCounted { get; set; }

        public int IsIncludeSender { get; set; }

        public int ContentAvailable { get; set; }

        public string SenderId { get; set; }

        public MentionMessage()
        {
        }

        public MentionMessage(string senderId, string[] targetId, string objectName, MentionMessageContent content, string pushContent, string pushData,
                              int isPersisted, int isCounted, int isIncludeSender, int contentAvailable)
        {
            SenderId = senderId;
            TargetId = targetId;
            ObjectName = objectName;
            Content = content;
            PushContent = pushContent;
            PushData = pushData;
            IsPersisted = isPersisted;
            IsCounted = isCounted;
            IsIncludeSender = isIncludeSender;
            ContentAvailable = contentAvailable;
        }
    }
}
