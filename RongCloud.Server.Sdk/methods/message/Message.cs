using System.Text;
using RongCloud.Server.methods.message._private;
using RongCloud.Server.methods.message.chatroom;
using RongCloud.Server.methods.message.discussion;
using RongCloud.Server.methods.message.@group;
using RongCloud.Server.methods.message.history;
using RongCloud.Server.methods.message.system;

namespace RongCloud.Server.methods.message
{
    public class Message

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "message";
        private static string method = "";
        private string appKey;
        private string appSecret;
        public Private msgPrivate;
        public Chatroom chatroom;
        public Discussion discussion;
        public Group group;
        public History history;
        public MsgSystem system;
        private RongCloud rongCloud;

        public RongCloud RongCloud
        {
            get => rongCloud;
            set
            {
                rongCloud = value;
                msgPrivate.RongCloud = value;
                chatroom.RongCloud = value;
                discussion.RongCloud = value;
                group.RongCloud = value;
                history.RongCloud = value;
                system.RongCloud = value;
            }
        }

        public Message(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            msgPrivate = new Private(appKey, appSecret);
            chatroom = new Chatroom(appKey, appSecret);
            discussion = new Discussion(appKey, appSecret);
            group = new Group(appKey, appSecret);
            history = new History(appKey, appSecret);
            system = new MsgSystem(appKey, appSecret);
        }
    }
}