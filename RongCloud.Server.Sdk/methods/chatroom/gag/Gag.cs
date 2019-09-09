using io.rong.models;
using io.rong.models.response;
using io.rong.util;
using System;
using System.Text;
using System.Web;
using io.rong.models.chatroom;

namespace io.rong.methods.chatroom.gag
{
    public class Gag

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/member-gag";

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
        public Gag(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }
        /**
         * 添加禁言聊天室成员方法（在 App 中如果不想让某一用户在聊天室中发言时，可将此用户在聊天室中禁言，被禁言用户可以接收查看聊天室中用户聊天信息，但不能发送消息.）
         *
         * @param  chatroom:封禁的聊天室信息，其中聊天室 d（必传）,minute(必传), memberIds（必传支持多个最多20个）
         *
         * @return ResponseResult
         **/
        public ResponseResult Add(ChatroomModel chatroom)
        {
            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.ADD);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }
            /* message = CommonUtil.checkParam("minute",minute,PATH,CheckMethod.ADD);
             if(null != message){
                 return (ResponseResult)RongJsonUtil.JsonStringToObj(message,ResponseResult.class);
             }*/

            StringBuilder sb = new StringBuilder();
            ChatroomMember[] members = chatroom.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));
            sb.Append("&minute=").Append(HttpUtility.UrlEncode(chatroom.Minute.ToString(), UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                              RongCloud.ApiHostType.Type + "/chatroom/user/gag/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 查询聊天室被禁言成员方法
         *
         * @param  chatroom:聊天室信息 Id。（必传）
         *
         * @return ListGagChatroomUserResult
         **/
        public ListGagChatroomUserResult GetList(ChatroomModel chatroom)
        {
            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.GETLIST);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ListGagChatroomUserResult>(message);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                                  RongCloud.ApiHostType.Type + "/chatroom/user/gag/list.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ListGagChatroomUserResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));

        }

        /**
         * 移除禁言聊天室成员方法
         *
         * @param  chatroom:封禁的聊天室信息，其中聊天室 Id。（必传）,用户 Id。（必传支持多个最多20个）
         * @return ResponseResult
         **/
        public ResponseResult Remove(ChatroomModel chatroom)
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
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                          RongCloud.ApiHostType.Type + "/chatroom/user/gag/rollback.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));

        }
    }

}
