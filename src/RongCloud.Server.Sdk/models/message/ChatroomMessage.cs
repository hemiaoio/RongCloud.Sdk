using RongCloud.Server.messages;

namespace RongCloud.Server.models.message
{
    public class ChatroomMessage : MessageModel

    {

        public ChatroomMessage()
        {

        }

        public ChatroomMessage(string senderUserId, string[] targetId, string objectName, BaseMessage content) : base(senderUserId, targetId, objectName, content, null, null)
        {

        }

    }
}
