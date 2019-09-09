using io.rong.models;
using io.rong.models.response;
using System;
using System.Text;
using io.rong.models.message;
using io.rong.util;
using System.Web;

namespace io.rong.methods.messages.discussion
{

    /**
     * 发送讨论组消息方法
     *
     * docs : http://www.rongcloud.cn/docs/server.html#message_discussion_publish
     * @author RongCloud
     *
     */
    public class Discussion

    {

        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "message/discussion";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public RongCloud getRongCloud()
        {
            return RongCloud;
        }

        public void setRongCloud(RongCloud rongCloud)
        {
            RongCloud = rongCloud;
        }
        public Discussion(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }
        /**
         * 发送讨论组消息方法（以一个用户身份向讨论组发送消息，单条消息最大 128k，每秒钟最多发送 20 条消息.）
         *
         *
         * @param  message:发送消息内容，参考融云消息类型表.示例说明；如果 objectName 为自定义消息类型，该参数可自定义格式。（必传）
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public ResponseResult Send(DiscussionMessage message)
        {


            string code = CommonUtil.CheckFiled(message, PATH, CheckMethod.PUBLISH);
            if (null != code)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(code);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&fromUserId=").Append(HttpUtility.UrlEncode(message.SenderId, UTF8));
            for (int i = 0; i < message.TargetId.Length; i++)
            {
                string child = message.TargetId[i];
                if (null != child)
                {
                    sb.Append("&toDiscussionId=").Append(HttpUtility.UrlEncode(child, UTF8));
                }
            }
            sb.Append("&objectName=").Append(HttpUtility.UrlEncode(message.Content.GetType(), UTF8));
            sb.Append("&content=").Append(HttpUtility.UrlEncode(message.Content.ToString(), UTF8));

            if (message.PushContent != null)
            {
                sb.Append("&pushContent=").Append(HttpUtility.UrlEncode(message.PushContent, UTF8));
            }

            if (message.PushData != null)
            {
                sb.Append("&pushData=").Append(HttpUtility.UrlEncode(message.PushData, UTF8));
            }

            if (0 == message.IsPersisted)
            {
                sb.Append("&isPersisted=").Append(HttpUtility.UrlEncode(message.IsPersisted.ToString(), UTF8));
            }

            if (0 == message.IsCounted)
            {
                sb.Append("&isCounted=").Append(HttpUtility.UrlEncode(message.IsCounted.ToString(), UTF8));
            }

            if (0 == message.IsIncludeSender)
            {
                sb.Append("&isIncludeSender=").Append(HttpUtility.UrlEncode(message.IsIncludeSender.ToString(), UTF8));
            }
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                      RongCloud.ApiHostType.Type + "/message/discussion/publish.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.PUBLISH, result));
        }

        /**
         * 设置用户某会话接收新消息时是否进行消息提醒。
         *
         * @param message
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public Result Recall(RecallMessage message)
        {
            //需要校验的字段
            string msgErr = CommonUtil.CheckFiled(message, PATH, CheckMethod.RECALL);
            if (null != msgErr)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(msgErr);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("&conversationType=").Append(HttpUtility.UrlEncode("2", UTF8));
            sb.Append("&fromUserId=").Append(HttpUtility.UrlEncode(message.SenderId, UTF8));
            sb.Append("&targetId=").Append(HttpUtility.UrlEncode(message.TargetId, UTF8));
            sb.Append("&messageUID=").Append(HttpUtility.UrlEncode(message.UId, UTF8));
            sb.Append("&sentTime=").Append(HttpUtility.UrlEncode(message.SentTime, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                         RongCloud.ApiHostType.Type + "/message/recall.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.RECALL, result));

        }
    }
}
