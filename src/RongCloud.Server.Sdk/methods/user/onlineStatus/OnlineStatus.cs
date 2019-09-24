using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.response;
using RongCloud.Server.models.user;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.user.onlineStatus
{
    public class OnlineStatus

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "user/online-status";
        private string appKey;
        private string appSecret;

        public RongCloud RongCloud { get; set; }

        public RongCloud getRongCloud()
        {
            return RongCloud;
        }

        public OnlineStatus(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        /**
         * 检查用户在线状态 方法（每秒钟限100次）
         * 请不要频繁循环调用此接口，而是选择合适的频率和时机调用，此接口设置了一定的频率限制。
         *
         * url /user/checkOnline
         * docs http://www.rongcloud.cn/docs/server.html#user_check_online
         *
         * @param  user:用户 id(必传)
         *
         * @return CheckOnlineResult
         **/
        public async Task<CheckOnlineResult> Check(UserModel user)
        {
            //参数校验
            string message = CommonUtil.CheckFiled(user, PATH, CheckMethod.CHECK);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<CheckOnlineResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(user.id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/user/checkOnline.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<CheckOnlineResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.CHECK, result));
        }
    }
}