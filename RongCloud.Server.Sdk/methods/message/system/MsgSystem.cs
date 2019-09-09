using io.rong.models;
using io.rong.models.response;
using System;
using System.Collections.Generic;
using System.Text;
using io.rong.models.message;
using io.rong.util;
using System.Web;

namespace io.rong.methods.messages.system
{
    /**
     * 发送系统消息方法
     *
     * docs : http://www.rongcloud.cn/docs/server.html#message_system_publish
     *
     */
    public class MsgSystem

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public static string PATH1 { get; } = "message/system";

        public RongCloud RongCloud { get; set; }

        public MsgSystem(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }

        /**
         * 发送系统消息方法（一个用户向一个或多个用户发送系统消息，单条消息最大 128k，会话类型为 SYSTEM。
         * 每秒钟最多发送 100 条消息，每次最多同时向 100 人发送，如：一次发送 100 人时，示为 100 条消息。）
         *
         * @param message 消息体
         * @return ResponseResult
         * @throws Exception
         **/
        public ResponseResult Send(MessageModel message)
        {
            SystemMessage systemMessage = (SystemMessage) message;
            string code = CommonUtil.CheckFiled(systemMessage, PATH1, CheckMethod.PUBLISH);
            if (null != code)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(code);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&fromUserId=").Append(HttpUtility.UrlEncode(systemMessage.SenderId, UTF8));

            for (int i = 0; i < systemMessage.TargetId.Length; i++)
            {
                string child = systemMessage.TargetId[i];
                if (null != child)
                {
                    sb.Append("&toUserId=").Append(HttpUtility.UrlEncode(child, UTF8));
                }
            }

            sb.Append("&objectName=").Append(HttpUtility.UrlEncode(systemMessage.Content.GetType(), UTF8));
            sb.Append("&content=").Append(HttpUtility.UrlEncode(systemMessage.Content.ToString(), UTF8));

            if (systemMessage.PushContent != null)
            {
                sb.Append("&pushContent=").Append(HttpUtility.UrlEncode(systemMessage.PushContent, UTF8));
            }

            if (systemMessage.PushData != null)
            {
                sb.Append("&pushData=").Append(HttpUtility.UrlEncode(systemMessage.PushData, UTF8));
            }

            sb.Append("&isPersisted=").Append(HttpUtility.UrlEncode(systemMessage.IsPersisted.ToString(), UTF8));


            if (0 == systemMessage.IsPersisted)
            {
                sb.Append("&isPersisted=").Append(HttpUtility.UrlEncode(systemMessage.IsPersisted.ToString(), UTF8));
            }

            if (0 == systemMessage.IsCounted)
            {
                sb.Append("&isCounted=").Append(HttpUtility.UrlEncode(systemMessage.IsCounted.ToString(), UTF8));
            }


            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/message/system/publish.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH1, CheckMethod.PUBLISH, result));
        }

        /**
         * 发送系统模板消息方法（一个用户向一个或多个用户发送系统消息，单条消息最大 128k，会话类型为 SYSTEM.每秒钟最多发送 100 条消息，每次最多同时向 100 人发送，如：一次发送 100 人时，示为 100 条消息。）
         *
         * @param  template:系统模版消息。
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public ResponseResult SendTemplate(TemplateMessage template)
        {
            string code = CommonUtil.CheckFiled(template, PATH1, CheckMethod.PUBLISHTEMPLATE);
            if (null != code)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(code);
            }

            List<string> toUserIds = new List<string>();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            List<string> push = new List<string>();

            foreach (var vo in template.Content)
            {
                toUserIds.Add(vo.Key);
                values.Add(vo.Value.Data);
                push.Add(vo.Value.Push);
            }

            Templates templateMessage = new Templates()
            {
                FromUserId = template.SenderId,
                ToUserId = toUserIds.ToArray(),
                ObjectName = template.ObjectName,
                Content = template.Template.ToString(),
                Values = values,
                PushContent = push.ToArray(),
                PushData = template.PushData,
                ContentAvailable = template.ContentAvailable
            };
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, templateMessage.ToString(),
                RongCloud.ApiHostType.Type + "/message/system/publish_template.json", "application/json");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH1, CheckMethod.PUBLISHTEMPLATE, result));
        }

        /**
         * 发送广播消息方法（发送消息给一个应用下的所有注册用户，如用户未在线会对满足条件（绑定手机终端）的用户发送 Push 信息，单条消息最大 128k，会话类型为 SYSTEM。每小时只能发送 2 次，每天最多发送 3 次。）
         * 该功能开发环境下可免费使用。生产环境下，您需要登录开发者后台，在“应用/IM 服务/高级功能设置”中开通公有云专业版后，在“广播消息和推送”中，开启后才能使用
         *
         * @param message 消息体
         *
         * @return ResponseResult
         * @throws Exception
         **/
        public ResponseResult Broadcast(BroadcastMessage message)
        {
            string errMsg = CommonUtil.CheckFiled(message, PATH1, CheckMethod.BROADCAST);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&fromUserId=").Append(HttpUtility.UrlEncode(message.SenderId, UTF8));
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

            if (message.Os != null)
            {
                sb.Append("&os=").Append(HttpUtility.UrlEncode(message.Os, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.Ordinal) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/message/broadcast.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH1, CheckMethod.BROADCAST, result));
        }
    }
}