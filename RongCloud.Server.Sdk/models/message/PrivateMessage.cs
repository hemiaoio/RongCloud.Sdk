using io.rong.messages;
using System;

namespace io.rong.models.message
{
    public class PrivateMessage : MessageModel

    {

        /**
         * 针对 iOS 平台，Push 时用来控制未读消息显示数，只有在 toUserId 为一个用户 Id 的时候有效。（可选）
         **/
        /**
         * 针对 iOS 平台，Push 时用来控制未读消息显示数，只有在 toUserId 为一个用户 Id 的时候有效。（可选）
         */
        /**
         * 当前版本有新的自定义消息，而老版本没有该自定义消息时，老版本客户端收到消息后是否进行未读消息计数，
         * 0 表示为不计数、 1 表示为计数，默认为 1 计数，未读消息数增加 1。（可选）
         */

        /**
         *是否过滤发送人黑名单列表，0 表示为不过滤、 1 表示为过滤，默认为 0 不过滤。（可选
         */
        /**
         * 发送用户自已是否接收消息，0 表示为不接收，1 表示为接收，默认为 0 不接收。（可选）
         */

        public string Count { get; set; }

        public int IsPersisted { get; set; }

        public int IsCounted { get; set; }

        public int VerifyBlacklist { get; set; }

        public int IsIncludeSender { get; set; }

        public int ContentAvailable { get; set; }

        public PrivateMessage()
        {
        }

        public PrivateMessage(string senderId, string[] targetId, string objectName, BaseMessage content, string pushContent, string pushData,
                              string count, int isPersisted, int isCounted, int verifyBlacklist, int isIncludeSender, int contentAvailable) : base(senderId, targetId, objectName, content, pushContent, pushData)
        {

            Count = count;
            IsPersisted = isPersisted;
            IsCounted = isCounted;
            VerifyBlacklist = verifyBlacklist;
            IsIncludeSender = isIncludeSender;
            ContentAvailable = contentAvailable;
        }

    }
}
