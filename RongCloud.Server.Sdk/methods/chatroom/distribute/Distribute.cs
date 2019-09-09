using io.rong.models;
using io.rong.models.response;
using io.rong.util;
using System;
using System.Text;
using System.Web;
using io.rong.models.chatroom;

namespace io.rong.methods.chatroom.distribute
{
    public class Distribute

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "chatroom/distribute";

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        public Distribute(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }

        /**
         * 停止聊天室消息分发（可实现控制对聊天室中消息是否进行分发，停止分发后聊天室中用户发送的消息，融云服务端不会再将消息发送给聊天室中其他用户。）
         *
         * @param  chatroom:聊天室信息，其中聊天室 Id。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Stop(ChatroomModel chatroom)
        {
            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.STOP_DISTRIBUTION);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                           RongCloud.ApiHostType.Type + "/chatroom/message/stopDistribution.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.STOP_DISTRIBUTION, result));

        }

        /**
         * 恢复聊天室消息分发
         *
         * @param  chatroom:聊天室 Id。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Resume(ChatroomModel chatroom)
        {
            string message = CommonUtil.CheckFiled(chatroom, PATH, CheckMethod.RESUME_DISTRIBUTION);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&chatroomId=").Append(HttpUtility.UrlEncode(chatroom.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                               RongCloud.ApiHostType.Type + "/chatroom/message/resumeDistribution.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.RESUME_DISTRIBUTION, result));
        }
    }
}
