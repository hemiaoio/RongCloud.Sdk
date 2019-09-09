using io.rong.models;
using io.rong.models.response;
using io.rong.util;
using System;
using System.Text;
using System.Web;
using io.rong.models.chatroom;

namespace io.rong.methods.chatroom.ban
{

    /**
     * 聊天室全局禁言服务
     * docs:http://www.rongcloud.cn/docs/server.html#chatroom_user_ban
     *
     * */
    public class Ban

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/global-gag";

        public Ban(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        /**
        * 添加用户聊天室全局禁言方法
        *
        * @param  chatroom : Id,minute。（必传）
        *
        * @return ResponseResult
        **/
        public ResponseResult Add(ChatroomModel chatroom)
        {
            string errMsg = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.ADD);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            ChatroomMember[] members = chatroom.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }
            sb.Append("&minute=").Append(HttpUtility.UrlEncode(chatroom.Minute.ToString(), UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                            RongCloud.ApiHostType.Type + "/chatroom/user/ban/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 查询被聊天室全局禁言用户方法
         *
         * @return ListGagChatroomUserResult
         **/
        public ListGagChatroomUserResult GetList()
        {
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, "",
                            RongCloud.ApiHostType.Type + "/chatroom/user/ban/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ListGagChatroomUserResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }

        /**
         * 移除用户聊天室全局禁言方法
         *
         * @param  chatroom: memberIds。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Remove(ChatroomModel chatroom)
        {
            if (chatroom == null)
            {
                return new ResponseResult(1002, "Paramer 'chatroom' is required");
            }

            string errMsg = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.REMOVE);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            ChatroomMember[] members = chatroom.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                               RongCloud.ApiHostType.Type + "/chatroom/user/ban/remove.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }
    }
}


