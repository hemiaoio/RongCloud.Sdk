using io.rong.models;
using io.rong.models.response;
using io.rong.util;
using System;
using System.Text;
using System.Web;

namespace io.rong.methods.sensitive
{
    /**
     *
     *  敏感词服务
     * docs: "http://www.rongcloud.cn/docs/server.html#sensitiveword"
     *
     * */
    public class Wordfilter

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "sensitiveword";


        public Wordfilter(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }


        /**
         * 添加敏感词方法（设置敏感词后，App 中用户不会收到含有敏感词的消息内容，默认最多设置 50 个敏感词。） 
         * 
         * @param  word:敏感词，最长不超过 32 个字符。（必传）
         *
         * @return ResponseResult
         **/
        public ResponseResult Add(string word)
        {
            string message = CommonUtil.CheckParam("keyword", word, PATH, CheckMethod.ADD);
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
                             RongCloud.ApiHostType.Type + "/wordfilter/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));
        }

        /**
         * 查询敏感词列表方法 
         * 
         *
         * @return ListWordfilterResult
         **/
        public ListWordfilterResult GetList()
        {
            StringBuilder sb = new StringBuilder();
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                              RongCloud.ApiHostType.Type + "/wordfilter/list.json", "application/x-www-form-urlencoded");

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
            if (word == null)
            {
                return new ResponseResult(1002, "Paramer 'word' is required");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&word=").Append(HttpUtility.UrlEncode(word, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                               RongCloud.ApiHostType.Type + "/wordfilter/delete.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }
    }
}