using System;
using System.Text;

namespace io.rong.methods.chatroom.whitelist
{
    public class Whitelist

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/whitelist";
        private RongCloud rongCloud;

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public RongCloud RongCloud
        {
            get => rongCloud;
            set
            {
                rongCloud = value;
                Message.RongCloud = value;
                User.RongCloud = value;
            }
        }
        public User User { get; set; }

        public Messages Message { get; set; }

        public Whitelist(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
            Message = new Messages(appKey, appSecret);
            User = new User(appKey, appSecret);
        }
    }
}
