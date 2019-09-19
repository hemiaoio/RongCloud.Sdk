using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.chatroom;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.chatroom.block
{
    /**
     *
     * 聊天室封禁服务
     * docs: "http://www.rongcloud.cn/docs/server.html#chatroom_user_block"
     *
     * */
    public class Block

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/block";

        public Block(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        /**
* 添加封禁聊天室成员方法
*
* @param  chatroom:聊天室信息，memberIds（必传支持多个最多20个）,minute:封禁时长，以分钟为单位，最大值为43200分钟。（必传）
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
            ChatroomMember[] members = chatroom.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }

            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));
            sb.Append("&minute=").Append(HttpUtility.UrlEncode(chatroom.Minute.ToString(), UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/chatroom/user/block/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 查询被封禁聊天室成员方法
         *
         * @param  chatroomId:聊天室 Id。（必传）
         *
         * @return ListBlockChatroomUserResult
         **/
        public async Task<ListBlockChatroomUserResult> GetList(string chatroomId)
        {
            string message = CommonUtil.CheckParam("id", chatroomId, PATH, CheckMethod.GETLIST);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ListBlockChatroomUserResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroomId, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/chatroom/user/block/list.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ListBlockChatroomUserResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }

        /**
         * 移除封禁聊天室成员方法
         *
         * @param  chatroom: 封禁的聊天室信息 其中聊天室 Id。（必传）,用户 Id。（必传）
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
            ChatroomMember[] members = chatroom.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }

            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                RongCloud.ApiHostType.Type + "/chatroom/user/block/rollback.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }
    }
}