using io.rong.methods.conversation;
using io.rong.methods.message;
using io.rong.methods.user;
using io.rong.methods.group;
using io.rong.methods.sensitive;
using io.rong.methods.chatroom;
using io.rong.methods.push;
using io.rong.util;
using System.Collections.Generic;

namespace io.rong
{
    public class RongCloud
    {
        private static Dictionary<string, RongCloud> rongCloud = new Dictionary<string, RongCloud>();

        public User User;
        public Message Message;
        public Wordfilter Wordfilter;
        public SensitiveWord Sensitiveword;
        public Group Group;
        public Chatroom Chatroom;
        public Conversation Conversation;
        public Push Push;
        public Broadcast Broadcast;

        private List<HostType> apiHostType = new List<HostType>
        {
            new HostType("https://api.cn.ronghub.com"),
            new HostType("https://api-cn.ronghub.com")
        };

        private volatile int errNum;
        private volatile int apiIndex = 0;

        public int ErrNum
        {
            get { return errNum; }
            set { errNum = value; }
        }

        public HostType ApiHostType
        {
            get
            {
                if (apiIndex < apiHostType.Count && errNum <= 3)
                {
                    return apiHostType[apiIndex];
                }
                else
                {
                    return apiHostType[0];
                }
            }
        }

        public HostType SmsHostType { get; set; } = new HostType("http://api.sms.ronghub.com");

        private RongCloud(string appKey, string appSecret)
        {
            User = new User(appKey, appSecret);
            User.RongCloud = this;
            Message = new Message(appKey, appSecret);
            Message.RongCloud = this;
            Conversation = new Conversation(appKey, appSecret);
            Conversation.RongCloud = this;
            Group = new Group(appKey, appSecret);
            Group.RongCloud = this;
            Wordfilter = new Wordfilter(appKey, appSecret);
            Wordfilter.RongCloud = this;
            Sensitiveword = new SensitiveWord(appKey, appSecret);
            Sensitiveword.RongCloud = this;
            Chatroom = new Chatroom(appKey, appSecret);
            Chatroom.RongCloud = this;
            Push = new Push(appKey, appSecret);
            Push.RongCloud = this;
            Broadcast = new Broadcast(appKey, appSecret);
            Broadcast.RongCloud = this;
        }

        public static RongCloud GetInstance(string appKey, string appSecret)
        {
            if (!rongCloud.ContainsKey(appKey))
            {
                rongCloud.Add(appKey, new RongCloud(appKey, appSecret));
            }

            return rongCloud[appKey];
        }

        public static RongCloud GetInstance(string appKey, string appSecret, string api)
        {
            if (!rongCloud.ContainsKey(appKey))
            {
                RongCloud rc = new RongCloud(appKey, appSecret);
                if (!string.IsNullOrWhiteSpace(api))
                {
                    rc.apiHostType.Insert(0, new HostType(api));
                }

                rongCloud.Add(appKey, rc);
            }

            return rongCloud[appKey];
        }
    }
}
