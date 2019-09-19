using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.methods.chatroom.ban;
using RongCloud.Server.methods.chatroom.block;
using RongCloud.Server.methods.chatroom.demotion;
using RongCloud.Server.methods.chatroom.distribute;
using RongCloud.Server.methods.chatroom.gag;
using RongCloud.Server.methods.chatroom.keepalive;
using RongCloud.Server.methods.chatroom.whitelist;
using RongCloud.Server.models;
using RongCloud.Server.models.chatroom;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.chatroom
{
    /**
 *
 * 聊天室服务
 * docs: "http://www.rongcloud.cn/docs/server.html#chatroom"
 *
 * */
    public class Chatroom

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom";
        private string appKey;
        private string appSecret;
        public Block block;
        public Gag gag;
        public Ban ban;
        public Keepalive keepalive;
        public Demotion demotion;
        public Whitelist whiteList;
        public Distribute distribute;
        private RongCloud rongCloud;

        internal RongCloud RongCloud
        {
            get => rongCloud;
            set
            {
                rongCloud = value;
                gag.RongCloud = value;
                keepalive.RongCloud = value;
                demotion.RongCloud = value;
                whiteList.RongCloud = value;
                block.RongCloud = value;
                demotion.RongCloud = value;
                distribute.RongCloud = value;
                ban.RongCloud = value;
            }
        }

        public Chatroom(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            gag = new Gag(appKey, appSecret);
            keepalive = new Keepalive(appKey, appSecret);
            demotion = new Demotion(appKey, appSecret);
            whiteList = new Whitelist(appKey, appSecret);
            block = new Block(appKey, appSecret);
            distribute = new Distribute(appKey, appSecret);
            ban = new Ban(appKey, appSecret);
        }

        /**
         * 创建聊天室方法 
         * 
         * @param  chatrooms:chatroom.id,name（必传）
         *
         * @return ResponseResult
         **/
        public async Task<ResponseResult> Create(ChatroomModel[] chatrooms)
        {
            if (chatrooms == null)
            {
                return new ResponseResult(1002, "Paramer 'chatrooms' is required");
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < chatrooms.Length; i++)
            {
                ChatroomModel chatroom = chatrooms[i];
                sb.Append("&chatroom[" + chatroom.Id + "]=").Append(HttpUtility.UrlEncode(chatroom.Name, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                rongCloud.ApiHostType.Type + "/chatroom/create.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.CREATE, result));
        }

        /**
         * 销毁聊天室方法
         *
         * @param  chatroom:要销毁的聊天室 Id。（必传）
         *
         * @return ResponseResult
         **/
        public async Task<ResponseResult> Destroy(ChatroomModel chatroom)
        {
            if (chatroom == null)
            {
                return new ResponseResult(1002, "Paramer 'chatroomId' is required");
            }

            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.DESTORY);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                rongCloud.ApiHostType.Type + "/chatroom/destroy.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.DESTORY, result));
        }

        /**
         * 查询聊天室内用户方法
         *
         * @param  chatroom:聊天室.id,count,order（必传）
         *
         * @return ChatroomUserQueryResult
         **/
        public async Task<ChatroomUserQueryResult> Get(ChatroomModel chatroom)
        {
            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.GET);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ChatroomUserQueryResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));
            sb.Append("&count=").Append(HttpUtility.UrlEncode(chatroom.Count.ToString(), UTF8));
            sb.Append("&order=").Append(HttpUtility.UrlEncode(chatroom.Order.ToString(), UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                rongCloud.ApiHostType.Type + "/chatroom/user/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ChatroomUserQueryResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.GET, result));
        }

        /**
         * 查询用户是否存在聊天室
         *
         * @param  member:聊天室成员。（必传）
         *
         * @return ResponseResult
         **/
        public async Task<CheckChatRoomUserResult> IsExist(ChatroomMember member)
        {
            string message = CommonUtil.CheckFiled(member, PATH, CheckMethod.ISEXIST);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<CheckChatRoomUserResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(member.ChatroomId, UTF8));
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                rongCloud.ApiHostType.Type + "/chatroom/user/exist.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<CheckChatRoomUserResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.ISEXIST, result));
        }
    }
}