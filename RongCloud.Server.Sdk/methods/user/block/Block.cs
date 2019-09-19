using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RongCloud.Server.models;
using RongCloud.Server.models.response;
using RongCloud.Server.models.user;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.user.block
{
    public class Block

    {
        private static Encoding UTF8 = Encoding.UTF8;
        private static string PATH = "user/block";
        private string appKey;
        private string appSecret;

        public RongCloud RongCloud { get; set; }

        public Block(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        /**
         * 封禁用户方法（每秒钟限 100 次）
         *
         * @param  user :用户信息 Id，minute（必传）
         *
         * @return Result
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
            sb.Append("&minute=").Append(HttpUtility.UrlEncode(user.Minute.ToString(), UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/user/block.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 解除用户封禁方法（每秒钟限 100 次）
         *
         * @param  userId:用户 Id。（必传）
         *
         * @return ResponseResult
         **/
        public async Task<ResponseResult> Remove(string userId)
        {
            //参数校验
            string message = CommonUtil.CheckParam("id", userId, PATH, CheckMethod.REMOVE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(userId, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/user/unblock.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }

        /**
         * 获取被封禁用户方法（每秒钟限 100 次）
         *
         *
         * @return QueryBlockUserResult
         **/
        public async Task<BlockUserResult> GetList()
        {
            StringBuilder sb = new StringBuilder();
            string body = sb.ToString();
            if (body.IndexOf("&", StringComparison.OrdinalIgnoreCase) == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/user/block/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<BlockUserResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }
    }
}