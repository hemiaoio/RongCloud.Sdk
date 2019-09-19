using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.message;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.message.@group
{
    /**
     * 发送群组消息方法
     *
     * docs : http://www.rongcloud.cn/docs/server.html#message_group_publish
     * @author RongCloud
     *
     */
    public class Group

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "message/group";
        private static readonly string RECAL_PATH = "message/recall";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public Group(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }

        /**
         * 发送群组消息方法（以一个用户身份向群组发送消息，单条消息最大 128k.每秒钟最多发送 20 条消息，每次最多向 3 个群组发送，如：一次向 3 个群组发送消息，示为 3 条消息。）
         *
         * @param message
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public async Task<ResponseResult> Send(GroupMessage message)
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
                    sb.Append("&toGroupId=").Append(HttpUtility.UrlEncode(child, UTF8));
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

            if (0 != message.IsPersisted)
            {
                sb.Append("&isPersisted=").Append(HttpUtility.UrlEncode(message.IsPersisted.ToString(), UTF8));
            }

            if (0 != message.IsCounted)
            {
                sb.Append("&isCounted=").Append(HttpUtility.UrlEncode(message.IsCounted.ToString(), UTF8));
            }

            if (0 != message.IsIncludeSender)
            {
                sb.Append("&isIncludeSender=").Append(HttpUtility.UrlEncode(message.IsIncludeSender.ToString(), UTF8));
            }

            if (0 != message.ContentAvailable)
            {
                sb.Append("&contentAvailable=")
                    .Append(HttpUtility.UrlEncode(message.ContentAvailable.ToString(), UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/message/group/publish.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.PUBLISH, result));
        }

        /**
         * 发送群组@消息方法（以一个用户身份向群组发送消息，单条消息最大 128k.每秒钟最多发送 20 条消息，每次最多向 3 个群组发送，如：一次向 3 个群组发送消息，示为 3 条消息。）
         *
         * @param message
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public async Task<ResponseResult> SendMention(MentionMessage message)
        {
            string code = CommonUtil.CheckFiled(message, PATH, CheckMethod.PUBLISH);
            if (null != code)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(code);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&fromUserId=").Append(HttpUtility.UrlEncode(message.SenderId, UTF8));
            string[] groupIds = message.TargetId;
            for (int i = 0; i < groupIds.Length; i++)
            {
                string child = groupIds[i];
                sb.Append("&toGroupId=").Append(HttpUtility.UrlEncode(child, UTF8));
            }

            sb.Append("&objectName=").Append(HttpUtility.UrlEncode(message.Content.Content.GetType(), UTF8));
            sb.Append("&content=").Append(HttpUtility.UrlEncode(message.Content.Content.ToString(), UTF8));

            if (message.PushContent != null)
            {
                sb.Append("&pushContent=").Append(HttpUtility.UrlEncode(message.PushContent, UTF8));
            }

            if (message.PushContent != null)
            {
                sb.Append("&pushData=").Append(HttpUtility.UrlEncode(message.PushContent, UTF8));
            }

            if (0 != message.IsPersisted)
            {
                sb.Append("&isPersisted=").Append(HttpUtility.UrlEncode(message.IsPersisted.ToString(), UTF8));
            }

            if (0 != message.IsCounted)
            {
                sb.Append("&isCounted=").Append(HttpUtility.UrlEncode(message.IsCounted.ToString(), UTF8));
            }

            if (0 != message.IsIncludeSender)
            {
                sb.Append("&isIncludeSender=").Append(HttpUtility.UrlEncode(message.IsIncludeSender.ToString(), UTF8));
            }

            sb.Append("&isMentioned=").Append(HttpUtility.UrlEncode("1", UTF8));

            if (0 != message.ContentAvailable)
            {
                sb.Append("&contentAvailable=")
                    .Append(HttpUtility.UrlEncode(message.ContentAvailable.ToString(), UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/message/group/publish.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.PUBLISH, result));
        }

        /**
         * 撤回群组消息。
         *
         * @param message
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public async Task<Result> Recall(RecallMessage message)
        {
            //需要校验的字段
            string errMsg = CommonUtil.CheckFiled(message, RECAL_PATH, CheckMethod.RECALL);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&conversationType=").Append(HttpUtility.UrlEncode("3", UTF8));
            sb.Append("&fromUserId=").Append(HttpUtility.UrlEncode(message.SenderId, UTF8));
            sb.Append("&targetId=").Append(HttpUtility.UrlEncode(message.TargetId, UTF8));
            sb.Append("&messageUID=").Append(HttpUtility.UrlEncode(message.UId, UTF8));
            sb.Append("&sentTime=").Append(HttpUtility.UrlEncode(message.SentTime, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/message/recall.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.RECALL, result));
        }
    }
}