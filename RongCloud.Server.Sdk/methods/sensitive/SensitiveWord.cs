using io.rong.util;
using io.rong.models.sensitiveword;
using io.rong.models;
using io.rong.models.response;
using System;
using System.Text;
using System.Web;

namespace io.rong.methods.sensitive
{
    /**
     *
     * 敏感词服务
     * docs: "http://www.rongcloud.cn/docs/server.html#sensitiveword"
     *
     * */
    public class SensitiveWord

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "sensitiveword";

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
        public SensitiveWord(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }


        /**
         * 添加敏感词方法（设置敏感词后，App 中用户不会收到含有敏感词的消息内容，默认最多设置 50 个敏感词。） 
         * 
         * @param  sensitiveword:敏感词
         * @return ResponseResult
         **/
        public ResponseResult Add(SensitiveWordModel sensitiveword)
        {

            string errMsg = CommonUtil.CheckFiled(sensitiveword, PATH, CheckMethod.ADD);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&word=").Append(HttpUtility.UrlEncode(sensitiveword.Keyword, UTF8));

            if (0 == sensitiveword.Type)
            {
                if (null == sensitiveword.Replace)
                {
                    return new ResponseResult(1002, "replace 参数为必传项");
                }
                sb.Append("&replaceWord=").Append(HttpUtility.UrlEncode(sensitiveword.Replace, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                         RongCloud.ApiHostType.Type + "/sensitiveword/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 查询敏感词列表方法 
         * 
         * @param  type:查询敏感词的类型，0 为查询替换敏感词，1 为查询屏蔽敏感词，2 为查询全部敏感词。默认为 1。（非必传）
         *
         * @return ListWordfilterResult
         **/
        public ListWordfilterResult GetList(int type)
        {
            StringBuilder sb = new StringBuilder();

            if (type != null)
            {
                sb.Append("&type=").Append(HttpUtility.UrlEncode(type.ToString(), UTF8));
            }
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                             RongCloud.ApiHostType.Type + "/sensitiveword/list.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ListWordfilterResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }

        /**
         * 移除敏感词方法（从敏感词列表中，移除某一敏感词。） 
         * 
         * @param  word:敏感词，最长不超过 32 个字符。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Remove(string word)
        {
            string message = CommonUtil.CheckParam("keyword", word, PATH, CheckMethod.REMOVE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("&word=").Append(HttpUtility.UrlEncode(word, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                RongCloud.ApiHostType.Type + "/sensitiveword/delete.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }
        /**
         * 批量移除敏感词方法（从敏感词列表中，移除某一敏感词。）
         *
         * @param  words:敏感词数组，一次最多移除 50 个敏感词（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult BatchDelete(string[] words)
        {
            string message = CommonUtil.CheckParam("keyword", words, PATH, CheckMethod.BATCH_DELETE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }
            StringBuilder sb = new StringBuilder();
            foreach (var word in words)
            {
                sb.Append("&words=").Append(HttpUtility.UrlEncode(word, UTF8));
            }
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                   RongCloud.ApiHostType.Type + "/sensitiveword/batch/delete.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.BATCH_DELETE, result));

        }

    }
}