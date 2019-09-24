using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.response;
using RongCloud.Server.models.user;
using RongCloud.Server.util;

/**
 *
 * 用户黑名单服务
 * docs: "http://www.rongcloud.cn/docs/server.html#black"
 *
 * @author RongCloud
 * @version
 * */
namespace RongCloud.Server.methods.user.blacklist
{
    public class Blacklist

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "user/blacklist";
        private string appKey;
        private string appSecret;

        public RongCloud RongCloud { get; set; }

        public Blacklist(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        /**
         * 添加用户到黑名单方法（每秒钟限 100 次）
         *
         * @param  user:用户 Id,blacklist（必传）
         *
         * @return ResponseResult
         **/
        public async Task<Result> Add(UserModel user)
        {
            string message = CommonUtil.CheckFiled(user, PATH, CheckMethod.ADD);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(user.Id, UTF8));
            foreach (UserModel blackUser in user.GetBlacklist())
            {
                sb.Append("&blackUserId=").Append(HttpUtility.UrlEncode(blackUser.Id, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/user/blacklist/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 获取某用户的黑名单列表方法（每秒钟限 100 次）
         *
         * @param  user:用户 Id。（必传）
         *
         * @return QueryBlacklistUserResult
         **/
        public async Task<BlackListResult> GetList(UserModel user)
        {
            string message = CommonUtil.CheckFiled(user, PATH, CheckMethod.GETLIST);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<BlackListResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(user.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/user/blacklist/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<BlackListResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }

        /**
         * 从黑名单中移除用户方法（每秒钟限 100 次）
         *
         * @param  user:用户 Id,blacklist（必传）
         *
         * @return ResponseResult
         **/
        public async Task<Result> Remove(UserModel user)
        {
            string message = CommonUtil.CheckFiled(user, PATH, CheckMethod.REMOVE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(user.Id, UTF8));
            foreach (UserModel blackUser in user.GetBlacklist())
            {
                sb.Append("&blackUserId=").Append(HttpUtility.UrlEncode(blackUser.Id, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/user/blacklist/remove.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<BlackListResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }
    }
}