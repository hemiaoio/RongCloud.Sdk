using io.rong.util;
using io.rong.models.conversation;
using io.rong.models;
using io.rong.models.response;
using System;
using System.Text;
using System.Web;

namespace io.rong.methods.conversation
{
    /**
     *
     * 会话消息免打扰服务
     * docs: "http://www.rongcloud.cn/docs/server.html#conversation_notification"
     *
     * */
    public class Conversation

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "conversation";
        private static string method = "";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public Conversation(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }
        /**
         * 设置用户某会话接收新消息时是否进行消息提醒。
         *
         * @param conversation 会话信息 其中type(必传)
         * @return ResponseResult
         **/
        public ResponseResult Mute(ConversationModel conversation)
        {
            string message = CommonUtil.CheckFiled(conversation, PATH, CheckMethod.MUTE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&conversationType=").Append(HttpUtility.UrlEncode(conversation.Type, UTF8));
            sb.Append("&requestId=").Append(HttpUtility.UrlEncode(conversation.UserId, UTF8));
            sb.Append("&targetId=").Append(HttpUtility.UrlEncode(conversation.TargetId, UTF8));
            sb.Append("&isMuted=").Append(HttpUtility.UrlEncode("1", UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                        RongCloud.ApiHostType.Type + "/conversation/notification/set.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.MUTE, result));
        }

        /**
         * 设置用户某会话接收新消息时是否进行消息提醒。
         *
         * @param conversation 会话信息 其中type(必传)
         * @return ResponseResult
         **/
        public ResponseResult UnMute(ConversationModel conversation)
        {
            string message = CommonUtil.CheckFiled(conversation, PATH, CheckMethod.UNMUTE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&conversationType=").Append(HttpUtility.UrlEncode(conversation.Type, UTF8));
            sb.Append("&requestId=").Append(HttpUtility.UrlEncode(conversation.UserId, UTF8));
            sb.Append("&targetId=").Append(HttpUtility.UrlEncode(conversation.TargetId, UTF8));
            sb.Append("&isMuted=").Append(HttpUtility.UrlEncode("0", UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                           RongCloud.ApiHostType.Type + "/conversation/notification/set.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.UNMUTE, result));

        }
    }
}