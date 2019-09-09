using io.rong.methods.user.blacklist;
using io.rong.methods.user.block;
using io.rong.methods.user.onlineStatus;
using io.rong.models;
using io.rong.models.response;
using io.rong.models.push;
using io.rong.methods.user.tag;
using io.rong.util;
using System;
using System.Text;
using System.Web;

namespace io.rong.methods.user
{
    public class User
    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "user";
        private string appKey;
        private string appSecret;
        public Block block;
        public Blacklist blackList;
        public OnlineStatus onlineStatus;
        public Tag tag;
        private RongCloud rongCloud;

        public RongCloud RongCloud
        {
            get { return rongCloud; }
            set
            {
                rongCloud = value;
                block.RongCloud = value;
                blackList.RongCloud = value;
                onlineStatus.RongCloud = value;
                tag.RongCloud = value;
            }
        }


        public User(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            block = new Block(appKey, appSecret);
            blackList = new Blacklist(appKey, appSecret);
            onlineStatus = new OnlineStatus(appKey, appSecret);
            tag = new Tag(appKey, appSecret);
        }
        /**
         * 获取 Token 方法 
         * url  "/user/getToken"
         * docs "http://rongcloud.cn/docs/server.html#getToken"
         *
         * @param user 用户信息 id,name,portrait(必传)
         *
         * @return TokenResult
         **/
        public TokenResult Register(UserModel user)
        {
            //需要校验的字段
            string message = CommonUtil.CheckFiled(user, PATH, CheckMethod.REGISTER);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<TokenResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(user.id, UTF8));
            sb.Append("&name=").Append(HttpUtility.UrlEncode(user.name, UTF8));
            sb.Append("&portraitUri=").Append(HttpUtility.UrlEncode(user.portrait, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length-1);
            }

            string result = RongHttpClient.ExecutePost(appKey, appSecret, body,
                    rongCloud.ApiHostType.Type + "/user/getToken.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<TokenResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REGISTER, result));
        }

        /**
         * 刷新用户信息方法 
         * url  "/user/refresh"
         * docs "http://www.rongcloud.cn/docs/server.html#user_refresh"
         *
         * @param user 用户信息 id name portrait(必传)
         *
         * @return ResponseResult
         **/
        public Result Update(UserModel user)
        {
            //需要校验的字段
            string message = CommonUtil.CheckFiled(user, PATH, CheckMethod.UPDATE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(user.id, UTF8));

            if (user.name != null)
            {
                sb.Append("&name=").Append(HttpUtility.UrlEncode(user.name, UTF8));
            }

            if (user.portrait != null)
            {
                sb.Append("&portraitUri=").Append(HttpUtility.UrlEncode(user.portrait, UTF8));
            }
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length-1);
            }
            string result = RongHttpClient.ExecutePost(appKey, appSecret, body,
                    RongCloud.ApiHostType.Type + "/user/refresh.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.UPDATE, result));
        }
    }
}
