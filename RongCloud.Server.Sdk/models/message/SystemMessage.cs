using io.rong.messages;
using System;

namespace io.rong.models.message
{
    public class SystemMessage : MessageModel

    {
        /**
         * 当前版本有新的自定义消息，而老版本没有该自定义消息时，
         * 老版本客户端收到消息后是否进行存储，0 表示为不存储、 1 表示为存储，默认为 1 存储消息。（可选）
         *
         * */
        /**
         * 当前版本有新的自定义消息，而老版本没有该自定义消息时，
         * 老版本客户端收到消息后是否进行未读消息计数，0 表示为不计数、 1 表示为计数，默认为 1 计数，未读消息数增加 1。（可选）
         * */
        /**
         * 针对 iOS 平台，对 SDK 处于后台暂停状态时为静默推送，是 iOS7 之后推出的一种推送方式。
         * 允许应用在收到通知后在后台运行一段代码，且能够马上执行。1 表示为开启，0 表示为关闭，默认为 0（可选）
         * */

        public SystemMessage()
        {
        }

        public SystemMessage(string senderUserId, string[] targetId, string objectName, BaseMessage content, string pushContent, string pushData,
                             int isPersisted, int isCounted, int contentAvailable) : base(senderUserId, targetId, objectName, content, pushContent, pushData)
        {
            IsPersisted = isPersisted;
            IsCounted = isCounted;
            ContentAvailable = contentAvailable;
        }

        public int IsPersisted { get; set; }

        public int IsCounted { get; set; }

        public int ContentAvailable { get; set; }
    }
}
