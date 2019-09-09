using io.rong.models.response;
using io.rong.methods.chatroom.demotion;
using System;

namespace io.rong.example.chatroom
{
    public class DemotionExample

    {
        /**
         * 此处替换成您的appKey
         * */
        private static readonly string appKey = "kj7swf8okyqt2";
        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "mFe3U1UClx4gx";

        /**
         * 自定义api地址
         * */
        private static readonly string api = "http://api.cn.ronghub.com";


        static void Main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api地址方式
            //RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Demotion demotion = rongCloud.Chatroom.demotion;

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/demotion.html#add
             * 添加应用内聊天室降级消息
             *
             * */
            string[] messageType = { "RC:VcMsg", "RC:ImgTextMsg", "RC:ImgMsg" };
            ResponseResult addResult = demotion.Add(messageType);
            Console.WriteLine("add demotion:  " + addResult);

            /**
             *
             *API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/demotion.html#remove
             * 移除应用内聊天室降级消息
             *
             * */
            ResponseResult removeResult = demotion.Remove(messageType);
            Console.WriteLine("remove demotion:  " + removeResult);


            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/demotion.html#getList
             * 添加聊天室消息优先级demo
             *
             * */
            ChatroomDemotionMsgResult demotionMsgResult = demotion.GetList();
            Console.WriteLine("get demotion:  " + demotionMsgResult);

            Console.ReadLine();

        }
    }
}
