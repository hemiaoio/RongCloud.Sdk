using System;
using System.Text;
using io.rong.models.response;
using io.rong.models;
using io.rong.util;
using io.rong.models.push.tag;
using System.Web;

namespace io.rong.methods.user.tag
{
    public class Tag
    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "user/tag";
        private string appKey;
        private string appSecret;

        public RongCloud RongCloud { get; set; }

        public Tag(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        /**
         * 设置用户标签
         */
        public ResponseResult Set(TagModel tagModel)
        {
            //需要校验的字段
            string message = CommonUtil.CheckFiled(tagModel, PATH, CheckMethod.TAG_SET);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            string body = RongJsonUtil.ObjToJsonString(tagModel);

            string result = RongHttpClient.ExecutePost(appKey, appSecret, body,
                    RongCloud.ApiHostType.Type + "/user/tag/set.json", "application/json");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.TAG_SET, result));
        }

        /**
         * 批量设置用户标签
         */
        public ResponseResult BatchSet(TagModel tagModel)
        {
            string message = CommonUtil.CheckFiled(tagModel, PATH, CheckMethod.TAG_BATCH_SET);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            string body = RongJsonUtil.ObjToJsonString(tagModel);

            string result = RongHttpClient.ExecutePost(appKey, appSecret, body,
                    RongCloud.ApiHostType.Type + "/user/tag/batch/set.json", "application/json");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.TAG_BATCH_SET, result));
        }


        /**
         * 获取用户标签
         */
        public Result Get(string[] userIds)
        {
           if (userIds.Length < 1)
            {
                return new Result(20005, "用户 Id 不能为空");
            }

            StringBuilder sb = new StringBuilder();
            foreach (string userId in userIds)
            {
                sb.Append("&userIds=").Append(HttpUtility.UrlEncode(userId, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(appKey, appSecret, body,
                    RongCloud.ApiHostType.Type + "/user/tags/get.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<TagListResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.TAG_GET, result));
        }
    }
}
