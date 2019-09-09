using io.rong.models.response;
using io.rong.methods.sensitive;
using io.rong.models.sensitiveword;
using System;

namespace io.rong.example.group
{

    public class SensitiveExample

    {
        /**
         * 此处替换成您的appKey
         * */
        private static readonly string appKey = "appKey";
        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "appSecret";
        /**
         * 自定义api地址
         * */
        private static readonly string api = "http://api.cn.ronghub.com";

        static void Main(string[] args)
        {

            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api 地址方式
            // RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            SensitiveWord SensitiveWord = rongCloud.Sensitiveword;

            /**
             *API 文档: http://www.rongcloud.cn/docs/server_sdk_api/sensitive/sensitive.html#add
             *
             * 添加替换敏感词方法
             *
             * */
            SensitiveWordModel sentiveWord = new SensitiveWordModel()
            {
                Type = 0,
                Keyword = "黄赌黄",
                Replace = "***"
            };

            ResponseResult addesult = SensitiveWord.Add(sentiveWord);
            Console.WriteLine("sentiveWord add:  " + addesult);

            /**
             *API 文档: http://www.rongcloud.cn/docs/server_sdk_api/sensitive/sensitive.html#add
             *
             * 添加替换敏感词方法
             *
             * */
            sentiveWord = new SensitiveWordModel()
            {
                Type = 1,
                Keyword = "黄赌黄"
            };

            ResponseResult addersult = SensitiveWord.Add(sentiveWord);
            Console.WriteLine("sentiveWord  add replace :  " + addersult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/sensitive/sensitive.html#getList
             * 查询敏感词列表方法
             *
             * */
            ListWordfilterResult result = SensitiveWord.GetList(1);
            Console.WriteLine("getList:  " + result);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/sensitive/sensitive.html#remove
             * 移除敏感词方法（从敏感词列表中，移除某一敏感词。）
             *
             * */

            ResponseResult removeesult = SensitiveWord.Remove("黄赌黄");
            Console.WriteLine("SensitivewordDelete:  " + removeesult);


            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/sensitive/sensitive.html#remove
             * 批量移除敏感词方法（从敏感词列表中，批量移除某一敏感词。）
             *
             * */
            string[] words = { "黄赌毒" };
            ResponseResult batchDeleteResult = SensitiveWord.BatchDelete(words);
            Console.WriteLine("SensitivewordbatchDelete:  " + batchDeleteResult);
            Console.ReadLine();

        }
    }
}
