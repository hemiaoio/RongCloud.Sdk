using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.chatroom.whitelist
{
    public class Messages

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/whitelist/message";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public Messages(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }

        /**
         * 添加聊天室消息白名单成员方法
         *
         * @param  objectNames:消息类型列表
         *
         * @return ResponseResult
         **/
        public async Task<ResponseResult> Add(string[] objectNames)
        {
            if (objectNames == null)
            {
                return new ResponseResult(1002, "Paramer 'objectNames' is required");
            }

            string errMsg = CommonUtil.CheckParam("objectNames", objectNames, PATH, CheckMethod.ADD);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < objectNames.Length; i++)
            {
                string child = objectNames[i];
                sb.Append("&objectnames=").Append(HttpUtility.UrlEncode(child, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/chatroom/whitelist/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 删除聊天室消息白名单方法
         *
         * @param  objectNames:消息类型列表
         *
         * @return ResponseResult
         **/
        public async Task<ResponseResult> Remove(string[] objectNames)
        {
            if (objectNames == null)
            {
                return new ResponseResult(1002, "Paramer 'objectNames' is required");
            }

            string errMsg = CommonUtil.CheckParam("objectNames", objectNames, PATH, CheckMethod.REMOVE);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < objectNames.Length; i++)
            {
                string child = objectNames[i];
                sb.Append("&objectnames=").Append(HttpUtility.UrlEncode(child, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/chatroom/whitelist/delete.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }

        /**
         * 获取聊天室消息类型白名单列表
         *
         *
         * @return ResponseResult
         **/
        public async Task<ChatroomWhitelistMsgResult> GetList()
        {
            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, "",
                RongCloud.ApiHostType.Type + "/chatroom/whitelist/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ChatroomWhitelistMsgResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }
    }
}