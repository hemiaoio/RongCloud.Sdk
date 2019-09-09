using io.rong.methods.messages._private;
using io.rong.methods.messages.chatroom;
using io.rong.methods.messages.discussion;
using io.rong.methods.messages.system;
using io.rong.methods.messages.history;
using System;
using System.Text;

namespace io.rong.methods.message
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
        public messages.group.Group group;
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
            group = new messages.group.Group(appKey, appSecret);
            history = new History(appKey, appSecret);
            system = new MsgSystem(appKey, appSecret);

        }
    }
}
