using io.rong.messages;
using System;

namespace io.rong.models.message
{

    /**
     * 讨论组消息体
     * @author hc
     */
    public class DiscussionMessage : MessageModel

    {

        /**
         * 针对 iOS 平台，Push 时用来控制未读消息显示数，只有在 toUserId 为一个用户 Id 的时候有效。（可选）
         */
        /**
         * 当前版本有新的自定义消息，而老版本没有该自定义消息时，老版本客户端收到消息后是否进行未读消息计数，
         * 0 表示为不计数、 1 表示为计数，默认为 1 计数，未读消息数增加 1。（可选）
         */

        /**
         * 当前版本有新的自定义消息，而老版本没有该自定义消息时，老版本客户端收到消息后是否进行未读消息计数，
         * 0 表示为不计数、 1 表示为计数，默认为 1 计数，未读消息数增加 1。（可选）
         */

        /**
         * ios静默推送 0关闭 1开启
         **/

        public int IsPersisted { get; set; }

        public int IsCounted { get; set; }

        public int IsIncludeSender { get; set; }

        public int ContentAvailable { get; set; }

        public DiscussionMessage()
        {
        }

        public DiscussionMessage(string senderId, string[] targetId, string objectName, BaseMessage content, string pushContent, string pushData,
                                 int isPersisted, int isCounted, int isIncludeSender, int contentAvailable):base(senderId, targetId, objectName, content, pushContent, pushData)
        {
            
            IsPersisted = isPersisted;
            IsCounted = isCounted;
            IsIncludeSender = isIncludeSender;
            ContentAvailable = contentAvailable;
        }
    }
}
