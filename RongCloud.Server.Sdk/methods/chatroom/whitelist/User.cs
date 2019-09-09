using io.rong.models;
using io.rong.models.response;
using io.rong.util;
using System;
using System.Text;
using System.Web;
using io.rong.models.chatroom;

namespace io.rong.methods.chatroom.whitelist
{
    public class User

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/whitelist/user";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public User(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }
        /**
         * 添加聊天室白名单成员方法
         *
         * @param  chatroom:聊天室.Id，memberIds 聊天室中白名单成员最多不超过 5 个。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Add(ChatroomModel chatroom)
        {

            if (chatroom == null)
            {
                return new ResponseResult(1002, "Paramer 'chatroom' is required");
            }

            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.ADD);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));

            ChatroomMember[] members = chatroom.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                                         RongCloud.ApiHostType.Type + "/chatroom/user/whitelist/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }
        /**
         * 添加聊天室白名单成员方法
         *
         * @param  chatroom:聊天室.Id，memberIds 聊天室中白名单成员最多不超过 5 个。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Remove(ChatroomModel chatroom)
        {
            if (chatroom == null)
            {
                return new ResponseResult(1002, "Paramer 'chatroom' is required");
            }

            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.REMOVE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));

            ChatroomMember[] members = chatroom.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                                                     RongCloud.ApiHostType.Type + "/chatroom/user/whitelist/remove.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }

        /**
         * 添加聊天室白名单成员方法
         *
         *
         * @return WhiteListResult
         **/
        public WhiteListResult GetList(ChatroomModel chatroom)
        {
            if (chatroom == null)
            {
                return new WhiteListResult(1002, "Paramer 'chatroom' is required");
            }

            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.GETLIST);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<WhiteListResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));

            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                                             RongCloud.ApiHostType.Type + "/chatroom/user/whitelist/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<WhiteListResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }
    }
}
