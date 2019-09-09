using io.rong.models;
using io.rong.models.response;
using io.rong.util;
using System;
using System.Text;
using System.Web;

namespace io.rong.methods.chatroom.demotion
{
    public class Demotion

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/demotion";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public Demotion(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }
        /**
         * 添加应用内聊天室降级消息
         *
         * @param  objectName:消息类型，每次最多提交 5 个，设置的消息类型最多不超过 20 个。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Add(string[] objectName)
        {
            string message = CommonUtil.CheckParam("type", objectName, PATH, CheckMethod.ADD);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < objectName.Length; i++)
            {
                string child = objectName[i];
                sb.Append("&objectName=").Append(HttpUtility.UrlEncode(child, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                       RongCloud.ApiHostType.Type + "/chatroom/message/priority/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 移除应用内聊天室降级消息
         *
         * @param  objectNames:要销毁消息类型表。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Remove(string[] objectNames)
        {
            string message = CommonUtil.CheckParam("type", objectNames, PATH, CheckMethod.REMOVE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < objectNames.Length; i++)
            {
                string child = objectNames[i];
                sb.Append("&objectName=").Append(HttpUtility.UrlEncode(child, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                          RongCloud.ApiHostType.Type + "/chatroom/message/priority/remove.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));


        }
        /**
         * 获取应用内聊天室降级消息
         *
         *
         * @return ResponseResult
         **/
        public ChatroomDemotionMsgResult GetList()
        {
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, "",
                            RongCloud.ApiHostType.Type + "/chatroom/message/priority/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ChatroomDemotionMsgResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }
    }
}
