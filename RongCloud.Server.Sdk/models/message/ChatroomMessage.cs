using io.rong.messages;
using System;

namespace io.rong.models.message
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
