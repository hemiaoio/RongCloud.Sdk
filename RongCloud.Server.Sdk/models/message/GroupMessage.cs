using io.rong.messages;
using System;

namespace io.rong.models.message
{
    public class GroupMessage : MessageModel

    {

        /**
         * 发送者自己是否接收此条消息, 0: 不接收, 1: 接收, 默认: 0
         **/
        /**
         * 针对 iOS 平台，Push 时用来控制未读消息显示数，只有在 toUserId 为一个用户 Id 的时候有效。（可选）
         */
        /**
         * 当前版本有新的自定义消息，而老版本没有该自定义消息时，老版本客户端收到消息后是否进行未读消息计数，
         * 0 表示为不计数、 1 表示为计数，默认为 1 计数，未读消息数增加 1。（可选）
         */

        /**
         * ios静默推送 0关闭 1开启
         **/

        public int IsIncludeSender { get; set; }

        public int IsPersisted { get; set; }

        public int IsCounted { get; set; }

        public int ContentAvailable { get; set; }

        public GroupMessage()
        {
        }
        /**
         * @param  senderId:发送人用户 Id 。（必传）
         * @param  targetId:接收群Id，提供多个本参数可以实现向多群发送消息，最多不超过 3 个群组。（必传）
         * @param  content:发送消息内容，参考融云消息类型表.示例说明；如果 objectName 为自定义消息类型，该参数可自定义格式。（必传）
         * @param  pushContent:定义显示的 Push 内容，如果 objectName 为融云内置消息类型时，则发送后用户一定会收到 Push 信息. 如果为自定义消息，则 pushContent 为自定义消息显示的 Push 内容，如果不传则用户不会收到 Push 通知。（可选）
         * @param  pushData:针对 iOS 平台为 Push 通知时附加到 payload 中，Android 客户端收到推送消息时对应字段名为 pushData。（可选）
         * @param  isPersisted:当前版本有新的自定义消息，而老版本没有该自定义消息时，老版本客户端收到消息后是否进行存储，0 表示为不存储、 1 表示为存储，默认为 1 存储消息。（可选）
         * @param  isCounted:当前版本有新的自定义消息，而老版本没有该自定义消息时，老版本客户端收到消息后是否进行未读消息计数，0 表示为不计数、 1 表示为计数，默认为 1 计数，未读消息数增加 1。（可选）
         * @param  isIncludeSender:发送用户自已是否接收消息，0 表示为不接收，1 表示为接收，默认为 0 不接收。（可选）
         *
         * */
        public GroupMessage(string senderId, string[] targetId, string objectName, BaseMessage content, string pushContent, string pushData, int isIncludeSender, int isPersisted, int isCounted, int contentAvailable) : base(senderId, targetId, objectName, content, pushContent, pushData)
        {
            IsIncludeSender = isIncludeSender;
            IsPersisted = isPersisted;
            IsCounted = isCounted;
            ContentAvailable = contentAvailable;
        }
    }
}
