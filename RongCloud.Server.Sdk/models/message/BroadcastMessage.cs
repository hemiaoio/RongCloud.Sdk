using RongCloud.Server.messages;

namespace RongCloud.Server.models.message
{

    public class BroadcastMessage : MessageModel

    {
        public BroadcastMessage()
        {
        }

        public BroadcastMessage(string senderUserId, string[] targetId, string objectName, BaseMessage content, string pushContent, string pushData,
                                string os) : base(senderUserId, targetId, objectName, content, pushContent, pushData)
        {
            Os = os;
        }

        public string Os { get; set; }
    }
}