using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.message;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.message._private
{
    /**
     * 发送单聊消息方法
     * docs : http://www.rongcloud.cn/docs/server.html#message_private_publish
     */
    public class Private

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "message/_private";
        private static readonly string RECAL_PATH = "message/recall";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public Private(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }

        /**
         * 发送单聊消息方法（一个用户向另外一个用户发送消息，单条消息最大 128k。每分钟最多发送 6000 条信息，每次发送用户上限为 1000 人，如：一次发送 1000 人时，示为 1000 条消息。） 
         * 
         * @param message 单聊消息
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public async Task<ResponseResult> Send(PrivateMessage message)
        {
            if (null == message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>("Paramer 'message' is required");
            }

            string errMsg = CommonUtil.CheckFiled(message, PATH, CheckMethod.SEND);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&fromUserId=").Append(HttpUtility.UrlEncode(message.SenderId, UTF8));

            for (int i = 0; i < message.TargetId.Length; i++)
            {
                string child = message.TargetId[i];
                if (null != child)
                {
                    sb.Append("&toUserId=").Append(HttpUtility.UrlEncode(child, UTF8));
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

            if (message.Count != null)
            {
                sb.Append("&count=").Append(HttpUtility.UrlEncode(message.Count, UTF8));
            }

            if (0 != message.VerifyBlacklist)
            {
                sb.Append("&verifyBlacklist=").Append(HttpUtility.UrlEncode(message.VerifyBlacklist.ToString(), UTF8));
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

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/message/private/publish.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.PUBLISH, result));
        }

        /**
         * 发送单聊模板消息方法（一个用户向多个用户发送不同消息内容，单条消息最大 128k。每分钟最多发送 6000 条信息，每次发送用户上限为 1000 人。） 
         * 
         * @param  message:单聊模版消息。
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public async Task<ResponseResult> SendTemplate(TemplateMessage message)
        {
            string errMsg = CommonUtil.CheckFiled(message, PATH, CheckMethod.SENDTEMPLATE);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            Templates templateMessage = new Templates();

            List<string> toUserIds = new List<string>();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            List<string> push = new List<string>();

            foreach (var vo in message.Content)
            {
                toUserIds.Add(vo.Key);
                values.Add(vo.Value.Data);
                push.Add(vo.Value.Push);
            }

            templateMessage.FromUserId = message.SenderId;
            templateMessage.ToUserId = toUserIds.ToArray();
            templateMessage.ObjectName = message.ObjectName;
            templateMessage.Content = message.Template.ToString();
            templateMessage.Values = values;
            templateMessage.PushContent = push.ToArray();
            templateMessage.PushData = push.ToArray();
            templateMessage.VerifyBlacklist = message.VerifyBlacklist;
            templateMessage.ContentAvailable = message.ContentAvailable;

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, templateMessage.ToString(),
                RongCloud.ApiHostType.Type + "/message/private/publish_template.json", "application/json");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.PUBLISHTEMPLATE, result));
        }

        /**
         * 设置用户某会话接收新消息时是否进行消息提醒。
         *
         * @param message
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public async Task<Result> Recall(RecallMessage message)
        {
            string errMsg = CommonUtil.CheckFiled(message, RECAL_PATH, CheckMethod.RECALL);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&conversationType=").Append(HttpUtility.UrlEncode("1", UTF8));
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