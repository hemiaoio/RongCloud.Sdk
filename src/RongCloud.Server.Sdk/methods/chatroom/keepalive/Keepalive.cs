using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.chatroom;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.chatroom.keepalive
{
    public class Keepalive

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/keepalive";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public Keepalive(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }

        /**
         * 添加聊天室保活方法
         *
         * @param   chatroom: 聊天室信息，id（必传）
         *
         * @return ResponseResult
         **/
        public async Task<ResponseResult> Add(ChatroomModel chatroom)
        {
            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.ADD);
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

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/chatroom/keepalive/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 删除聊天室保活方法
         *
         * @param  chatroom: 聊天室信息，id（必传）
         *
         * @return ResponseResult
         **/
        public async Task<ResponseResult> Remove(ChatroomModel chatroom)
        {
            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.REMOVE);
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

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/chatroom/keepalive/remove.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }

        /**
         * 获取聊天室保活
         *
         *
         * @return ResponseResult
         **/
        public async Task<ChatroomKeepaliveResult> GetList()
        {
            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, "",
                RongCloud.ApiHostType.Type + "/chatroom/keepalive/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ChatroomKeepaliveResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }
    }
}